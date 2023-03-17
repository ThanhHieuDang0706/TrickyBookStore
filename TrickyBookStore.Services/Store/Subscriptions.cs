using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Store
{
    public static class Subscriptions
    {
        public static readonly IEnumerable<Subscription> Data = new List<Subscription>
        {
            new Subscription { Id = 1, SubscriptionType = SubscriptionTypes.Paid, Priority = 2,
                PriceDetails = new Dictionary<string, double>
                {
                    { "DiscountNewBook", 0.95 },
                    { "DiscountOldBook", 0.05 }
                },
                NumOfNewBookDiscount = 3,
                Fee = 50
            },
            new Subscription { Id = 2, SubscriptionType = SubscriptionTypes.Free, Priority = 1,
                PriceDetails = new Dictionary<string, double>
                {
                    { "DiscountNewBook", 1 },
                    { "DiscountOldBook", 0.9 },
                },
                Fee = 0
            },
            new Subscription { Id = 3, SubscriptionType = SubscriptionTypes.Premium, Priority = 4,
                PriceDetails = new Dictionary<string, double>
                {
                    { "DiscountNewBook", 0.75 },
                    { "DiscountOldBook", 0 },
                },
                Fee = 200,
                NumOfNewBookDiscount = 3
            },
            new Subscription { Id = 4, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                {
                    { "DiscountNewBook", 0.75 },
                    { "DiscountOldBook", 0 },
                },
                NumOfNewBookDiscount = 3,
                Fee = 75
            },
            new Subscription { Id = 5, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                {
                    { "DiscountNewBook", 0.75 },
                    { "DiscountOldBook", 0 },
                },
                NumOfNewBookDiscount = 3,
                BookCategoryId = 1,
                Fee = 75
            },
            new Subscription { Id = 6, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                {
                    { "DiscountNewBook", 0.75 },
                    {"DiscountOldBook", 0 }
                },
                NumOfNewBookDiscount = 3,
                BookCategoryId = 3,
                Fee = 75
            },
            new Subscription { Id = 7, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                {
                    { "DiscountNewBook", 0.75 }, { "DiscountOldBook", 0 }
                },
                NumOfNewBookDiscount = 3,
                BookCategoryId = 2,
                Fee = 75
            },
            new Subscription { Id = 7, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                { { "DiscountNewBook", 0.75 }, { "DiscountOldBook", 0 } },
                NumOfNewBookDiscount = 3,
                BookCategoryId = 4,
                Fee = 75
            },
        };
    }
}
