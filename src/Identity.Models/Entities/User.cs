using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models.Entities
{
    public class User : BaseModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public virtual UserRole Role { get; set; }

        public bool? Active { get; set; }
        public DateTime? InactiveDate { get; set; }

        public int ApllicationId { get; set; } //Type of Application

        public int OrganizationId { get; set; } //Application Client 
    }
}
