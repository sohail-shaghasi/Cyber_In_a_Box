using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CIAB.DataLayer
{
    public class BaseDataLayer
    {
        public string CIABconnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString; }
        }



        protected string ByteArrayToString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length * 2);

            foreach (byte b in byteArray)
                hex.AppendFormat("{0:x2}", b);


            return hex.ToString();
        }
    }
}