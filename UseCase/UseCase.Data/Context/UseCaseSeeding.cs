using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UseCase.Common.Enums;
using UseCase.Data.Model;

namespace UseCase.Data.Context
{
    public static class SeedData
    {
        public static IHost SeedAdminUser(this IHost webHost)
        {
            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                try
                {
                    UseCaseContext context = scope.ServiceProvider.GetRequiredService<UseCaseContext>();
                    context.Database.EnsureCreated();

                    UserManager<Cashier> userManagerCashier = scope.ServiceProvider.GetRequiredService<UserManager<Cashier>>();
                    UserManager<Customer> userManagerCustomer = scope.ServiceProvider.GetRequiredService<UserManager<Customer>>();
                    UserManager<Corporation> userManagerCorporation = scope.ServiceProvider.GetRequiredService<UserManager<Corporation>>();

                    RoleManager<ApiRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApiRole>>();

                    if (!userManagerCashier.Users.Any())
                    {
                        roleManager.CreateAsync(new ApiRole(){ Name = "Cashier"}).Wait();
                      

                        Cashier user = new Cashier()
                        {
                            UserName = "semihcashier",
                            Name = "Semih",
                            LastName = "Özmen",
                            Address = "istanbul",
                            IsActive = true,
                        };

                        System.Threading.Tasks.Task<IdentityResult> data = userManagerCashier.CreateAsync(user, "1-234567");
                        data.Wait();
                        
                        userManagerCashier.AddToRoleAsync(user, "Cashier").Wait();
                  
                    }

                    if (!userManagerCustomer.Users.Any())
                    {
                       
                        roleManager.CreateAsync(new ApiRole() { Name = "Customer" }).Wait();
                      

                        Customer user = new Customer()
                        {
                            UserName = "semihcustomer1",
                            Name = "Semih",
                            LastName = "Customer",
                            Address = "istanbul",
                            Deposit = 100,
                            SubscriptionType = SubscriptionType.Customer,
                            IdentityNumber = "12345678999",
                            IsActive = true,
                        };

                        System.Threading.Tasks.Task<IdentityResult> data = userManagerCustomer.CreateAsync(user, "1-234567");
                        data.Wait();

                        userManagerCustomer.AddToRoleAsync(user, "Customer").Wait();

                        Invoice invoice1 = new Invoice()
                        {
                            UserId = user.Id,
                            InvoiceName = "ARALIK 2020",
                            InvoicePrice = 50,
                            InvoiceDate = new DateTime(2020, 12, 31, 23, 59, 59),
                            InvoiceExpiryDate = new DateTime(2021, 01, 20, 23, 59, 59),
                            PaymentDate = new DateTime(2021, 03, 16, 18, 34, 02),
                            PaymentStatus = true,
                        };



                        Invoice invoice2 = new Invoice()
                        {
                            UserId = user.Id,
                            InvoiceName = "OCAK 2021",
                            InvoicePrice = 50,
                            InvoiceDate = new DateTime(2021, 01, 31, 23, 59, 59),
                            InvoiceExpiryDate = new DateTime(2021, 02, 20, 23, 59, 59),
                            PaymentStatus = false,
                        };



                        Invoice invoice3 = new Invoice()
                        {
                            UserId = user.Id,
                            InvoiceName = "ŞUBAT 2021",
                            InvoicePrice = 50,
                            InvoiceDate = new DateTime(2021, 02, 28, 23, 59, 59),
                            InvoiceExpiryDate = new DateTime(2021, 03, 20, 23, 59, 59),
                            PaymentStatus = false,
                        };



                        Invoice invoice4 = new Invoice()
                        {
                            UserId = user.Id,
                            InvoiceName = "ŞUBAT 2021",
                            InvoicePrice = 50,
                            InvoiceDate = new DateTime(2021, 03, 30, 23, 59, 59),

                            PaymentStatus = false,
                        };

                        context.Invoices.Add(invoice1);
                        context.Invoices.Add(invoice2);
                        context.Invoices.Add(invoice3);
                        context.Invoices.Add(invoice4);


                    }

                    if (!userManagerCorporation.Users.Any())
                    {

                        roleManager.CreateAsync(new ApiRole() { Name = "Corporation" }).Wait();

                        Corporation user = new Corporation()
                        {
                            UserName = "semihcorporation",
                            Name = "Semih",
                            LastName = "Corporation",
                            Address = "istanbul",
                            Deposit = 100,
                            SubscriptionType = SubscriptionType.Corporation,
                            TaxNumber = "9114175381",
                            IsActive = true,
                        };

                        System.Threading.Tasks.Task<IdentityResult> data = userManagerCorporation.CreateAsync(user, "1-234567");
                        data.Wait();

                        userManagerCorporation.AddToRoleAsync(user, "Corporation").Wait();


                        Corporation user2 = new Corporation()
                        {
                            UserName = "semih2corporation",
                            Name = "Semih",
                            LastName = "Corporation2",
                            Address = "istanbul",
                            Deposit = 100,
                            SubscriptionType = SubscriptionType.Corporation,
                            TaxNumber = "7354524251",
                            IsActive = true,
                        };

                        System.Threading.Tasks.Task<IdentityResult> data2 = userManagerCorporation.CreateAsync(user2, "1-234567");
                        data.Wait();

                        userManagerCorporation.AddToRoleAsync(user2, "Corporation").Wait();

                        Invoice invoice1 = new Invoice()
                        {
                            UserId = user.Id,
                            InvoiceName = "ARALIK 2020",
                            InvoicePrice = 50,
                            InvoiceDate =new DateTime(2020,12,31,23,59,59),
                            InvoiceExpiryDate = new DateTime(2021, 01, 20, 23, 59, 59),
                            PaymentDate = new DateTime(2021, 03, 16, 18, 32, 00),
                            PaymentStatus = true,
                        };

                     

                        Invoice invoice2 = new Invoice()
                        {
                            UserId = user.Id,
                            InvoiceName = "OCAK 2021",
                            InvoicePrice = 50,
                            InvoiceDate = new DateTime(2021, 01, 31, 23, 59, 59),
                            InvoiceExpiryDate = new DateTime(2021, 02, 20, 23, 59, 59),
                            PaymentStatus = false,
                        };

                        

                        Invoice invoice3 = new Invoice()
                        {
                            UserId = user.Id,
                            InvoiceName = "ŞUBAT 2021",
                            InvoicePrice = 50,
                            InvoiceDate = new DateTime(2021, 02, 28, 23, 59, 59),
                            InvoiceExpiryDate = new DateTime(2021, 03, 20, 23, 59, 59),
                            PaymentStatus = false,
                        };

                      

                        Invoice invoice4 = new Invoice()
                        {
                            UserId = user.Id,
                            InvoiceName = "MART 2021",
                            InvoicePrice = 50,
                            InvoiceDate = new DateTime(2021, 03, 30, 23, 59, 59),
                            PaymentStatus = false,
                        };

                        context.Invoices.Add(invoice1);
                        context.Invoices.Add(invoice2);
                        context.Invoices.Add(invoice3);
                        context.Invoices.Add(invoice4);



                        Invoice invoice5 = new Invoice()
                        {
                            UserId = user2.Id,
                            InvoiceName = "ŞUBAT 2021",
                            InvoicePrice = 50,
                            InvoiceDate = new DateTime(2021, 02, 28, 23, 59, 59),
                            InvoiceExpiryDate = new DateTime(2021, 03, 20, 23, 59, 59),
                            PaymentStatus = false,
                        };

                        Invoice invoice6 = new Invoice()
                        {
                            UserId = user2.Id,
                            InvoiceName = "MART 2021",
                            InvoicePrice = 50,
                            InvoiceDate = new DateTime(2021, 02, 28, 23, 59, 59),
                            PaymentStatus = false,
                        };


                        context.Invoices.Add(invoice5);
                        context.Invoices.Add(invoice6);
                    }

                   
                }
                catch (Exception ex)
                {

                    //throw;
                }
            }

            return webHost;
        }
    }

}
