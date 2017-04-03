using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTMS.Models
{
    public class WorkAssigned
    {
        public int ID { get; set; }
        public int WorkID { get; set; }
        public int UserID { get; set; }

        public virtual Work Work { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}