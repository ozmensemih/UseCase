using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UseCase.Common.Enums;

namespace UseCase.DTO
{
    public class SubscriptionDto
    {
        [Required]
        public string IdentityNumber { get; set; }
        [Required]
        public string TaxNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public SubscriptionType SubscriptionType { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public  decimal Deposit { get; set; }
      
    }
}
