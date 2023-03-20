using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;

namespace TrickyBookStore.Services.PurchaseTransactions
{
    public class PurchaseTransactionService : IPurchaseTransactionService
    {
        public PurchaseTransactionService()
        {
         
        }

        public IList<PurchaseTransaction> GetPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var purchaseTransactions = Store.PurchaseTransactionStore.Data.
                Where(
                    transaction => 
                    transaction.CustomerId == customerId && 
                    transaction.CreatedDate >= fromDate && 
                    transaction.CreatedDate < toDate)
                .OrderBy(transaction => transaction.CreatedDate)
                .ToList();

            return purchaseTransactions;
        }
    }
}
