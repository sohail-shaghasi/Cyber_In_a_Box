using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CIAB.Controllers
{
    public class BaseController : Controller
    {
        protected Logger Logger { get; private set; }

        public BaseController()
        {
            Logger = LogManager.GetLogger(GetType().FullName);
        }


        /// <summary>
        /// This function is to convert byte returned from password to hexa decimal.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        protected string ByteArrayToString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length * 2);

            foreach (byte b in byteArray)
                hex.AppendFormat("{0:x2}", b);


            return hex.ToString();
        }
        //temporary need to move data functions in to data layer
        protected string CIABconnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString; }
        }
	}
}