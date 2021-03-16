using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCase.DTO
{
    public class CashierDto
    {
       
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

    }
}
