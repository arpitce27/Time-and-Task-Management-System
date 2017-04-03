using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTMS.Models
{
    public class WorkViewModel
    {
        [Required]
        [StringLength(200, ErrorMessage = "The title must be at least 6 characters long.", MinimumLength = 6)]
        [Display(Name = "Title")]
        public String WorkTitle { get; set; }

        [Required]
        [Display(Name = "Work Type")]
        public WorkType WorkType { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public WorkPriority WorkPriority { get; set; }

        [Required]
        [StringLength(400, ErrorMessage = "The title must be at least 10 characters long.", MinimumLength = 10)]
        [Display(Name = "Title")]
        public String WorkDescr { get; set; }

        [Required(ErrorMessage = "Please enter a deadline")]
        [Display(Name = "Deadline")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Deadline { get; set; }


    }
}