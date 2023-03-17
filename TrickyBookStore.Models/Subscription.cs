using System;
using System.Collections.Generic;
using System.Text;

namespace TrickyBookStore.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public SubscriptionTypes SubscriptionType { get; set; }
        public int Priority { get; set; }
        public int? BookCategoryId { get; set; }
        public double Fee { get; set; }
        public int? NumOfNewBookDiscount { get; set; } = 0;
        public Dictionary<string, double> PriceDetails { get; set; }
    }
}
