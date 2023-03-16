using TrickyBookStore.Services.Payment;

namespace ProgramEntry
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IPaymentService _paymentService;
        public Worker(ILogger<Worker> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("Year: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.WriteLine("Month: ");
                    int month = int.Parse(Console.ReadLine());
                    Console.WriteLine("Customer ID: ");
                    long customerId = long.Parse(Console.ReadLine());

                    _paymentService.GetPaymentAmount(customerId, new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero).AddMonths(1));
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                }
            }
        }
    }
}