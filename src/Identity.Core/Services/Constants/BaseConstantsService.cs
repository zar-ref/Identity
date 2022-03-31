using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Services.Constants
{
    public class BaseConstantsService : IBaseContantsService
    {
        public virtual string[] AppllicationAccountRoles { get; set; } = new string[]
        {
            GenericConstants.ApplicationRoles.User.ToString(),
            GenericConstants.ApplicationRoles.Manager.ToString(),
            GenericConstants.ApplicationRoles.Admin.ToString(),
        };
    }
}
