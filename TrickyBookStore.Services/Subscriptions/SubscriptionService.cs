using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        public SubscriptionService()
        {
            
        }

        public IList<Subscription> GetSubscriptions(params int[] ids)
        {
            List<Subscription> subscriptions = Store.Subscriptions.Data
                .Where(subscription => ids.Contains(subscription.Id))
                .ToList();
            return subscriptions;
        }

        public Subscription GetFreeSubscription()
        {
            return Store.Subscriptions.Data.FirstOrDefault(subscription => subscription.SubscriptionType == SubscriptionTypes.Free);
        }
    }
}
