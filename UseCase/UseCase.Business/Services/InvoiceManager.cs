using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UseCase.Business.Interfaces;
using UseCase.Common;
using UseCase.Common.Enums;
using UseCase.Data.Model;
using UseCase.Data.UnitOfWork;
using UseCase.DTO;


namespace UseCase.Business.Services
{
    public class InvoiceManager : IInvoiceService
    {

        private readonly IUnitOfWork _unitOfWork;

        public InvoiceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<InvoiceDto> GetUserIdInvoce(Guid userId)
        {
            IQueryable<InvoiceDto> result = _unitOfWork.Invoces.Get(p => p.UserId == userId).Select(
                s => new InvoiceDto()
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    InvoiceName = s.InvoiceName,
                    InvoiceDate = s.InvoiceDate,
                    InvoiceExpiryDate = s.InvoiceExpiryDate,
                    InvoicePrice = s.InvoicePrice,
                    PaymentDate = s.PaymentDate,
                    PaymentStatus = s.PaymentStatus,
                    PaymentExpired = (
                        s.InvoiceExpiryDate < DateTime.Now && s.PaymentStatus == false
                            ? "gecikti"
                            : s.InvoiceDate.Month == DateTime.Now.Month && s.PaymentStatus == false
                                ? "guncel"
                                : s.InvoiceExpiryDate >= DateTime.Now && s.InvoiceExpiryDate != null &&
                                  s.PaymentStatus == false
                                    ? "odemeyap"
                                    : "odendi"
                    )
                }).OrderByDescending(p=>p.InvoiceDate);

            return result;
        }

        public IEnumerable<InvoiceDto> GetUserIdInvoce(Guid userId, bool paymentStatus)
        {
            IQueryable<InvoiceDto> result = _unitOfWork.Invoces.Get(p => p.UserId == userId && p.PaymentStatus == paymentStatus).Select(
                s => new InvoiceDto()
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    InvoiceName = s.InvoiceName,
                    InvoiceDate = s.InvoiceDate,
                    InvoiceExpiryDate = s.InvoiceExpiryDate,
                    InvoicePrice = s.InvoicePrice,
                    PaymentDate = s.PaymentDate,
                    PaymentStatus = s.PaymentStatus,
                    PaymentExpired = (
                        s.InvoiceExpiryDate < DateTime.Now && s.PaymentStatus == false
                            ? "gecikti"
                            : s.InvoiceDate.Month == DateTime.Now.Month && s.PaymentStatus == false
                                ? "guncel"
                                : s.InvoiceExpiryDate >= DateTime.Now && s.InvoiceExpiryDate != null &&
                                  s.PaymentStatus == false
                                    ? "odemeyap"
                                    : "odendi"
                    )
                }).OrderByDescending(p => p.InvoiceDate);

            return result;
        }

        public ApiResponse<bool> Paid(Guid invoiceId, Guid userId,bool isCasheir)
        {

            ApiResponse<bool> response = new ApiResponse<bool>();

            var invoice = _unitOfWork.Invoces.Get(p => p.Id == invoiceId && p.InvoiceDate <= DateTime.Now && p.PaymentStatus == false).FirstOrDefault();

            if (invoice == null)
            {
                return response.ErrorResult(false, ResponseMessageEnum.NotFound, 404, "Ödenecek fatura bulunamadı");
            }

            if ((invoice.UserId != userId && !isCasheir))
            {
                return response.ErrorResult(false, ResponseMessageEnum.UnAuthorized, 401);
            }
        
            invoice.PaymentStatus = true;
            invoice.PaymentDate = DateTime.Now;

            _unitOfWork.Invoces.Update(invoice);
            _unitOfWork.Save();

            return response.SetResult(true);
        }

       
        public bool PaymentStatus(Guid userId)
        {
            var invoices = _unitOfWork.Invoces.Get(p => p.UserId == userId && p.PaymentStatus == false && p.InvoiceDate.Month != DateTime.Now.Month).ToList();

            if (invoices.Count > 0)
            {
                return true;
            }

            return false;
        }

    }
}
