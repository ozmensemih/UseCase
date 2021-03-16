
using Microsoft.AspNetCore.Authorization;
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
using UseCase.Business.Interfaces;
using UseCase.Common;
using UseCase.Common.Enums;
using UseCase.Data.Model;
using UseCase.DTO;

namespace UseCase.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Cashier")]
    public class CashierController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<Cashier> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<ApiRole> _rolesManager;
        private readonly ISubscriptionService _subscriptionService;


        public CashierController(
            SignInManager<User> signInManager,
            UserManager<Cashier> userManager,
            IConfiguration configuration,
            RoleManager<ApiRole> rolesManager,
            ISubscriptionService subscriptionService
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _rolesManager = rolesManager;
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse<LoginResultDto>> Login([FromBody] LoginDto loginDto)
        {
            var response = new ApiResponse<LoginResultDto>();
            Microsoft.AspNetCore.Identity.SignInResult result =
                await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);

            if (!result.Succeeded)
            {
                return response.ErrorResult(default, ResponseMessageEnum.NotFound, 404, "Kullanıcı adı hatalı");
            }

            Cashier appUserCashier = _userManager.Users.SingleOrDefault(r => r.UserName == loginDto.UserName);
            if (appUserCashier == null)
            {
                return response.ErrorResult(default, ResponseMessageEnum.UnAuthorized, 401, "Kullanıcı tipi hatalı");
            }

            var userRoles = _userManager.GetRolesAsync(appUserCashier).Result.ToList();

            string key1 = _configuration["Application:Secret"];
            string audience = _configuration["Application:JwtIssuer"];
            string issuer = _configuration["Application:JwtIssuer"];
            string expire = _configuration["Application:JwtExpireDays"];

            var resultDto = new LoginResultDto()
            {
                Token = CommonFactory.GetJwtToken(appUserCashier.Id.ToString(), key1, audience, issuer, expire,
                    appUserCashier.UserName, userRoles),
                Id = appUserCashier.Id,
                Name = appUserCashier.Name,
                LastName = appUserCashier.LastName
            };

            response.Result = resultDto;

            return response;

        }

        // ilk cashier kaydını oluşturmak için yazdım.
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse<bool>> Register([FromBody] CashierDto model)
        {
            var response = new ApiResponse<bool>();

            var appUserCashier = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
            if (appUserCashier != null)
            {
                return response.ErrorResult(false, ResponseMessageEnum.UserAttached, 200);
            }

            Cashier user = new Cashier
            {

                UserName = model.UserName,
                Name = model.Name,
                LastName = model.LastName,
                Address = model.Address,
                IsActive = true

            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return response.ErrorResult(false, ResponseMessageEnum.Exception, 500, "Kullanıcı eklenemedi");
            }

            var roleExist = await _rolesManager.RoleExistsAsync("Cashier");
            if (!roleExist)
            {
                var role = new ApiRole() { Name = "Cashier" };
                var roleResult = await _rolesManager.CreateAsync(role);
            }

            var newUserRole = await _userManager.AddToRoleAsync(user, "Cashier").ConfigureAwait(true);

            return response.SetResult(true);
        }

        [HttpPost]
        public async Task<ApiResponse<bool>> AddCustomer([FromBody] CustomerDto customerDto)
        {
            var response = _subscriptionService.AddCustomer(customerDto);
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<bool>> AddCorporation([FromBody] CorporationDto corporationDto)
        {
            var response = _subscriptionService.AddCorporation(corporationDto);
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerDto>> SearchCustomer([FromBody] SearchCustomerDto searchCustomerDto)
        {
            var response = _subscriptionService.SearchCustomer(searchCustomerDto);
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<CorporationDto>> SearchCorporation([FromBody] SearchCorporationDto searchCorporation)
        {
            var response = _subscriptionService.SearchCorporation(searchCorporation);
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<bool>> CloseSubscription([FromBody] Guid Id)
        {
            var response = _subscriptionService.CloseSubscription(Id);
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<bool>> DepositRefund([FromBody] Guid Id)
        {
            var response = _subscriptionService.DepositRefund(Id);
            return response;

        }
    }
}