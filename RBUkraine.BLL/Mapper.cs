using AutoMapper;
using RBUkraine.BLL.Models;
using RBUkraine.BLL.Models.Animal;
using RBUkraine.DAL.Entities;
using System.Linq;
using RBUkraine.BLL.Models.CharitableContribution;
using RBUkraine.BLL.Models.CharitableOrganization;
using RBUkraine.BLL.Models.CharityEvent;
using RBUkraine.BLL.Models.News;
using RBUkraine.BLL.Models.Product;
using RBUkraine.BLL.Models.User;
using RBUkraine.BLL.Models.VolunteerApplication;

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
                .ForMember(x => x.CharitableOrganization, opt => opt.Ignore())
                .ForMember(x => x.Image, opt => opt.MapFrom(x => x.AnimalImages.FirstOrDefault()));
            CreateMap<Animal, AnimalDetailsModel>()
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.AnimalImages));
            CreateMap<AnimalEditorModel, Animal>()
                .ForMember(x => x.CharitableOrganization, opt => opt.Ignore())
                .ForMember(x => x.AnimalImages, opt => opt.MapFrom(x => x.Images));

            CreateMap<CharityEvent, CharityEventModel>();
            CreateMap<CharityEventEditorModel, CharityEvent>();

            CreateMap<CharitableOrganizationImage, Image>()
                .ReverseMap();
            CreateMap<CharitableOrganization, CharitableOrganizationModel>()
                .ForMember(x => x.Animals, opt => opt.Ignore());
            CreateMap<CharitableOrganizationEditorModel, CharitableOrganization>();

            CreateMap<News, NewsModel>();
            CreateMap<NewsEditorModel, News>();

            CreateMap<RatingItem, RatingItemModel>();

            CreateMap<Product, ProductModel>();
            CreateMap<ProductEditorModel, Product>();
            CreateMap<ProductImage, Image>()
                .ReverseMap();

            CreateMap<VolunteerApplication, VolunteerApplicationModel>();
            CreateMap<VolunteerApplicationEditorModel, VolunteerApplication>();
        }
    }
}