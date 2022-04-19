using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models.Entities
{
    public class AccountJwtCredentials
    {
        public string ApplicationId { get; set; }
        public string [] Roles { get; set; }
        public string Email { get; set; }
    }
}
