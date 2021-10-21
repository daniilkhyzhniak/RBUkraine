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
using RBUkraine.BLL.Models.User;
using RBUkraine.PL.ViewModels.CharitableOrganizations;
using RBUkraine.PL.ViewModels.CharityEvents;

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

            CreateMap<AnimalEditorViewModel, AnimalEditorModel>()
                .ForMember(x => x.Images, opt => opt.MapFrom(x => MapFilesToImages(x.Files)));
            CreateMap<AnimalDetailsModel, AnimalEditorViewModel>();
            CreateMap<AnimalModel, AnimalViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => MapImage(x.Image)));
            CreateMap<AnimalDetailsModel, AnimalDetailsViewModel>()
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images.Select(MapImage)));
            CreateMap<AnimalDetailsModel, AnimalTranslateEditorViewModel>();
            CreateMap<AnimalTranslateEditorViewModel, AnimalTranslateEditorModel>();

            CreateMap<CharityEventModel, CharityEventViewModel>();

            CreateMap<CharitableOrganizationModel, CharitableOrganizationViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => MapImage(x.Image)));
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