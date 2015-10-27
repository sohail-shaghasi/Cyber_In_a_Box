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
        string CIABconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;

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

        [StringLength(10, MinimumLength = 5, ErrorMessage ="5 characters minimum, no spaces, no special characters")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match")]//to compare the password fields.
        public string ConfirmPassword { get; set; }

        [System.Web.Mvc.Remote("IsEmailAvailable", "Validation", ErrorMessage = "Email Address is in use. Please enter a different Email.")]
        public string RegisterationEmail { get; set; }
        
        public string HashPassword { get; set; }
        //--Registeration Properties Ends-----


        /// <summary>
        /// Check if the User Exists in the Database.
        /// </summary>
        /// <param name="_UserName"></param>
        /// <param name="_Password"></param>
        /// <returns>True if User Exists and Password is Correct</returns>
        public bool IsValid(string _UserName, string _Password)
        {
            try
            {

                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_Login";

                Encoding encoder = new UTF8Encoding();
                SHA1 sha = new SHA1Managed();
                byte[] passwordHash = sha.ComputeHash(encoder.GetBytes(_Password + "d3katk00"));
                string hashPass = ByteArrayToString(passwordHash);
                HashPassword = hashPass;


                //Login Parameters
                CIABcommand.Parameters.AddWithValue("@UserName", _UserName);//parameter for User Name
                CIABcommand.Parameters.AddWithValue("@Password", hashPass);// parameter for Password
                SqlDataReader reader = CIABcommand.ExecuteReader();


                while (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    LoginEmail = reader["Email"].ToString();
                    Role = reader["Role"].ToString();

                    reader.Dispose();
                    CIABcommand.Dispose();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }


        }

        //------------------------------------------------------------------------

        /// <summary>
        /// This function is to convert byte returned from password to hexa decimal.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] byteArray)
        {


            StringBuilder hex = new StringBuilder(byteArray.Length * 2);

            foreach (byte b in byteArray)
                hex.AppendFormat("{0:x2}", b);


            return hex.ToString();
        }
    }
}