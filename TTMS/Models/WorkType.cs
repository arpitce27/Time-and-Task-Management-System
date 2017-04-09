using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTMS.Models
{
    public class WorkType
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Work Type")]
        public string TypeName { get; set; }
        public virtual ICollection<Work> Work { get; set; }

    }
}