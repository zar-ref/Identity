using Identity.Domain.Repositories;
using Identity.Infrastructure.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.DataAccess
{
    public interface IUnityOfWork: IDisposable
    {


        Dictionary<int, string> ContextNamesByApplicationIdDictionary { get; set; }
        Dictionary<string, BaseContext> DbContextByContextNameDictionary { get; set; }

        Task<int> Save(string contextName);
        int GetLastIdInsert();
        IUserRepository UserRepository { get; }

        Task ManualDispose(string contextName); //To be used on jobs
    }
}
