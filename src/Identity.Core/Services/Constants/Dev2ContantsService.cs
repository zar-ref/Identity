using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Services.Constants
{
    public class Dev2ContantsService : BaseConstantsService
    {
        public override string[] AppllicationAccountRoles { get; set; } = new string[]
        {
            GenericConstants.ApplicationRoles.Manager.ToString(),
            GenericConstants.ApplicationRoles.Admin.ToString(),
        };
    }
}
