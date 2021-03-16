using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UseCase.DTO
{
    public class InvoicePaidDto
    {
        [Required]
        public Guid Id { get; set; }
        public bool PaymentSatatus { get; set; }

    }
}
