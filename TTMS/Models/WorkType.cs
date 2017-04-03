using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTMS.Models
{
    public class WorkType
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<Work> Work { get; set; }

    }
}