using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TTMS.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
		[Required]
		[Display(Name = "UserName")]

		public string UserName { get; set; }

		[Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
		[Required]
		[Display(Name = "UserRoles")]
		public string UserRoles { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[Display(Name = "UserName")]
		public string UserName { get; set; }

		[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Office")]
        public string Office { get; set; }

        [Required(ErrorMessage = "First Name is Required!")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last Name is Required!")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Address is Required!")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "City is Required!")]
        [Display(Name = "City")]
        public string City { get; set; }

        //[Required(ErrorMessage = "State is Required!")]
        [Display(Name = "State")]
        public string State { get; set; }

        //[Required(ErrorMessage = "Zip Code is Required!")]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        //[Required(ErrorMessage = "Country is Required!")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        //[Required(ErrorMessage = "Phone Number is Required!")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
    }

    public class AdminRegistationViewModel
    {
        [Required]
        [Display(Name = "UserRoles")]
        public string UserRoles { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Office")]
        public string Office { get; set; }

        [Required(ErrorMessage = "First Name is Required!")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last Name is Required!")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Address is Required!")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "City is Required!")]
        [Display(Name = "City")]
        public string City { get; set; }

        //[Required(ErrorMessage = "State is Required!")]
        [Display(Name = "State")]
        public string State { get; set; }

        //[Required(ErrorMessage = "Zip Code is Required!")]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        //[Required(ErrorMessage = "Country is Required!")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        //[Required(ErrorMessage = "Phone Number is Required!")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
