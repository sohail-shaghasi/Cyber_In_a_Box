using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace CIAB.Models
{
    public class AdminViewModel
    {
        [HiddenInput(DisplayValue = true)]
        public int? OrderID { get; set; }
        public int UserId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? OrderNumber { get; set; }
        [Required]
        [UIHint("Date")]
        public DateTime? OrderDate { get; set; }
        [Required]
        public string OrderStatus { get; set; }
        [UIHint("Email")]
        public string UserEmail { get; set; }
        [Required]
        public string UserCompany { get; set; }
        [Required]
        public string UserContactNumber { get; set; }
        [Required]
        ProductName productName = new ProductName();
        [UIHint("ProductsDropDownlist")]
        public ProductName ProductName
        { 
            get { return productName; } 
            set { productName = value;}
        }
        AdminOrderStatus adminOrderStatus = new AdminOrderStatus();
        [UIHint("adminViewStatus")]
        public AdminOrderStatus AdminOrderStatus
        {
            get { return adminOrderStatus; }
            set { adminOrderStatus = value; }
        }
    }
    public class AdminOrderStatus
    {
        public int Code { get; set; }
        public string StatusValue { get; set; }
    }
    public class ProductName
    {
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
    }
}