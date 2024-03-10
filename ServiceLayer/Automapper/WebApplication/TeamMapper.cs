using AutoMapper;
using EntityLayer.WebApplication.ViewModels.TeamVM;
using EntityLayer.WebApplication.Entities;


namespace ServiceLayer.Automapper.WebApplication
{
    public class TeamMapper : Profile
    {
        public TeamMapper()
        {
            CreateMap<Team, TeamListVM>().ReverseMap();
            CreateMap<Team, TeamAddVM>().ReverseMap();
            CreateMap<Team, TeamUpdateVM>().ReverseMap();
            CreateMap<Team, TeamVMForUI>().ReverseMap();
        }
    }

}
