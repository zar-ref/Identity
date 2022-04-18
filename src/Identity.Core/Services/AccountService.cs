using Identity.Core.Mapping;
using Identity.Core.Services.Interfaces;
using Identity.DTO;
using Identity.Entities.Entities;
using Identity.Entities.Entities.Identity;
using Identity.Infrastructure.DataAccess;
using Identity.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography; 
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

        public async Task<string> Login(int applicationId, SignInUserDTO userDTO)
        {
            var contextName = GetContextNameFromApplicationId(applicationId);
            using (var unitOfWork = GetUnitOfWorkInstance())
            {
                var account = await unitOfWork.UserRepository.Login(contextName, userDTO.Email, userDTO.Password);
                if (account == null)
                    return null;
                var authClaims = new List<Claim>
                {
                    new Claim("Email", account.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("ApplicationId", account.ApplicationCode)
                };

                foreach (var role in account.IdentityRoles)
                {
                    authClaims.Add(new Claim("Roles", role.Name));
                }

                var token = GenerateToken(authClaims);
                return token;
            }
        }


        public string GenerateToken(List<Claim> authClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddHours(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AccountJwtCredentials ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key =Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken; 
                // return user id from JWT token if validation successful
                var retVal =  new AccountJwtCredentials()
                {
                    ApplicationId = jwtToken.Claims.FirstOrDefault(_claim => _claim.Type == "ApplicationId").Value,
                    Email = jwtToken.Claims.FirstOrDefault(_claim => _claim.Type == "Email").Value,
                    Roles = jwtToken.Claims.Where(_claim => _claim.Type == "Roles").Select(_role => _role.Value).ToArray(),
      
                };
                return retVal;
            }
            catch(Exception ex)
            {
                return null;
            }
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
