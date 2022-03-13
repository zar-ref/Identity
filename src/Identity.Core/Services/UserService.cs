using Identity.Core.Mapping;
using Identity.DTO;
using Identity.Entities.Entities;
using Identity.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnityOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddUser(int applicationId, SignInUserDTO userDTO)
        {
            var contextName = GetContextNameFromApplicationId(applicationId);
            using (var unitOfWork = GetUnitOfWorkInstance())
            {
                var userModel = CoreMapper.Mapper.Map<Models.Entities.User>(userDTO);
                var userEntity = CoreMapper.Mapper.Map<User>(userModel);
                await unitOfWork.UserRepository.Add( contextName , userEntity);
                await unitOfWork.Save(contextName);
            }
        }
    }
}
