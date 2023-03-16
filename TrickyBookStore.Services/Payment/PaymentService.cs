using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private ICustomerService CustomerService { get; }
        private ISubscriptionService SubscriptionService { get; }
        private IPurchaseTransactionService PurchaseTransactionService { get; }

        struct GroupedTransactionsById
        {
            public long CustomerId;
            public List<PurchaseTransaction> Transactions;
        }

        public PaymentService(ICustomerService customerService, IPurchaseTransactionService purchaseTransactionService, ISubscriptionService subscriptionService)
        {
            CustomerService = customerService;
            SubscriptionService = subscriptionService;
            PurchaseTransactionService = purchaseTransactionService;
        }

        public double GetPaymentAmountOfPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            IList<PurchaseTransaction> purchaseTransactions =
                PurchaseTransactionService.GetPurchaseTransactions(customerId, fromDate, toDate);

            // group by customer id and map it to the transactions related to the customer id
            var groupedPurchaseTransactions = purchaseTransactions.GroupBy(
                               transaction => transaction.CustomerId,
                                              transaction => transaction,
                                              (key, transactions) => new GroupedTransactionsById() { CustomerId = key, Transactions = transactions.ToList() }).ToList();

            double totalPayment = groupedPurchaseTransactions.Aggregate(0d, CalculatePaymentOfGroupedTransactions);
            return totalPayment;
        }

        private double CalculatePaymentOfGroupedTransactions(double total, GroupedTransactionsById currentGroupedTransaction)
        {
            double paymentAmount = 0;
            long customerId = currentGroupedTransaction.CustomerId;
            Customer customer = CustomerService.GetCustomerById(customerId);


            foreach (var purchaseTransaction in currentGroupedTransaction.Transactions)
            {
                // go through each transaction, check if it is a subscription or a book purchase
            }
            return total + paymentAmount;
        }
    }
}
