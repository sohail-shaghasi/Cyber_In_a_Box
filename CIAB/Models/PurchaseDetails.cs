using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CIAB.Models
{
    public class PurchaseDetails
    {
        public PurchaseDetails()
        {

        }
        public int PurchaseID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Quantity { get; set; }

    }
}