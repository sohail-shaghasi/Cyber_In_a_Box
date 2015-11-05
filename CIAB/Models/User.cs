using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;


namespace CIAB.Models
{
    public class User
    {
        public User()
        {

        }

        //--Login Properties Starts--
        public int UserID { get; set; }
        public string LoginUserName { get; set; }
        public string LoginPassword { get; set; }
        public string Role { get; set; }
        public bool? cbPersist { get; set; }
        public string LoginEmail { get; set; }

        //--Login Properties Ends--


        //--Registeration Properties Starts--
        public string FullName { get; set; }


        //HttpMethod = "POST"
        [System.Web.Mvc.Remote("IsUserNameAvailable", "Validation", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "User Name must be between 3 to 10 characters")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space in User Name is not allowed.")]
        public string UserName { get; set; }


        [RegularExpression(@"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){8,20}", ErrorMessage = @"Error. Password must have one capital, one special character and one numerical character. It can not start with a special character or a digit.")]
       // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = " Please input minimum 8 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character:")]
        
       // [StringLength(20, MinimumLength = 8, ErrorMessage ="8 characters minimum, no spaces, no special characters")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match")]//to compare the password fields.
        public string ConfirmPassword { get; set; }

        [System.Web.Mvc.Remote("IsEmailAvailable", "Validation", ErrorMessage = "Email Address is in use. Please enter a different Email.")]
        public string RegisterationEmail { get; set; }
        
        public string HashPassword { get; set; }
        //--Registeration Properties Ends-----
    }
}