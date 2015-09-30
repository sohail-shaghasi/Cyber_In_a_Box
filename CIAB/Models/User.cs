using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;


namespace CIAB.Models
{
    public class User
    {
        //Reading Connection string from Web.Config File 
        string CIABconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;

        public User()
        {

        }
        //------------------------------------------------------------------------
        //Properties that Represent columns of the Database Table.
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string email { get; set; }
        public string HashPassword { get; set; }

        public bool? cbPersist { get; set; }






        //------------------------------------------------------------------------


        /// <summary>
        /// Check if the User Exists in the Database.
        /// </summary>
        /// <param name="_UserName"></param>
        /// <param name="_Password"></param>
        /// <returns>True if User Exists and Password is Correct</returns>
        public bool IsValid(string _UserName, string _Password)
        {


            bool isValid = false;


            try
            {

                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_Login";




                //apply Hashing on password.

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
                    email = reader["Email"].ToString();

                    reader.Dispose();
                    CIABcommand.Dispose();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
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