using ProgramEntry;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ICustomerService, CustomerService>();
        services.AddSingleton<ISubscriptionService, SubscriptionService>();
        services.AddSingleton<IPurchaseTransactionService, PurchaseTransactionService>();
        services.AddSingleton<IBookService, BookService>();
        services.AddSingleton<IPaymentService, PaymentService>();
        services.AddHostedService<Worker>();
    })
    .Build();


await host.RunAsync();
