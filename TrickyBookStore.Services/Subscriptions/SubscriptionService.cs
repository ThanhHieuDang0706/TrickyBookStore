using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Customers;

namespace TrickyBookStore.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        private ICustomerService CustomerService;

        public SubscriptionService(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        public IList<Subscription> GetSubscriptions(params int[] ids)
        {
            List<Subscription> subscriptions = Store.Subscriptions.Data.Where(subscription => ids.Contains(subscription.Id)).ToList();
            return subscriptions;
        }
    }
}
