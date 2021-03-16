using System;
using System.Collections.Generic;
using System.Text;
using UseCase.Common;
using UseCase.Data.Model;
using UseCase.DTO;

namespace UseCase.Business.Interfaces
{
    public interface IInvoiceService
    {
        IEnumerable<InvoiceDto> GetUserIdInvoce(Guid userId);
        IEnumerable<InvoiceDto> GetUserIdInvoce(Guid userId, bool paymentStatus);
        ApiResponse<bool> Paid(Guid invoiceId, Guid userId,bool isCasheir);
        bool PaymentStatus(Guid userId);
    }
}
