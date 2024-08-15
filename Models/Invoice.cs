namespace comprobantes_back.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public DateTime? DeliveryDate {  get; set; }
        public decimal Total {  get; set; } 
        public decimal Deposit {  get; set; } 
        public decimal Balance {  get; set; } 
        public int? JobId {  get; set; }
        public string? Job {  get; set; }
        public string Status { get; set; }
    }
}
