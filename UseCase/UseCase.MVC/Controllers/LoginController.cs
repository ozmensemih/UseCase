using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCase.Business.Gateway;
using UseCase.Common.Enums;
using UseCase.MVC.App_Start;
using UseCase.MVC.Models;

namespace UseCase.MVC.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ApiGateway _buybackGateway;

        public LoginController(ApiGateway buybackGateway)
        {
            _buybackGateway = buybackGateway;

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tokenResult = _buybackGateway.GetToken(model.UserName, model.Password, model.SubscriptionType);

                // var addUser = _buybackGateway.AddCustomer(model.UserName, model.Password);
                if (tokenResult.IsError || String.IsNullOrWhiteSpace(tokenResult.Result.Token))
                {
                    ModelState.AddModelError(string.Empty, tokenResult.Message);
                    return View();
                }

                AuthBuilder authBuilder = new AuthBuilder();
                UserLoginData userData = new UserLoginData()
                {
                    Token = tokenResult.Result.Token,
                    Id = tokenResult.Result.Id,
                    FirstName = tokenResult.Result.Name,
                    LastName = tokenResult.Result.LastName
                };
                List<string> roles = new List<string>();

                if (model.SubscriptionType == SubscriptionType.Customer)
                {
                    roles.Add("Customer");
                }
                else if (model.SubscriptionType == SubscriptionType.Corporation)
                {
                    roles.Add("Corporation");
                }

                authBuilder.SignInAsync(HttpContext, userData, roles).ConfigureAwait(true);

              return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            AuthBuilder authBuilder = new AuthBuilder();
            await authBuilder.SignOutAsync(HttpContext);

            return RedirectToAction("Index", "Login");
        }
    }
}
