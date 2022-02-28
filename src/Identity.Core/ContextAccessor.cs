using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
    public class ContextAccessor
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _environment;

        public ContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUser()
        {
            return (string)_httpContextAccessor.HttpContext.Items["User"];
        }

        public int GetApplicationType()
        {
            return (int)_httpContextAccessor.HttpContext.Items["ApplicationType"];
        }

    }
}
