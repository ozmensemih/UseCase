using System;
using System.Collections.Generic;
using System.Text;
using UseCase.Business.Interfaces;
using UseCase.Data.UnitOfWork;
using System.Linq;
using UseCase.Common;
using UseCase.Common.Enums;
using System.Threading.Tasks;
using UseCase.DTO;
using Microsoft.AspNetCore.Identity;
using UseCase.Data.Model;

namespace UseCase.Business.Services
{
    public class SubscriptionManeger:ISubscriptionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceService _invoiceService;
        private readonly UserManager<Customer> _customerManager;
        private readonly UserManager<Corporation> _corporationManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<ApiRole> _rolesManager;


        public SubscriptionManeger(
            IUnitOfWork unitOfWork, IInvoiceService invoiceService,
            UserManager<Customer> customerManager,
            UserManager<Corporation> corporationManager,
            UserManager<User> userManager,
        RoleManager<ApiRole> rolesManager
        )
        {
            _unitOfWork = unitOfWork;
            _invoiceService = invoiceService;
            _customerManager = customerManager;
            _corporationManager = corporationManager;
            _userManager = userManager;
            _rolesManager = rolesManager;
        }

        public ApiResponse<bool> DepositRefund(Guid userId)
        {
            var depositRefund = _unitOfWork.Invoces.Get(p => p.Subscription.Id == userId).FirstOrDefault();

            ApiResponse<bool> response = new ApiResponse<bool>();

            if (depositRefund == null)
            {
                return response.ErrorResult(false, ResponseMessageEnum.SubscriptionNotFound, 404);
            }

            if (_invoiceService.PaymentStatus(userId))
            {
                return response.ErrorResult(false, ResponseMessageEnum.InvoiceStatusError, 400);
            }

             depositRefund.Subscription.Deposit = 0;
             _unitOfWork.Invoces.Update(depositRefund);
             _unitOfWork.Save();
             response.SetResult(true);

            return response;

        }

        public bool UserPassive(Guid userId)
        {
            var userPassive = _unitOfWork.Users.Get(p => p.Id == userId && p.IsActive).FirstOrDefault();

            if (userPassive != null)
            {
                userPassive.IsActive = false;
                _unitOfWork.Users.Update(userPassive);

                var subscriptionUpDate= _unitOfWork.Invoces.Get(p=>p.UserId==userId).FirstOrDefault();
                subscriptionUpDate.Subscription.SubscriptionEndDate=DateTime.Now;
                
                _unitOfWork.Invoces.Update(subscriptionUpDate);

                _unitOfWork.Save();

                
                return true;
            }

            return false;

        }

        public bool UserDeposit(Guid userId)
        {
            bool invoice = _unitOfWork.Invoces.Get(p => p.Subscription.Id == userId && p.Subscription.Deposit > 0).Any();
        
            return invoice;
        }

        public ApiResponse<bool> CloseSubscription(Guid userId)
        {

            ApiResponse<bool> response = new ApiResponse<bool>();

            if (_invoiceService.PaymentStatus(userId))
            {
                return response.ErrorResult(false, ResponseMessageEnum.InvoiceStatusError, 451);
            }

            if (UserDeposit(userId))
            {
                return response.ErrorResult(false, ResponseMessageEnum.UserDepositError, 451);
            }
            if (!UserPassive(userId))
            {
                return response.ErrorResult(false, ResponseMessageEnum.SubscriptionNotFound, 404);
            }
            response.SetResult(true);
            return response;
        }

        public ApiResponse<CorporationDto> SearchCorporation(SearchCorporationDto searchCorporation)
        {
            var response = new ApiResponse<CorporationDto>();

            CorporationDto appUserCorporation = _corporationManager.Users.Select(
                s => new CorporationDto()
                {
                    Id = s.Id,
                    TaxNumber = s.TaxNumber,
                    Name = s.Name,
                    LastName = s.LastName,
                    Address = s.Address,
                    Deposit = s.Deposit,
                    IsActive = s.IsActive

                }).SingleOrDefault(r => r.TaxNumber == searchCorporation.TaxNumber);


            if (appUserCorporation == null)
            {
                return response.ErrorResult(default, ResponseMessageEnum.SubscriptionNotFound, 404);
            }

            return response.SetResult(appUserCorporation);
        }

        public ApiResponse<CustomerDto> SearchCustomer(SearchCustomerDto searchCustomerDto)
        {
            var response = new ApiResponse<CustomerDto>();

            CustomerDto appUserCustomer = _customerManager.Users.Select(
                s => new CustomerDto()
                {
                    Id = s.Id,
                    IdentityNumber = s.IdentityNumber,
                    Name = s.Name,
                    LastName = s.LastName,
                    Address = s.Address,
                    Deposit = s.Deposit,
                    IsActive = s.IsActive

                }).SingleOrDefault(r => r.IdentityNumber == searchCustomerDto.IdentityNumber);


            if (appUserCustomer == null)
            {
                return response.ErrorResult(default, ResponseMessageEnum.SubscriptionNotFound, 404);
            }

            return response.SetResult(appUserCustomer);
        }

        public ApiResponse<bool> AddCorporation(CorporationDto corporationDto)
        {
            var response = new ApiResponse<bool>();
            if (UserControl(corporationDto.UserName))
            {
                return response.ErrorResult(false, ResponseMessageEnum.UserDepositError, 409);
            }

            Corporation appUserCorporation =
                _corporationManager.Users.SingleOrDefault(r => r.TaxNumber == corporationDto.TaxNumber);


            if (appUserCorporation != null)
            {
                return response.ErrorResult(false, ResponseMessageEnum.UserIsAttached, 452);
            }

            Corporation user = new Corporation
            {
                TaxNumber = corporationDto.TaxNumber,
                UserName = corporationDto.UserName,
                Name = corporationDto.Name,
                LastName = corporationDto.LastName,
                Address = corporationDto.Address,
                Deposit = corporationDto.Deposit,
                IsActive = true,
                SubscriptionType = SubscriptionType.Corporation,
                SubscriptionStartDate=DateTime.Now

            };

            IdentityResult result =  _corporationManager.CreateAsync(user, corporationDto.Password).Result;

            if (!result.Succeeded)
            {
                return response.ErrorResult(false, ResponseMessageEnum.Exception, 500);

            }

            var roleExist =  _rolesManager.RoleExistsAsync("Corporation").Result;
            if (!roleExist)
            {
                var role = new ApiRole() { Name = "Corporation" };
                var roleResult =  _rolesManager.CreateAsync(role);
            }

            var newUserRole =  _corporationManager.AddToRoleAsync(user, "Corporation").Result;

            return response.SetResult(true);
        }

        public ApiResponse<bool> AddCustomer(CustomerDto customerDto)
        {
            var response = new ApiResponse<bool>();

            if (UserControl(customerDto.UserName))
            {
                return response.ErrorResult(false, ResponseMessageEnum.UserDepositError, 409);
            }
            Customer appUserCustomer = _customerManager.Users.SingleOrDefault(r => r.IdentityNumber == customerDto.IdentityNumber);

            if (appUserCustomer != null)
            {
                return response.ErrorResult(false, ResponseMessageEnum.UserIsAttached, 452);
            }

            Customer user = new Customer
            {
                IdentityNumber = customerDto.IdentityNumber,
                UserName = customerDto.UserName,
                Name = customerDto.Name,
                LastName = customerDto.LastName,
                Address = customerDto.Address,
                Deposit = customerDto.Deposit,
                IsActive = true,
                SubscriptionType = SubscriptionType.Customer,
                SubscriptionStartDate = DateTime.Now

            };

            IdentityResult result =  _customerManager.CreateAsync(user, customerDto.Password).Result;

            if (!result.Succeeded)
            {
                return response.ErrorResult(false, ResponseMessageEnum.Exception, 500);
            }

            var roleExist =  _rolesManager.RoleExistsAsync("Customer").Result;
            if (!roleExist)
            {
                var role = new ApiRole() { Name = "Customer" };
                var roleResult =  _rolesManager.CreateAsync(role).Result;
            }

            var newUserRole = _customerManager.AddToRoleAsync(user, "Cashier").Result;

            return response.SetResult(true);
        }

        private bool UserControl(string userName)
        {
            bool userControl = _userManager.Users.Any(p => p.UserName == userName);
            return userControl;
        }


    }
}
