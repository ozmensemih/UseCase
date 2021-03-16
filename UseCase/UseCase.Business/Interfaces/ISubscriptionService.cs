using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UseCase.Common;
using UseCase.DTO;

namespace UseCase.Business.Interfaces
{
    public interface ISubscriptionService
    {
        bool UserPassive(Guid userId);
        ApiResponse<bool>  DepositRefund(Guid userId);
        bool UserDeposit(Guid userId);
        ApiResponse<bool> CloseSubscription(Guid userId);
        ApiResponse<CustomerDto> SearchCustomer(SearchCustomerDto searchCustomerDto);
        ApiResponse<CorporationDto> SearchCorporation(SearchCorporationDto searchCorporation);

        ApiResponse<bool> AddCorporation(CorporationDto corporationDto);
        ApiResponse<bool> AddCustomer(CustomerDto customerDto);

        

    }
}
