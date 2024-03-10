using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreLayer.Enumerators;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TeamVM;
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
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Team> _repository;
        private readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toast;
        private const string Section = "Team Section";

        public TeamService(IMapper mapper, IUnitOfWork unitOfWork, IImageHelper imageHelper, IToastNotification toast)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Team>();
            _imageHelper = imageHelper;
            _toast = toast;
        }




        public async Task<List<TeamListVM>> GetAllListAsync()
        {
            var teamListVM = await _repository.GetAllEntity().ProjectTo<TeamListVM>
                (_mapper.ConfigurationProvider).ToListAsync();

            return teamListVM;
        }

        public async Task AddTeamAsync(TeamAddVM request)
        {
            var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.team, null);
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

            var team = _mapper.Map<Team>(request);
            await _repository.AddEntityAsync(team);
            await _unitOfWork.CommitAsync();

            _toast.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _repository.GetEntityByIdAsync(id);
            _repository.DeleteEntity(team);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(team.FileName);

            _toast.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task<TeamUpdateVM> GetTeamById(int id)
        {
            var team = await _repository.Where(x => x.Id == id).ProjectTo<TeamUpdateVM>(_mapper.ConfigurationProvider).SingleAsync();
            return team;
        }

        public async Task UpdateTeamAsync(TeamUpdateVM request)
        {
            var oldTeam = await _repository.Where(x => x.Id == request.Id).AsNoTracking().FirstAsync();
            if (request.Photo != null)
            {
                var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.team, null);
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
				request.FileName = oldTeam.FileName;
				request.FileType = oldTeam.FileType;
			}

			var team = _mapper.Map<Team>(request);
            _repository.UpdateEntity(team);
			var result = await _unitOfWork.CommitAsync();
			if (!result)
			{
				_imageHelper.DeleteImage(request.FileName);
				throw new ClientSideExceptions(ExceptionMessages.ConcurencyException);
			}

			if (request.Photo != null)
            {
                _imageHelper.DeleteImage(oldTeam.FileName);
            }

            _toast.AddInfoToastMessage(NotificationMessagesWebApplication.UpdateMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        // UI Side Methods ..

        public async Task<List<TeamVMForUI>> GetAllListForUIAsync() 
        {
            var uiList = await _repository.GetAllEntity().ProjectTo<TeamVMForUI>
                (_mapper.ConfigurationProvider).ToListAsync();
            return uiList;
        }
    }
}
