using Identity.Domain.Repositories;
using Identity.Entities.Entities;
using Identity.Infrastructure.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICollection<BaseContext> _dbContexts;
        private BaseContext _currentContext;
        //public UserRepository(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public UserRepository(ICollection<BaseContext> dbContexts)
        {
            _dbContexts = dbContexts;
            _currentContext = dbContexts.FirstOrDefault();
        }
        public async Task Add(string contextName, User account)
        {
            //await _context.AddAsync(account);
            await _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).AddAsync(account);
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }


    }
}
