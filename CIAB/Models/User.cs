using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CIAB.Models
{
    public class User
    {
        public LoginViewModel loginViewModel { get; set; }
        public RegisterViewModel registerViewModel { get; set; }
        ////--Login Properties Starts--
        //public int UserID { get; set; }
        //public string LoginUserName { get; set; }

        //public string LoginPassword { get; set; }
        //public string Role { get; set; }
        //public bool? cbPersist { get; set; }
        //public string LoginEmail { get; set; }
        ////--Login Properties Ends--
        ////--Registeration Properties Starts--
        //[RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = ("Please provide a single space between FirstName and LastName."))]
        //public string FullName { get; set; }
        //[System.Web.Mvc.Remote("IsUserNameAvailable", "Validation", ErrorMessage = "User name already exists. Please enter a different user name.")]
        //[StringLength(10, MinimumLength = 3, ErrorMessage = "User Name must be between 3 to 10 characters")]
        //[RegularExpression(@"(\S)+", ErrorMessage = "White space in User Name is not allowed.")]
        //public string UserName { get; set; }
        //[RegularExpression(@"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){8,20}", ErrorMessage = @"Error. Password must have one capital, one special character and one numerical character. It can not start with a special character or a digit.")]
        //public string Password { get; set; }
        //[Compare("Password", ErrorMessage = "The Password and Confirm Password do not match")]//to compare the password fields.
        //public string ConfirmPassword { get; set; }
        //[DataType(DataType.EmailAddress)]
        //[System.Web.Mvc.Remote("IsEmailAvailable", "Validation", ErrorMessage = "Email Address is in use. Please enter a different Email.")]
        //public string RegisterationEmail { get; set; }
        //public string HashPassword { get; set; }
    }

    public class LoginViewModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "UserName is Required")]
        public string LoginUserName { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string LoginPassword { get; set; }
        public string Role { get; set; }
        public bool? cbPersist { get; set; }
        public string LoginEmail { get; set; }
        public string LoginHashPassword { get; set; }
    }
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "FullName is Required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = ("Please provide a single space between FirstName and LastName."))]
        public string RegisterFullName { get; set; }
        [System.Web.Mvc.Remote("IsUserNameAvailable", "Validation", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "User Name must be between 3 to 10 characters")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space in User Name is not allowed.")]
        [Required(ErrorMessage = "UserName is Required")]
        public string RegisterUserName { get; set; }
        [RegularExpression(@"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){8,20}", ErrorMessage = @"Error. Password must have one capital, one special character and one numerical character. It can not start with a special character or a digit.")]
        [Required(ErrorMessage = "Password is Required")]
        public string RegisterPassword { get; set; }
        [Compare("RegisterPassword", ErrorMessage = "The Password and Confirm Password do not match")]//to compare the password fields.
        [Required(ErrorMessage = "ConfirmPassword is Required")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        [System.Web.Mvc.Remote("IsEmailAvailable", "Validation", ErrorMessage = "Email Address is in use. Please enter a different Email.")]
        [Required(ErrorMessage = "Email is Required")]
        public string RegisterEmail { get; set; }
        public string RegisterHashPassword { get; set; }
    }
}