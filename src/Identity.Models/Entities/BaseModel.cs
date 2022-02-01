using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models.Entities
{
    public class BaseModel
    {
        public T ShallowCopy<T>() where T : BaseModel
        {
            return (T)(MemberwiseClone());
        }
        [Key]
        public int Id { get; set; }

        public bool isNew
        {
            get { return Id <= 0; }

        }

        private bool _deleted;
        public bool Deleted
        {
            get
            {
                if (isNew)
                    return false;
                else
                    return _deleted;
            }

            set { _deleted = value; }
        }

        private bool _active;

        public bool Active
        {
            get
            {
                if (isNew)
                    return true;
                else
                    return _active;
            }

            set { _active = value; }
        }

    }
}
