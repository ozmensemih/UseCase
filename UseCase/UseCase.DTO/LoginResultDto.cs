using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCase.DTO
{
    public class LoginResultDto
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
      
    }
}
