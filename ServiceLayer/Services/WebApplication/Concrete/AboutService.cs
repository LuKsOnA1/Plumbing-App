using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreLayer.Enumerators;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutVM;
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
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<About> _repository;
        private readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toast;
        private const string Section = "About Section";

		public AboutService(IMapper mapper, IUnitOfWork unitOfWork, IImageHelper imageHelper, IToastNotification toast)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_repository = _unitOfWork.GetGenericRepository<About>();
			_imageHelper = imageHelper;
			_toast = toast;
		}




		public async Task<List<AboutListVM>> GetAllListAsync()
        {
            var aboutListVM = await _repository.GetAllEntity().ProjectTo<AboutListVM>
                (_mapper.ConfigurationProvider).ToListAsync();

            return aboutListVM;
        }

        public async Task AddAboutAsync(AboutAddVM request)
        {
            var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.about, null);
            
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

            var about = _mapper.Map<About>(request);
            await _repository.AddEntityAsync(about);
            await _unitOfWork.CommitAsync();

            _toast.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task DeleteAboutAsync(int id)
        {
            var about = await _repository.GetEntityByIdAsync(id);
            _repository.DeleteEntity(about);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(about.FileName);

			_toast.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessages(Section), new ToastrOptions
			{
				Title = NotificationMessagesWebApplication.SuccessedTitle
            });
		}

        public async Task<AboutUpdateVM> GetAboutById(int id)
        {
            var about = await _repository.Where(x => x.Id == id).ProjectTo<AboutUpdateVM>(_mapper.ConfigurationProvider).SingleAsync();
            return about;
        }

        public async Task UpdateAboutAsync(AboutUpdateVM request)
        {
            var oldAbout = await _repository.Where(x => x.Id == request.Id).AsNoTracking().FirstAsync();
            
            if (request.Photo != null)
            {
      
                var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.about, null);
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

            var about = _mapper.Map<About>(request);
            _repository.UpdateEntity(about);
            var result = await _unitOfWork.CommitAsync();
            if (!result)
            {
				_imageHelper.DeleteImage(request.FileName);
                throw new ClientSideExceptions(ExceptionMessages.ConcurencyException);
			}

            if (request.Photo != null)
            {
                _imageHelper.DeleteImage(oldAbout.FileName);
            }

			_toast.AddInfoToastMessage(NotificationMessagesWebApplication.UpdateMessages(Section), new ToastrOptions
			{
				Title = NotificationMessagesWebApplication.SuccessedTitle
			});
		}

        // UI Service Methods ...

        public async Task<List<AboutVMForUI>> GetAllListForUIAsync()
        {
            var uiList = await _repository.GetAllEntity().ProjectTo<AboutVMForUI>
                (_mapper.ConfigurationProvider).ToListAsync();
            return uiList;
        }

	}
}
