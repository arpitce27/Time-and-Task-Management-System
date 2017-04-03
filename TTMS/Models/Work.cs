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
        [Key]
        public int ID { get; set; }
        public int WorkTypeID { get; set; }
        public Priority Priority { get; set; }
        public string WorkTitle { get; set; }
        public string WorkDescr { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }

        [DisplayFormat(NullDisplayText = "Initiated")]
        public Status? Status { get; set; }

        public virtual WorkType WorkType { get; set; }

    }
}