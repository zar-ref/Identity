using Identity.DTO;
using Identity.Entities.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task AddAccount(int applicationId, SignInUserDTO user);
        Task<JwtSecurityToken> Login(int applicationId, SignInUserDTO userDTO);
        JwtSecurityToken GetToken(List<Claim> authClaims);
        Task<bool> ChangePassowrd(ChangePasswordDTO changePasswordDto);
    }
}
