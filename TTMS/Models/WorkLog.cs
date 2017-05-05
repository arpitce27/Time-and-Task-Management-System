using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTMS.Models
{
    public class WorkLog
    {
        public int Id { get; set; }

        [Display(Name = "Works")]
        public int WorkId { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CretionDate { get; set; }

        [Required(ErrorMessage = "Start Time is required")]
        [Display(Name = "Start Time")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End Time is required")]
        [Display(Name = "End Time")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Display(Name = "Total Time Spent")]
        public double TimeSpend
        {
            get;
            //{
            //    return (EndTime - StartTime).TotalHours;
            //}
            set;
            //{
            //    this.TimeSpend = (EndTime - StartTime).TotalHours;
            //}
        }
        public virtual Work work { get; set; }
        public virtual User user { get; set; }
    }
}