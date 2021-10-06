using AutoMapper;
using RBUkraine.BLL.Models;
using RBUkraine.PL.ViewModels.Authentication;

namespace RBUkraine.PL
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<LoginViewModel, AuthModel>();
            CreateMap<RegisterViewModel, AuthModel>();
            CreateMap<RegisterViewModel, UserCreationModel>();
            
            CreateMap<UserModel, UserViewModel>();
        }
    }
}