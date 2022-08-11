using System;
namespace ORC.workshop.Models
{
    public enum Status_type
    {
        Draft, Submit, Pending, BookingComplete, CustomerConfirm, Cancel
    }
    public class OrderModel
    {
        public int BookingId { get; set; }
        public string DeliveryDateTime { get; set; }
        public string DeliveryDate { get; set; }
        public string CastingMethod { get; set; }
        public string Tel { get; set; }
        public string ContactName { get; set; }
        public int Quantity { get; set; }
        public string BookingCode { get; set; }
        public string MoreDetail { get; set; }
        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        public int PlantId { get; set; }
        public int SiteId { get; set; }
        public int Quotation { get; set; }
        public int QC { get; set; }
        public Status_type Status { get; set; }
    }
}
