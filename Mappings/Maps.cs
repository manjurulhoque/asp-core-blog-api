using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using blogapi.Models;
using blogapi.ViewModels;

namespace blogapi.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Post, PostViewModel>().ReverseMap();
        }
    }
}
