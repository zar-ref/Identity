using Identity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task AddAccount(int applicationId, SignInUserDTO user);
    }
}
