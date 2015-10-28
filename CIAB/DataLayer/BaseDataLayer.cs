using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIAB.DataLayer
{
    public class BaseDataLayer
    {
        public string CIABconnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString; }
        }
    }
}