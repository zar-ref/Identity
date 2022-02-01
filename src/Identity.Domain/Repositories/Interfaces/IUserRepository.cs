using Identity.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {

        void Add(User account);
        void Delete(int? id);
    }
}
