using Identity.Domain.Repositories.Interfaces;
using System;



namespace Identity.Core
{
    public interface IUnityOfWork : IDisposable
    {
        Task<int> Save();
        int GetLastIdInsert();

        IUserRepository UserRepository { get; }
    }
}

