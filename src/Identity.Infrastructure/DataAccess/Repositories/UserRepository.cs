using Identity.Domain.Repositories;
using Identity.Entities.Entities;
using Identity.Entities.Entities.Identity;
using Identity.Infrastructure.DataAccess.DbContexts;
using Microsoft.AspNetCore.Identity;
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

        public UserRepository(ICollection<BaseContext> dbContexts)
        {
            _dbContexts = dbContexts;
        }
        public async Task Add(string contextName, User account)
        {
            //await _context.AddAsync(account);
            await _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).AddAsync(account);
        }

        public async Task<bool> AddAccount(string contextName, Account account, string password)
        {
            var userExists = await _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).UserManager.FindByEmailAsync(account.Email);
            if (userExists != null)
                return false;
            var result = await _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).UserManager.CreateAsync(account, password);
            if (!result.Succeeded)
                return false;

            return true;
        }

        public async Task<Account> Login(string contextName, string email, string password)
        {
            //var user = _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).UserManager.Users.FirstOrDefault(_account => _account.NormalizedEmail == email);
            var user = await _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).UserManager.FindByEmailAsync(email);

            if (user == null)
                return null;
            var isPassowrdCorrect = await _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).UserManager.CheckPasswordAsync(user, password);
            if (!isPassowrdCorrect)
                return null;

            var roles = _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).RoleManager.Roles.Where(_r => _r.Account == user).ToList(); //.FirstOrDefault(F => F.AccountId == user.Id);

            user.IdentityRoles = roles;
            return user;
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangePassword(string contextName, string email, string oldPassword, string newPassowrd)
        {
            var user = await _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).UserManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            var result = await _dbContexts.FirstOrDefault(_ctx => _ctx.ContextName == contextName).UserManager.ChangePasswordAsync(user, oldPassword, newPassowrd);

            return result.Succeeded;
        }


    }
}
