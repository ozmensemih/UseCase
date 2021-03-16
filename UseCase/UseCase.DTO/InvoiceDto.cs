using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase.DTO
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string InvoiceName { get; set; }
        public decimal InvoicePrice { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? InvoiceExpiryDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool PaymentStatus { get; set; }
        public string PaymentExpired { get; set; }
    }
}
