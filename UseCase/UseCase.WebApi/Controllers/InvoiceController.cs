using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.FlowAnalysis;
using UseCase.Business.Interfaces;
using UseCase.Common;
using UseCase.Common.Enums;
using UseCase.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UseCase.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        private readonly IInvoiceService _invoceService;

        public InvoiceController(IInvoiceService invoceService)
        {
            _invoceService = invoceService;
        }

        [Authorize(Roles = "Cashier")]
        public ActionResult<ApiResponse<List<InvoiceDto>>> GetUserIdInvoce(Guid id)
        {
            var response = new ApiResponse<List<InvoiceDto>>();
            List<InvoiceDto> result = _invoceService.GetUserIdInvoce(id).ToList();
            if (result.Count < 0)
            {
                return response.ErrorResult(default, ResponseMessageEnum.NotFound, 404);
            }

            response.Result = result;

            return response;
        }

        [Authorize]
        public ActionResult<ApiResponse<List<InvoiceDto>>> GetUserInvoiceListPaymentStatus(Guid id, bool paymentStatus = false)
        {
            var response = new ApiResponse<List<InvoiceDto>>();
            List<InvoiceDto> result = _invoceService.GetUserIdInvoce(id, paymentStatus).ToList();
            if (result.Count < 0)
            {
                return response.ErrorResult(default, ResponseMessageEnum.NotFound, 404, "Fatura bulunamadı.");
            }

            response.Result = result;

            return response;
        }
        [HttpPost]

        [Authorize]
        public ActionResult<ApiResponse<bool>> Paid([FromBody] Guid id)
        {
            bool isCashier = false;
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (User.IsInRole("Cashier"))
            {
                isCashier = true;
            }
            ApiResponse<bool> result = _invoceService.Paid(id, userId, isCashier);
            return result;

        }


    }
}
