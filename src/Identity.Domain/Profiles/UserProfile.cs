using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Entities.Entities;
using Identity.Models.Entities;
namespace Identity.Domain.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<Models.Entities.User, Entities.Entities.User>().ReverseMap();
        }
    }
}
