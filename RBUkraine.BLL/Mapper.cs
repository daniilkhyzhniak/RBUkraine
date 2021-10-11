using AutoMapper;
using RBUkraine.BLL.Models;
using RBUkraine.BLL.Models.Animal;
using RBUkraine.DAL.Entities;
using System.Linq;
using RBUkraine.BLL.Models.User;

namespace RBUkraine.BLL
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserCreationModel, User>()
                .ForMember(x => x.Password, opt => opt.Ignore());

            CreateMap<AnimalImage, Image>()
                .ReverseMap();
            CreateMap<Animal, AnimalModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => x.AnimalImages.FirstOrDefault()));
            CreateMap<Animal, AnimalDetailsModel>()
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.AnimalImages));
            CreateMap<AnimalEditorModel, Animal>()
                .ForMember(x => x.AnimalImages, opt => opt.MapFrom(x => x.Images));
        }
    }
}