using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Entities.Entities
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; }
        public bool Active { get; set; }

    }
}
