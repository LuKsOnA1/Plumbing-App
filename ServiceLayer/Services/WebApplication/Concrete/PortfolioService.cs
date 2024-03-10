using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreLayer.Enumerators;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.PortfolioVM;
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
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Portfolio> _repository;
        private readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toast;
        private const string Section = "Portfolio Section";

        public PortfolioService(IMapper mapper, IUnitOfWork unitOfWork, IImageHelper imageHelper, IToastNotification toast)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Portfolio>();
            _imageHelper = imageHelper;
            _toast = toast;
        }




        public async Task<List<PortfolioListVM>> GetAllListAsync()
        {
            var portfolioListVM = await _repository.GetAllEntity().ProjectTo<PortfolioListVM>
                (_mapper.ConfigurationProvider).ToListAsync();

            return portfolioListVM;
        }

        public async Task AddPortfolioAsync(PortfolioAddVM request)
        {
            var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.portfolio, null);
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

            var portfolio = _mapper.Map<Portfolio>(request);
            await _repository.AddEntityAsync(portfolio);
            await _unitOfWork.CommitAsync();

            _toast.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task DeletePortfolioAsync(int id)
        {
            var portfolio = await _repository.GetEntityByIdAsync(id);
            _repository.DeleteEntity(portfolio);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(portfolio.FileName);

            _toast.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task<PortfolioUpdateVM> GetPortfolioById(int id)
        {
            var portfolio = await _repository.Where(x => x.Id == id).ProjectTo<PortfolioUpdateVM>(_mapper.ConfigurationProvider).SingleAsync();
            return portfolio;
        }

        public async Task UpdatePortfolioAsync(PortfolioUpdateVM request)
        {
            var oldPortfolio = await _repository.Where(x => x.Id == request.Id).AsNoTracking().FirstAsync();
            if (request.Photo != null)
            {
                var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.portfolio, null);
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
                request.FileName = oldPortfolio.FileName;
                request.FileType = oldPortfolio.FileType;
            }

			var portfolio = _mapper.Map<Portfolio>(request);
            _repository.UpdateEntity(portfolio);
			var result = await _unitOfWork.CommitAsync();
			if (!result)
			{
				_imageHelper.DeleteImage(request.FileName);
				throw new ClientSideExceptions(ExceptionMessages.ConcurencyException);
			}

			if (request.Photo != null)
            {
                _imageHelper.DeleteImage(oldPortfolio.FileName);
            }

            _toast.AddInfoToastMessage(NotificationMessagesWebApplication.UpdateMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        // UI Service Methods ...

        public async Task<List<PortfolioVMForUI>> GetAllListForUIAsync()
        {
            var uiList = await _repository.GetAllEntity().ProjectTo<PortfolioVMForUI>
                (_mapper.ConfigurationProvider).ToListAsync();

            return uiList;
        }

	}
}
