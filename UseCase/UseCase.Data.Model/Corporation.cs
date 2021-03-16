using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UseCase.Data.Model
{
    public class Corporation : Subscription
    {
       
        public string TaxNumber { get; set; }
       
    }
}
