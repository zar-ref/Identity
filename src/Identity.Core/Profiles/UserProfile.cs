using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Identity.DTO;
namespace Identity.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SignInUserDTO, Models.Entities.User>()
                    .ForMember(_dst => _dst.UserId, _opt => _opt.Ignore())
                    .ForMember(_dst => _dst.RoleId, _opt => _opt.Ignore())
                    .ForMember(_dst => _dst.Active, _opt => _opt.Ignore());
        }

    }
}
