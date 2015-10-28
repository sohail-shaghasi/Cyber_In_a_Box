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

    public class Products
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProducImage { get; set; }
        public int ProductQuantity { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ProductDescription { get; set; }

    }
}
