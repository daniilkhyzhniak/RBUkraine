using AutoMapper;
using Microsoft.AspNetCore.Http;
using RBUkraine.BLL.Models;
using RBUkraine.BLL.Models.Animal;
using RBUkraine.PL.ViewModels;
using RBUkraine.PL.ViewModels.Animals;
using RBUkraine.PL.ViewModels.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RBUkraine.BLL.Models.CharitableOrganization;
using RBUkraine.BLL.Models.CharityEvent;
using RBUkraine.BLL.Models.News;
using RBUkraine.BLL.Models.Product;
using RBUkraine.BLL.Models.User;
using RBUkraine.PL.ViewModels.CharitableOrganizations;
using RBUkraine.PL.ViewModels.CharityEvents;
using RBUkraine.PL.ViewModels.News;
using RBUkraine.PL.ViewModels.Products;

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
            CreateMap<UserViewModel, UserEditorModel>();

            CreateMap<AnimalEditorViewModel, AnimalEditorModel>()
                .ForMember(x => x.Images, opt => opt.MapFrom(x => MapFilesToImages(x.Files)));
            CreateMap<AnimalModel, AnimalEditorViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => MapImage(x.Image)));
            CreateMap<AnimalModel, AnimalViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => MapImage(x.Image)));
            CreateMap<AnimalDetailsModel, AnimalDetailsViewModel>()
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images.Select(MapImage)));
            CreateMap<AnimalModel, AnimalTranslateEditorViewModel>();
            CreateMap<AnimalTranslateEditorViewModel, AnimalTranslateEditorModel>();

            CreateMap<CharityEventModel, CharityEventViewModel>();
            CreateMap<CharityEventModel, CharityEventEditorViewModel>();
            CreateMap<CharityEventEditorViewModel, CharityEventEditorModel>();

            CreateMap<CharitableOrganizationModel, CharitableOrganizationViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => MapImage(x.Image)));
            CreateMap<CharitableOrganizationModel, CharitableOrganizationEditorViewModel>();
            CreateMap<CharitableOrganizationEditorViewModel, CharitableOrganizationEditorModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => MapFileToImage(x.File)));

            CreateMap<NewsModel, NewsViewModel>();
            CreateMap<NewsModel, NewsEditorModel>();
            CreateMap<NewsEditorViewModel, NewsEditorModel>();

            CreateMap<ProductModel, ProductViewModel>();
            CreateMap<ProductModel, ProductEditorViewModel>();
            CreateMap<ProductEditorViewModel, ProductEditorModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => MapFileToImage(x.File)));
        }

        private static Image MapFileToImage(IFormFile file)
        {
            if (file is null)
            {
                return null;
            }

            using var memoryStream = new MemoryStream();

            var img = new Image
            {
                Title = file.FileName
            };

            file.CopyTo(memoryStream);
            img.Data = memoryStream.ToArray();
            memoryStream.Close();

            return img;
        }

        private static IEnumerable<Image> MapFilesToImages(IFormFileCollection files)
        {
            var images = new List<Image>();

            if (!files?.Any() ?? true)
            {
                return images;
            }

            using var memoryStream = new MemoryStream();

            foreach (var file in files)
            {
                var img = new Image
                {
                    Title = file.FileName
                };

                file.CopyTo(memoryStream);
                img.Data = memoryStream.ToArray();
                images.Add(img);
            }

            memoryStream.Close();

            return images;
        }
        private static ImageViewModel MapImage(Image image)
        {
            var imageBase64Data = Convert.ToBase64String(image.Data);
            var imageUrl = $"data:image/jpg;base64,{imageBase64Data}";

            return new ImageViewModel
            {
                Id = image.Id,
                Title = image.Title,
                Url = imageUrl
            };
        }
    }
}