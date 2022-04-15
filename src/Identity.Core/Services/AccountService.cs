using Identity.Core.Mapping;
using Identity.Core.Services.Interfaces;
using Identity.DTO;
using Identity.Entities.Entities;
using Identity.Entities.Entities.Identity;
using Identity.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IContextAccessor _contextAccessor;

        public AccountService(IUnityOfWork unitOfWork, IConfiguration configuration, IContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public async Task AddAccount(int applicationId, SignInUserDTO userDTO)
        {
            var contextName = GetContextNameFromApplicationId(applicationId);
            using (var unitOfWork = GetUnitOfWorkInstance())
            {
                var account = new Account()
                {
                    ApplicationCode = applicationId.ToString(),
                    Email = userDTO.Email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                await unitOfWork.UserRepository.AddAccount(contextName, account, userDTO.Password);
                await unitOfWork.Save(contextName);
            }
        }

        public async Task<JwtSecurityToken> Login(int applicationId, SignInUserDTO userDTO)
        {
            var contextName = GetContextNameFromApplicationId(applicationId);
            using (var unitOfWork = GetUnitOfWorkInstance())
            {
                var account = await unitOfWork.UserRepository.Login(contextName, userDTO.Email, userDTO.Password);
                if (account == null)
                    return null;
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("ApplicationId", account.ApplicationCode)
                };

                foreach (var role in account.IdentityRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role.Name));
                }

                var token = GetToken(authClaims);
                return token;
            }
        }


        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

            return token;
        }

        public async Task<bool> ChangePassowrd(ChangePasswordDTO changePasswordDto)
        {
            var contextName = _contextAccessor.GetApplicationContextName();
            if (contextName == null)
                return false;
            using (var unitOfWork = GetUnitOfWorkInstance())
            {
                return await unitOfWork.UserRepository.ChangePassword(contextName, changePasswordDto.Email, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
            }


        }
    }
}
