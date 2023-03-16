using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.PurchaseTransactions;

namespace TrickyBookStore.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private ICustomerService CustomerService { get; }
        private IPurchaseTransactionService PurchaseTransactionService { get; }

        public PaymentService(ICustomerService customerService, 
            IPurchaseTransactionService purchaseTransactionService)
        {
            CustomerService = customerService;
            PurchaseTransactionService = purchaseTransactionService;
        }

        public double GetPaymentAmount(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            IList<PurchaseTransaction> purchaseTransactions =
                PurchaseTransactionService.GetPurchaseTransactions(customerId, fromDate, toDate);

            throw new NotImplementedException();
        }
    }
}
