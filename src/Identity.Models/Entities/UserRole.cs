using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models.Entities
{
    public class UserRole: BaseModel
    {
        public string Name { get; set; }
        public bool Active { get; set; }

    }
}
