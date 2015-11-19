using System.ComponentModel.DataAnnotations;
namespace CIAB.Models
{
    public class ChangePasswordFromUserDetailsPage
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailFromDB { get; set; }
        public string UniqueID { get; set; }
        [StringLength(10, MinimumLength = 5, ErrorMessage = "5 characters minimum, no spaces, no special characters")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match")]//to compare the password fields.
        public string ConfirmPassword { get; set; }
    }
}