using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DTO
{
    public  class SignInUserDTO
    {

        public int ApllicationId { get; set; } //Type of Application

        public int OrganizationId { get; set; } //Application Client 
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
