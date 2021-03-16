
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using UseCase.Common;
using UseCase.Common.Enums;
using UseCase.Data.Model;
using UseCase.DTO;

namespace UseCase.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<ApiRole> _rolesManager;
        private readonly IConfiguration _configuration;

        public CustomerController(
            UserManager<Customer> userManager,
            SignInManager<User> signInManager,
            RoleManager<ApiRole> rolesManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rolesManager = rolesManager;
            _configuration = configuration;
        }



        [HttpPost]
        public async Task<ApiResponse<LoginResultDto>> Login([FromBody] LoginDto model)
        {
            var response = new ApiResponse<LoginResultDto>();
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (!result.Succeeded)
            {
                return response.ErrorResult(default, ResponseMessageEnum.NotFound, 404, "Kullanıcı bilgileri hatalı");
            }

            Customer customer = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
      

            if (customer == null)
            {
                return response.ErrorResult(default, ResponseMessageEnum.UnAuthorized, 401, "Kullanıcı tipi hatalı");
            }
            var userRoles = _userManager.GetRolesAsync(customer).Result.ToList();

            string key1 = _configuration["Application:Secret"];
            string audience = _configuration["Application:JwtIssuer"];
            string issuer = _configuration["Application:JwtIssuer"];
            string expire = _configuration["Application:JwtExpireDays"];

            var resultDto = new LoginResultDto()
            {
                Token = CommonFactory.GetJwtToken(customer.Id.ToString(), key1, audience, issuer, expire, customer.UserName, userRoles),
                Id = customer.Id,
                Name = customer.Name,
                LastName = customer.LastName
            };

            response.Result = resultDto;
            return response;
        }

    }
}