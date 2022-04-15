using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.DataAccess
{
    public interface IContextAccessor
    {
        string GetUser();
        int GetApplicationId();
        string GetApplicationContextName();
    }
}
