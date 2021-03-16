using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UseCase.Common.Enums;

namespace UseCase.DTO
{
    public class SerachtUserIdInvoiceDto
    {
        [Required]
        public Guid Id { get; set; }
        public bool PaymentStatus { get; set; }
   }
}
