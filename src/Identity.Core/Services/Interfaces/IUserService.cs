using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.DTO;
namespace Identity.Core.Services
{
    public interface IUserService
    {


        Task AddUser(int applicationId , SignInUserDTO user);
    }
}
