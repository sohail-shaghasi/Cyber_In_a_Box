using NLog;
using System.Text;
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
        protected string ByteArrayToString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length * 2);

            foreach (byte b in byteArray)
                hex.AppendFormat("{0:x2}", b);


            return hex.ToString();
        }
        protected string CIABconnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString; }
        }
	}
}