using CIAB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CIAB.DataLayer
{
    public class HomeDataLayer : BaseDataLayer
    {
        public void ContactUs(string inputName, string Subject, string inputEmail, string Message, string optProduct, string body)
        {
            //SMTP parameters starts here
            string strReciever = System.Configuration.ConfigurationManager.AppSettings["smtpReciever"];
            string strEmailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpEmailFrom"];
            string strSubject = System.Configuration.ConfigurationManager.AppSettings["smtpSubject"];
            string strSMTPUser = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
            string strSMPTpass = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
            string strSMPTHost = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
            int SMPTPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);

            //check if the parameters are supplied from web.config file.
            if (strReciever != null || strReciever != string.Empty || strEmailFrom != null || strEmailFrom != string.Empty || strSubject != string.Empty ||  strSubject != null 
                || strSMPTpass != null || strSMPTpass != string.Empty || strSMPTHost != string.Empty || strSMPTHost!= null || strSMPTHost != string.Empty)
            { 
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(strEmailFrom);
                    message.To.Add(inputEmail);
                    message.Subject = strSubject; //Subject;
                    message.Body = string.Format(body, inputEmail, Subject, inputName, optProduct, Message);
                    message.IsBodyHtml = true;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.BodyEncoding = Encoding.UTF8;

                    // credebtials for smtp client account
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = strSMTPUser,
                            Password = strSMPTpass
                        };
                        smtp.Credentials = credential;
                        smtp.Host = strSMPTHost;
                        smtp.Port = SMPTPort;
                        smtp.EnableSsl = true;

                        smtp.Send(message);
                    }
                }
            }


        }

        public int RegisterUser(User NewUser)
        {
            string strFullName = NewUser.registerViewModel.RegisterFullName;
            string strUserName = NewUser.registerViewModel.RegisterUserName;
            string strEmail = NewUser.registerViewModel.RegisterEmail;
            string strPass = NewUser.registerViewModel.RegisterPassword;
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "spRegisterUser";
            CIABcommand.Parameters.AddWithValue("@Fullname", SqlDbType.NVarChar).Value = strFullName.ToString();//parameter for Full Name
            CIABcommand.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = strUserName.ToString();// parameter for UserName
            CIABcommand.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = strEmail.ToString();//parameter for Email
            Encoding encoder = new UTF8Encoding();
            SHA1 sha = new SHA1Managed();
            byte[] passwordHash = sha.ComputeHash(encoder.GetBytes(strPass + "d3katk00"));
            string hashPass = ByteArrayToString(passwordHash);
            CIABcommand.Parameters.AddWithValue("@Pass", SqlDbType.NVarChar).Value = hashPass.ToString();// parameter for pass
            return Convert.ToInt32(CIABcommand.ExecuteScalar());
        }

        public UserProfileEdit GetUserProfile(string userName, string password)
        {
            var result = new UserProfileEdit();

            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_UpdateProfile";
            //Input Parameters for Stored Procedure
            CIABcommand.Parameters.AddWithValue("@Parameter", "select");
            CIABcommand.Parameters.AddWithValue("@UserName", userName);
            CIABcommand.Parameters.AddWithValue("@Password", password);
            SqlDataReader reader = CIABcommand.ExecuteReader();
            while (reader.Read())
            {
                // Output Parameters from stored Procedure
                result.email = reader["Email"].ToString();
                result.FullName = reader["FullName"].ToString();
                result.Website = reader["Website"].ToString();
                result.CompanyName = reader["CompanyName"].ToString();
                result.CompanyAddress = reader["CompanyAddress"].ToString();
            }
            return result;
        }


        public bool IsValid(User user)
        {
            SqlConnection CIABconnection = new SqlConnection(base.CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_Login";

            //Login Parameters
            CIABcommand.Parameters.AddWithValue("@UserName", user.loginViewModel.LoginUserName);//parameter for User Name
            CIABcommand.Parameters.AddWithValue("@Password", user.loginViewModel.LoginHashPassword);// parameter for Password
            SqlDataReader reader = CIABcommand.ExecuteReader();


            while (reader.Read())
            {
                user.loginViewModel.UserID = (int)reader["UserID"];
                user.loginViewModel.LoginEmail = reader["Email"].ToString();
                user.loginViewModel.Role = reader["Role"].ToString();

                reader.Dispose();
                CIABcommand.Dispose();
                return true;
            }

            return false;

        }
    }
}