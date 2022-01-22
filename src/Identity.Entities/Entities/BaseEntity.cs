using Identity.Entities.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Entities.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }


        public string CreateUser { get; set; }

        public DateTime? CreateDate { get; set; }

        public string ModifyUser { get; set; }

        public DateTime? ModifyDate { get; set; }

        [NotMapped]
        public bool isNew
        {
            get { return Id <= 0; }
        }

        //[Display(Name = "Active")]
        //public bool Active { get; set; }

        public bool? Deleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
