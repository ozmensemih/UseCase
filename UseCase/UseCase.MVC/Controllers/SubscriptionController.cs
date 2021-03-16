using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCase.Business.Gateway;
using UseCase.DTO;
using UseCase.MVC.Models;

using UseCase.Common.Enums;
using UseCase.Common;

namespace UseCase.MVC.Controllers
{
    [Authorize(Roles = "Cashier")]
    public class SubscriptionController : BaseController
    {
        private readonly ApiGateway _buybackGateway;

        public SubscriptionController(ApiGateway buybackGateway)
        {
            _buybackGateway = buybackGateway;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Customer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Customer(SearchCustomerDto model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApiResponse<CustomerDto> resultCustomer = _buybackGateway.SearchCustomer(UserData.Token, model);

            if (resultCustomer.IsError)
            {
                ModelState.AddModelError(string.Empty, resultCustomer.Message);
                return View(model);
            }

            ViewBag.Customer = resultCustomer.Result;
            return View(model);

        }

        public IActionResult Corporation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Corporation(SearchCorporationDto model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApiResponse<CorporationDto> resultCorporation = _buybackGateway.SearchCorporation(UserData.Token, model);

            if (resultCorporation.IsError)
            {
                ModelState.AddModelError(string.Empty, resultCorporation.Message);
                return View(model);
            }

            ViewBag.Corporation = resultCorporation.Result;
            return View(model);



        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerDto model)
        {
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }
                
            var addCustomer = _buybackGateway.AddSubscriptionCustomer(UserData.Token, model);

                if (addCustomer.IsError)
                {
                    ModelState.AddModelError(string.Empty, addCustomer.Message);
                    return View();
                }
                return RedirectToAction("Success", "Message");

        }

        public IActionResult AddCorportion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCorportion(CorporationDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var addUCorportion = _buybackGateway.AddSubscriptionCorportion(UserData.Token, model);

                if (addUCorportion.IsError)
                {
                    ModelState.AddModelError(string.Empty, addUCorportion.Message);
                    return View();
                }
                return RedirectToAction("Success", "Message");

        }

        public IActionResult Close (Guid Id)
        {
            var closeSubscription = _buybackGateway.CloseSubscription(UserData.Token, Id);

            if (closeSubscription.IsError)
            {
                TempData["ErrorMessage"] = closeSubscription.Message;
                return RedirectToAction("Error", "Message");
            }

            return RedirectToAction("Success", "Message");
        }

        public IActionResult DepositRefund(Guid Id)
        {
            var depositRefund = _buybackGateway.DepositRefund(UserData.Token, Id);

            if (depositRefund.IsError)
            {
                TempData["ErrorMessage"] = depositRefund.Message;
                return RedirectToAction("Error", "Message");

            }
            return RedirectToAction("Success", "Message");
        }
    }
}
