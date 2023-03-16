// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PurchaseTransactions;
using PurchaseTransactionService = TrickyBookStore.Services.PurchaseTransactions.PurchaseTransactionService;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddScoped<ICustomerService, CustomerService>();
    services.AddScoped<IPurchaseTransactionService, PurchaseTransactionService>();
    services.AddScoped<IBookService, BookService>();
    services.AddScoped<IPaymentService, PaymentService>();
});