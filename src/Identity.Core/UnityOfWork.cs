using Identity.Domain;
using Identity.Domain.Repositories;
using Identity.Domain.Repositories.Interfaces;
using System;
using System.Transactions;

public class UnityOfWork : IUnityOfWork
{
    private TrackChangesManager _trackChangesManager;
    private bool _disposed;
    private ApplicationDbContext _context;
    public IUserRepository UserRepository { get { return new UserRepository(_context); } }
    public UnityOfWork()
    {

    }

    public int GetLastIdInsert()
    {
        return _trackChangesManager.GetLastIdInsert();
    }
    public void Commit()
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
        {
            try
            {
                _trackChangesManager.DetectChanges();
                _trackChangesManager.TrackChanges();
                _context.SaveChanges();

                //should be assincronous operation 
                scope.Complete();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
