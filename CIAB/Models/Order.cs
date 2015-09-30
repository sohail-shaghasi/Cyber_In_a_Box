using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIAB.Models
{
    public class Order
    {

        public Order()
        {
                
        }


        public int OrderID{get; set;}
        public int Quantity{get; set;}
        public double UnitPrice{get; set;}
        public int OrderNumber{get; set;}
        public DateTimeOffset? OrderDate{get; set;}
        public int ProductID{ get; set;}
        public int CustomerID{ get; set; }

        public double subTotal
        {
            get
            {
                return Quantity * UnitPrice;
            }
            
        }



    }
}