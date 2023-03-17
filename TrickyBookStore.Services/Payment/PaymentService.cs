using System;
using System.Collections.Generic;
using System.Linq;
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
        private IBookService BookService { get; }
        private IPurchaseTransactionService PurchaseTransactionService { get; }

        public PaymentService(ICustomerService customerService, IPurchaseTransactionService purchaseTransactionService, ISubscriptionService subscriptionService, IBookService bookService)
        {
            CustomerService = customerService;
            SubscriptionService = subscriptionService;
            PurchaseTransactionService = purchaseTransactionService;
            BookService = bookService;
        }

        public double GetPaymentAmountOfPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            IList<PurchaseTransaction> purchaseTransactions =
                PurchaseTransactionService.GetPurchaseTransactions(customerId, fromDate, toDate);

            Customer customer = CustomerService.GetCustomerById(customerId);
            var bookIds = purchaseTransactions.Select(transaction => transaction.BookId);
            var books = BookService.GetBooks(bookIds.ToArray()).ToLookup(book => book.Id, book => book);

            IList<Subscription> subscriptions = SubscriptionService.GetSubscriptions(customer.SubscriptionIds.ToArray());
            Dictionary<int, int> usedDiscountCount = new Dictionary<int, int>();
            foreach (var subscription in subscriptions)
            {
                usedDiscountCount.Add(subscription.Id, 0);
            }

            var categorySubscriptions = subscriptions
                .Where(subscription => subscription.SubscriptionType == SubscriptionTypes.CategoryAddicted)
                .ToLookup(s => s.BookCategoryId, s => s);
            var premiumSubscription = subscriptions.FirstOrDefault(subscription => subscription.SubscriptionType == SubscriptionTypes.Premium);
            var paidSubscription = subscriptions.FirstOrDefault(subscription => subscription.SubscriptionType == SubscriptionTypes.Paid);
            var freeSubscription = SubscriptionService.GetFreeSubscription();

            double totalPayment = 0d;
            foreach (var transaction in purchaseTransactions)
            {
                Book book = books[transaction.BookId].First();
                if (book.IsOld)
                {
                    if (!categorySubscriptions.Contains(book.CategoryId) || premiumSubscription == null)
                    {
                        if (paidSubscription != null)
                        {
                            totalPayment += book.Price * paidSubscription.PriceDetails["DiscountOldBook"];
                        }
                        else
                        {
                            totalPayment += book.Price * freeSubscription.PriceDetails["DiscountOldBook"];
                        }
                    }
                }
                else
                {
                    Subscription subscription =
                        SelectAvailableSubscriptionForNewBook(subscriptions, categorySubscriptions, book, usedDiscountCount);
                    if (subscription.SubscriptionType == SubscriptionTypes.Free)
                    {
                        totalPayment += book.Price * subscription.PriceDetails["DiscountNewBook"];
                    }
                    else
                    {
                        totalPayment += book.Price * subscription.PriceDetails["DiscountNewBook"];
                        usedDiscountCount[subscription.Id]++;
                    }
                }
            }
            double subscriptionFees = subscriptions.Sum(subscription => subscription.Fee);
            return totalPayment + subscriptionFees;
        }

        private Subscription SelectAvailableSubscriptionForNewBook(IList<Subscription> subscriptions, ILookup<int?, Subscription> categorySubscriptionLookup, Book book,
            Dictionary<int, int> usedDiscountCount)
        {
            if (categorySubscriptionLookup.Contains(book.CategoryId))
            {
                Subscription subscription = categorySubscriptionLookup[book.CategoryId].First();
                if (subscription.NumOfNewBookDiscount > usedDiscountCount[subscription.Id])
                {
                    return subscription;
                }
            }

            if (subscriptions.Any(subscription => subscription.SubscriptionType == SubscriptionTypes.Premium))
            {
                Subscription premiumSubscription = subscriptions.First(subscription => subscription.SubscriptionType == SubscriptionTypes.Premium);
                if (premiumSubscription.NumOfNewBookDiscount > usedDiscountCount[premiumSubscription.Id])
                {
                    return premiumSubscription;
                }
            }

            if (subscriptions.Any(subscription => subscription.SubscriptionType == SubscriptionTypes.Paid))
            {
                Subscription paidSubscription = subscriptions.First(subscription => subscription.SubscriptionType == SubscriptionTypes.Paid);
                if (paidSubscription.NumOfNewBookDiscount > usedDiscountCount[paidSubscription.Id])
                {
                    return paidSubscription;
                }
            }
            return SubscriptionService.GetFreeSubscription();
        }

    }
}
