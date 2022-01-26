using AutoMapper;
using Domain.Categories.Models;
using Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class MapperProfile : Profile
    {
        
        public MapperProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
