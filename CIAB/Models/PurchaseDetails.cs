using System;
namespace CIAB.Models
{
    public class PurchaseDetails
    {
        public int PurchaseID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Quantity { get; set; }
    }
}