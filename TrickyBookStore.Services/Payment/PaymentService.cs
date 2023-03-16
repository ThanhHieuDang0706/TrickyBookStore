using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        struct GroupedTransactionsById
        {
            public long CustomerId;
            public List<PurchaseTransaction> Transactions;
        }

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

            foreach (var purchaseTransaction in currentGroupedTransaction.Transactions)
            {

            }
            return total + paymentAmount;
        }
    }
}
