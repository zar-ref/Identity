using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Entities.Entities.Identity
{
    //tutorial: https://www.c-sharpcorner.com/article/jwt-authentication-and-authorization-in-net-6-0-with-identity-framework/
    public class Account : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(50)")]
        public string ApplicationCode { get; set; }

        public virtual ICollection<IdentityAccountRole> IdentityRoles { get; set; }

        //Locations collection to add in the future...
    }


}
