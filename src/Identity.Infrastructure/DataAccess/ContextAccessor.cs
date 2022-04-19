using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.DataAccess
{
    public class ContextAccessor : IContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUser()
        {
            return (string)_httpContextAccessor.HttpContext.Items["User"];
        }

        public int GetApplicationId()
        {
            return (int)_httpContextAccessor.HttpContext.Items["ApplicationId"];
        }

        public string GetApplicationContextName()
        {
            return (string)_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(_claim => _claim.Type == "ApplicationId")?.Value ?? String.Empty;
        }
    }
}
