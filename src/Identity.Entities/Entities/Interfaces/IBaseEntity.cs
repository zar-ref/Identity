using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Entities.Entities.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime? CreateDate { get; set; }
        string CreateUser { get; set; }
        bool? Deleted { get; set; }
        DateTime? DeletedDate { get; set; }
        DateTime? ModifyDate { get; set; }
        string ModifyUser { get; set; }
    }
}
