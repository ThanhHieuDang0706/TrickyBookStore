using ProgramEntry;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<ISubscriptionService, SubscriptionService>();
        services.AddTransient<IPurchaseTransactionService, PurchaseTransactionService>();
        services.AddTransient<IBookService, BookService>();
        services.AddSingleton<IPaymentService, PaymentService>();
        services.AddHostedService<Worker>();
    })
    .Build();


await host.RunAsync();
