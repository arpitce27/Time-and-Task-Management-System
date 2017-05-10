using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTMS.Models
{
    public class OfficeInfo
    {
        [Key]
        public int PK_Office { get; set; }

        [Required(ErrorMessage = "Office Name is Required!")]
        [Display(Name = "Office Name")]
        [StringLength(200)]
        public string OfficeName { get; set; }

        [Required(ErrorMessage = "Description is Required!")]
        [Display(Name = "Description")]
        public string OfficeDescr { get; set; }

        [Required(ErrorMessage = "Address is Required!")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is Required!")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is Required!")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is Required!")]
        [Display(Name = "Zip Code")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Country is Required!")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone Number is Required!")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
    }
}