using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Entities.Entities.Identity
{
    public class IdentityAccountRole : IdentityRole<int>
    {
        public IdentityAccountRole(string name) : base(name) { }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}
