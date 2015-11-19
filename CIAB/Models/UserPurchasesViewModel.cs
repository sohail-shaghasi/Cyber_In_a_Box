using System;
namespace CIAB.Models
{
    public class UserPurchasesViewModel
    {
        public int? OrderID { get; set; }
        public int UserId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string UserEmail { get; set; }
        public string UserCompany { get; set; }
        public string UserContactNumber { get; set; }
        public string ProductName { get; set; }
        public string FileName1 { get; set; }
        public string FileName2 { get; set; }
        public string FileName3 { get; set; }
    }
}