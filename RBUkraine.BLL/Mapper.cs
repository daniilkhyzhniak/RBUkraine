using AutoMapper;
using RBUkraine.BLL.Models;
using RBUkraine.DAL.Entities;

namespace RBUkraine.BLL
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserModel>();
        }
    }
}