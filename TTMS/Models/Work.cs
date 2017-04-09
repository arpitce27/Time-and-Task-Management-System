using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTMS.Models
{
    public enum Priority
    {
        Low, Medium, High
    }
    public enum Status
    {
        NotAssigned, Assigned, Inprogress, AboutToFinish, Completed
    }
    public class Work
    {
        public Work()
        {
            this.Assignedstudents = new HashSet<User>();
        }
        [Key]
        public int ID { get; set; }
        [Display(Name = "Work Type")]
        public int WorkTypeID { get; set; }
        public Priority Priority { get; set; }

        [StringLength(200, MinimumLength = 5)]
        [Display(Name = "Work Title")]
        public string WorkTitle { get; set; }
        [StringLength(400, MinimumLength = 5)]
        [Display(Name = "Description")]
        public string WorkDescr { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Creation Date")]
        public System.DateTime CreationDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Deadline { get; set; }

        [DisplayFormat(NullDisplayText = "Initiated")]
        public Status? Status { get; set; }

        public virtual WorkType WorkType { get; set; }
        public virtual ICollection<User> Assignedstudents { get; set; }

    }
}