using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIAB.Models
{
    public class OrderDetailsPage
    {

        public int  OrderId { get; set; }
        public string  Email { get; set; }
        public string CompanyName { get; set; }
        public string  ProductName { get; set; }



        //------------properties for upload control
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileDestinationPath { get; set; }

    }
}