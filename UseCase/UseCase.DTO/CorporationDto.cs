using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UseCase.Common.Enums;

namespace UseCase.DTO
{
    public class CorporationDto
    {
        public  Guid Id { get; set; }
        [Required]
        public string TaxNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public decimal Deposit { get; set; }
        public bool IsActive { get; set; }
        public SubscriptionType SubscriptionType { get; set; }

    }
}
