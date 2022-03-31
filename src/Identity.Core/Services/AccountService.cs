using Identity.Core.Mapping;
using Identity.Core.Services.Interfaces;
using Identity.DTO;
using Identity.Entities.Entities;
using Identity.Entities.Entities.Identity;
using Identity.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Services
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IUnityOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
