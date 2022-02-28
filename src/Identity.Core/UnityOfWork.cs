using Identity.Core;
using Identity.Domain;
using Identity.Domain.Repositories;
using Identity.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Transactions;

namespace Identity.Core
{
    public class UnityOfWork : IUnityOfWork
    {
        private TrackChangesManager _trackChangesManager;
        private bool _disposed;
        private ApplicationDbContext _context;
        public IUserRepository UserRepository { get { return new UserRepository(_context); } }
        public UnityOfWork(IHttpContextAccessor httpContextAccessor)
        {
            _context = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>());
            _trackChangesManager = new TrackChangesManager(httpContextAccessor, _context);
        }

        public int GetLastIdInsert()
        {
            return _trackChangesManager.GetLastIdInsert();
        }
        public async Task<int> Save()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
            {
                try
                {
                    _trackChangesManager.DetectChanges();
                    _trackChangesManager.TrackChanges();
                    int affectedRows = await _context.SaveChangesAsync().ConfigureAwait(false);
                    return affectedRows;


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
}

