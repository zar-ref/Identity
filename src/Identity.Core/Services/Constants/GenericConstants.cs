using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Services.Constants
{
    public static class GenericConstants
    {
        public enum ApplicationContextNames //clients of the application
        {
            Dev1,
            Dev2
        }

       public enum ApplicationRoles
        {
            User,
            Manager,
            Admin
        }

    }
}
