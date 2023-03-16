using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Store
{
    public static class Subscriptions
    {
        public static readonly IEnumerable<Subscription> Data = new List<Subscription>
        {
            new Subscription { Id = 1, SubscriptionType = SubscriptionTypes.Paid, Priority = ...,
                PriceDetails = new Dictionary<string, double>
                {
                   
                }
            },
            new Subscription { Id = 2, SubscriptionType = SubscriptionTypes.Free, Priority = ...,
                PriceDetails = new Dictionary<string, double>
                {

                }
            },
            new Subscription { Id = 3, SubscriptionType = SubscriptionTypes.Premium, Priority = ...,
                PriceDetails = new Dictionary<string, double>
                {
                   
                }
            },
            new Subscription { Id = 4, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = ...,
                PriceDetails = new Dictionary<string, double>
                {
                  
                }              
            },
            new Subscription { Id = 5, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = ...,
                PriceDetails = new Dictionary<string, double>
                {
                    { "FixPrice", 75 },
                    ....
                },
                BookCategoryId = 1
            },
            new Subscription { Id = 6, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = ...,
                PriceDetails = new Dictionary<string, double>
                {
                    { "FixPrice", 75 },
                    ....
                },
                BookCategoryId = 3
            }
        };
    }
}
