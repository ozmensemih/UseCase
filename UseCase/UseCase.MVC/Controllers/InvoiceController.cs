using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCase.Business.Gateway;
using UseCase.Common;
using UseCase.DTO;

namespace UseCase.MVC.Controllers
{
    [Authorize(Roles = "Customer,Corporation,Cashier")]

    public class InvoiceController : BaseController
    {

        private readonly ApiGateway _buybackGateway;

        public InvoiceController(ApiGateway buybackGateway)
        {
            _buybackGateway = buybackGateway;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Paid(Guid id)
        {

            var resultPaid = _buybackGateway.Paid(UserData.Token, id);
            if (resultPaid.Result)
            {
                return RedirectToAction("Success", "Message");
            }

            TempData["ErrorMessage"] = resultPaid.Message;
            return RedirectToAction("Error", "Message");

        }

        [HttpGet]
        [Authorize(Roles = "Cashier")]
        public IActionResult UserInvoiceList(SerachtUserIdInvoiceDto model)
        {
            var resultInvoice = _buybackGateway.UserInvoiceList(UserData.Token, model);
            return View(resultInvoice.Result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult UserInvoiceListPaymentStatus(SerachtUserIdInvoiceDto model)
        {
           
                var resultInvoice = _buybackGateway.UserInvoiceListPaymentStatus(UserData.Token, model);
                return View("UserInvoiceList", resultInvoice.Result);
                       
        }

        [Authorize]
        public IActionResult Ekstre()
        {
            return View();
        }
    }
}
