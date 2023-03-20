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
            var books = BookService.GetBooks(bookIds.ToArray()).ToDictionary(book => book.Id, book => book);

            IList<Subscription> subscriptions = SubscriptionService.GetSubscriptions(customer.SubscriptionIds.ToArray());
            double totalPurchasePayment = CalculateTotalPurchasePayment(subscriptions, purchaseTransactions, books);
            double subscriptionFees = subscriptions.Sum(subscription => subscription.Fee);
            return totalPurchasePayment + subscriptionFees;
        }

        private double CalculateTotalPurchasePayment(IList<Subscription> subscriptions, IList<PurchaseTransaction> purchaseTransactions, Dictionary<long, Book> books)
        {
            Dictionary<int, int> usedDiscountCount = new Dictionary<int, int>();
            foreach (var subscription in subscriptions)
            {
                usedDiscountCount.Add(subscription.Id, 0);
            }

            IDictionary<int?, Subscription>
                categorySubscriptionDictionary = subscriptions
                .Where(subscription => subscription.SubscriptionType == SubscriptionTypes.CategoryAddicted)
                .ToDictionary(subscription => subscription.BookCategoryId, subscription => subscription);

            double totalPayment = 0d;
            foreach (var transaction in purchaseTransactions)
            {
                Book book = books[transaction.BookId];
                if (book.IsOld)
                {
                    totalPayment = CalculateOldBookPayment(book, subscriptions);
                }
                else
                {
                    Subscription subscription = SelectAvailableSubscriptionForNewBook(subscriptions, categorySubscriptionDictionary, book, usedDiscountCount);
                    totalPayment += book.Price * subscription.DiscountNewBook;
                    usedDiscountCount[subscription.Id]++;
                }
            }
            return totalPayment;
        }

        private bool IsOldBookFree(IList<Subscription> subscriptions, Book book)
        {
            return subscriptions.Any(subscription =>
                subscription.SubscriptionType == SubscriptionTypes.Premium ||
                (subscription.SubscriptionType == SubscriptionTypes.CategoryAddicted && book.CategoryId == subscription.BookCategoryId));
        }

        private double CalculateOldBookPayment(Book book, IList<Subscription> subscriptions)
        {
            if (IsOldBookFree(subscriptions, book))
            {
                return 0d;
            }
            var paidSubscription = subscriptions.FirstOrDefault(subscription => subscription.SubscriptionType == SubscriptionTypes.Paid);
            if (paidSubscription != null)
            {
                return book.Price * paidSubscription.DiscountOldBook;
            }
            var freeSubscription = SubscriptionService.GetFreeSubscription();
            return book.Price * freeSubscription.DiscountOldBook;
        }

        private Subscription SelectAvailableSubscriptionForNewBook(IList<Subscription> subscriptions, IDictionary<int?, Subscription> categorySubscriptionDictionary, Book book,
            Dictionary<int, int> usedDiscountCount)
        {
            if (categorySubscriptionDictionary.ContainsKey(book.CategoryId))
            {
                Subscription subscription = categorySubscriptionDictionary[book.CategoryId];
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
