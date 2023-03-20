using System;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private ISubscriptionService SubscriptionService { get; }

        public CustomerService(ISubscriptionService subscriptionService)
        {
            SubscriptionService = subscriptionService;
        }

        public Customer GetCustomerById(long id)
        {
            Customer targetCustomer = Store.Customers.Data.FirstOrDefault(customer => customer.Id == id);
            if (targetCustomer == null)
            {
                throw new ArgumentException($"Customer with id {id} not found");
            }
            return targetCustomer;
        }
    }
}
