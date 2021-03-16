
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UseCase.Common;
using UseCase.Common.Enums;
using UseCase.Data.Model;
using UseCase.DTO;

namespace UseCase.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CorporationController : ControllerBase
    {
    
        private readonly UserManager<Corporation> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<ApiRole> _rolesManager;
        private readonly IConfiguration _configuration;

        public CorporationController(
            UserManager<Corporation> userManager,
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

            Corporation corporation = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
            
            if (corporation == null)
            {
                return response.ErrorResult(default, ResponseMessageEnum.UnAuthorized, 401,"Kullanıcı tipi hatalı");
            }

            var userRoles = _userManager.GetRolesAsync(corporation).Result.ToList();

            string key1 = _configuration["Application:Secret"];
            string audience = _configuration["Application:JwtIssuer"];
            string issuer = _configuration["Application:JwtIssuer"];
            string expire = _configuration["Application:JwtExpireDays"];


            var resultDto = new LoginResultDto()
            {
                Token = CommonFactory.GetJwtToken(corporation.Id.ToString(), key1, audience, issuer, expire, corporation.UserName, userRoles),
                Id = corporation.Id,
                Name = corporation.Name,
                LastName = corporation.LastName
            };

            response.Result = resultDto;
            return response;
        }

    }
}