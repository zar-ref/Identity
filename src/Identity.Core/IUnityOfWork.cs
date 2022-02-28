using System;



namespace Identity.Core
{
    public interface IUnityOfWork : IDisposable
    {
        void Commit();
        int GetLastIdInsert();
    }
}

