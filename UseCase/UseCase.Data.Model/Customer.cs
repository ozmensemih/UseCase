using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UseCase.Data.Model;

namespace UseCase.Data.Model
{
    public class Customer : Subscription
    {
        public  string IdentityNumber { get; set; }
  
    }
}
