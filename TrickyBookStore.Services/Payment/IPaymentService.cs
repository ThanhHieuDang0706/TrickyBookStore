﻿using System;
using System.Runtime.CompilerServices;
using TrickyBookStore.Services.Books;

// KeepIt
namespace TrickyBookStore.Services.Payment
{
    public interface IPaymentService
    {
        
        double GetPaymentAmountOfPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate);
    }
}
