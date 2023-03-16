using ProgramEntry;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<IPurchaseTransactionService, PurchaseTransactionService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IPaymentService, PaymentService>();
    })
    .Build();


await host.RunAsync();
