﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreLayer.Enumerators;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TestimonalVM;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exception.WebApplication;
using ServiceLayer.Helpers.Generic.Image;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class TestimonalService : ITestimonalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Testimonal> _repository;
        private readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toast;
        private const string Section = "Testimonal";

        public TestimonalService(IMapper mapper, IUnitOfWork unitOfWork, IImageHelper imageHelper, IToastNotification toast)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Testimonal>();
            _imageHelper = imageHelper;
            _toast = toast;
        }




        public async Task<List<TestimonalListVM>> GetAllListAsync()
        {
            var testimonalListVM = await _repository.GetAllEntity().ProjectTo<TestimonalListVM>
                (_mapper.ConfigurationProvider).ToListAsync();

            return testimonalListVM;
        }

        public async Task AddTestimonalAsync(TestimonalAddVM request)
        {
            var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.testimonal, null);
            if (imageResult.Error != null)
            {
                _toast.AddErrorToastMessage(imageResult.Error, new ToastrOptions
                {
                    Title = NotificationMessagesWebApplication.FailedTitle
                });
                return;
            }
            request.FileName = imageResult.FileName!;
            request.FileType = imageResult.FileType!;

            var testimonal = _mapper.Map<Testimonal>(request);
            await _repository.AddEntityAsync(testimonal);
            await _unitOfWork.CommitAsync();

            _toast.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task DeleteTestimonalAsync(int id)
        {
            var testimonal = await _repository.GetEntityByIdAsync(id);
            _repository.DeleteEntity(testimonal);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(testimonal.FileName);

            _toast.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task<TestimonalUpdateVM> GetTestimonalById(int id)
        {
            var testimonal = await _repository.Where(x => x.Id == id).ProjectTo<TestimonalUpdateVM>(_mapper.ConfigurationProvider).SingleAsync();
            return testimonal;
        }

        public async Task UpdateTestimonalAsync(TestimonalUpdateVM request)
        {
            var oldTestimonal = await _repository.Where(x => x.Id == request.Id).AsNoTracking().FirstAsync();
            if (request.Photo != null)
            {
                var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.testimonal, null);
                if (imageResult.Error != null)
                {
                    _toast.AddErrorToastMessage(imageResult.Error, new ToastrOptions
                    {
                        Title = NotificationMessagesWebApplication.FailedTitle
                    });
                    return;
                }
                request.FileName = imageResult.FileName!;
                request.FileType = imageResult.FileType!;
            }

            if (request.Photo == null)
            {
                request.FileName = oldTestimonal.FileName;
                request.FileType = oldTestimonal.FileType;
            }

            

            var testimonal = _mapper.Map<Testimonal>(request);
            _repository.UpdateEntity(testimonal);
			var result = await _unitOfWork.CommitAsync();
			if (!result)
			{
				_imageHelper.DeleteImage(request.FileName);
				throw new ClientSideExceptions(ExceptionMessages.ConcurencyException);
			}

			if (request.Photo != null)
            {
                _imageHelper.DeleteImage(oldTestimonal.FileName);
            }

            _toast.AddInfoToastMessage(NotificationMessagesWebApplication.UpdateMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        // UI Side Methods ...

        public async Task<List<TestimonalVMForUI>> GetAllListForUI()
        {
            var uiList = await _repository.GetAllEntity().ProjectTo<TestimonalVMForUI>
                (_mapper.ConfigurationProvider).ToListAsync();
            return uiList;
        }
    }
}
