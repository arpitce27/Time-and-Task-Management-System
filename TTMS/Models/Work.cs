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
            this.Comments = new HashSet<Comment>();
            this.RelatedWorkLogs = new HashSet<WorkLog>();
        }
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "You must choose the work type")]
        [Display(Name = "Work Type")]
        public int WorkTypeID { get; set; }

        [Required(ErrorMessage = "You must choose the priority")]
        public Priority Priority { get; set; }

        [Required(ErrorMessage = "Work title field is required")]
        [StringLength(200, MinimumLength = 5)]
        [Display(Name = "Work Title")]
        public string WorkTitle { get; set; }

        [Required(ErrorMessage = "Description field is required")]
        [StringLength(400, MinimumLength = 5)]
        [Display(Name = "Description")]
        public string WorkDescr { get; set; }

        [Required(ErrorMessage = "Creation Date field is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Creation Date")]
        public System.DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Deadline Date field is required")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Deadline { get; set; }

        [DisplayFormat(NullDisplayText = "Initiated")]
        public Status? Status { get; set; }

        [Display(Name = "Hour Worked")]
        public double HourWorked
        {
            get
            {
                double _HourWorked = 0;
                foreach (var i in RelatedWorkLogs)
                {
                    _HourWorked = _HourWorked + i.TimeSpend;
                }
                return _HourWorked;
            }
        }
        public virtual WorkType WorkType { get; set; }

        [Display(Name = "Assigned Students")]
        public virtual ICollection<User> Assignedstudents { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<WorkLog> RelatedWorkLogs { get; set; }

    }
}