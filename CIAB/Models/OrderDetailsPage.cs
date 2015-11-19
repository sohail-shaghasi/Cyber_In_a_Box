namespace CIAB.Models
{
    public class OrderDetailsPage
    {
        public int  OrderId { get; set; }
        public string  Email { get; set; }
        public string CompanyName { get; set; }
        public string  ProductName { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileDestinationPath { get; set; }
    }
}