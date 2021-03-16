using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UseCase.Common.Enums;

namespace UseCase.Data.Model
{
    public class Subscription : User
    {
      
        public decimal Deposit { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public SubscriptionType SubscriptionType  { get; set; }

}
}
