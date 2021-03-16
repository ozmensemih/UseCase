using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UseCase.Data.Model
{
    public class Invoice:BaseEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Subscription Subscription { get; set; }
        public  Guid? CategoryId { get; set; }
        public string InvoiceName { get; set; }
        public decimal InvoicePrice { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? InvoiceExpiryDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool PaymentStatus { get; set; }


    }
}
