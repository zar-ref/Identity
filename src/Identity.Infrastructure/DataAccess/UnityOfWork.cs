﻿using Identity.Domain.Repositories;
using Identity.Entities.Entities.Identity;
using Identity.Infrastructure.DataAccess.DbContexts;
using Identity.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNet.Identity;

namespace Identity.Infrastructure.DataAccess
{
    public class UnityOfWork : IUnityOfWork
    {
        private TrackChangesManager _trackChangesManager;
        public IUserRepository UserRepository { get { return new UserRepository(_dbContexts); } }

        private readonly ICollection<BaseContext> _dbContexts;
        private ContextAccessor _contextAccessor { get; set; }
        public Dictionary<string, BaseContext> DbContextByContextNameDictionary { get; set; } = new Dictionary<string, BaseContext>();
        public Dictionary<int, string> ContextNamesByApplicationIdDictionary { get; set; } = new Dictionary<int, string>()
        {
            {-2 , "Dev2" },
            {-1 , "Dev1" },
        };


        public UnityOfWork(ICollection<BaseContext> dbContexts, IHttpContextAccessor httpContextAccessor)
        {
            _dbContexts = dbContexts;
            _trackChangesManager = new TrackChangesManager(httpContextAccessor, dbContexts);
            _contextAccessor = new ContextAccessor(httpContextAccessor);
            ConstructDbContextDictionary(dbContexts);
            ConstructDbContextIdentityManagers(dbContexts);
        }




        public int GetLastIdInsert()
        {
            return _trackChangesManager.GetLastIdInsert();
        }
        public async Task<int> Save(string contextName)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    _trackChangesManager.DetectChanges(contextName);
                    _trackChangesManager.TrackChanges();
                    int affectedRows = await _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).SaveChangesAsync().ConfigureAwait(false);
                    scope.Complete();
                    return affectedRows;


                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void ConstructDbContextDictionary(ICollection<BaseContext> dbContexts)
        {
            foreach (var context in dbContexts)
                DbContextByContextNameDictionary.Add(context.ContextName, context);

        }

        private void ConstructDbContextIdentityManagers(ICollection<BaseContext> dbContexts)
        {
            foreach (var context in dbContexts)
            {
                var um    = new UserStore<Account, IdentityRole<int>, BaseContext , int>(context) ; 
                var um2 = new Microsoft.AspNetCore.Identity.UserManager<Account>(um , null , new PasswordHasher<Account>(), null, null , null , null, null, null);

                var rm = new RoleStore<IdentityRole<int> , BaseContext, int>(context);
                context.UserManager = um2;
                context.RoleManager = new Microsoft.AspNetCore.Identity.RoleManager<IdentityRole<int>>(rm , null, null, null, null);
            }

        }

        public async void Dispose()
        {
            var contextName = ContextNamesByApplicationIdDictionary[_contextAccessor.GetApplicationId()];
            await _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).DisposeAsync().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }
    }
}
