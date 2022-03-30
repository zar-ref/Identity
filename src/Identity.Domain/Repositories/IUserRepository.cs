using Identity.Entities.Entities;
using Identity.Entities.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task Add(string contextName, User account);
        void Delete(int? id);

        Task<bool> AddAccount(string contextName, Account account, string password);
    }
}
