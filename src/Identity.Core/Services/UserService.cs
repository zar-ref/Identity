using Identity.Domain;
using Identity.DTO;
using Identity.Entities.Entities;
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
        public async Task AddUser(SignInUserDTO userDTO)
        {
            using (var unitOfWork = GetUnitOfWorkInstance())
            {
                var userModel = DomainBootsrapperMapper.Mapper.Map<Models.Entities.User>(userDTO);
                var userEntity = DomainBootsrapperMapper.Mapper.Map<User>(userModel);
                await unitOfWork.UserRepository.Add(userEntity);
                await unitOfWork.Save();
            }
        }
    }
}
