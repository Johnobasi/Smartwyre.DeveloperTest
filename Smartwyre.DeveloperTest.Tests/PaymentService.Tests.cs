using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    /// <summary>
    /// Test class
    /// </summary>
    public class PaymentServiceTests
    {

        [Fact]
        public void MakeUserPayment()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IPaymentService, PaymentService>();
            IServiceProvider serviceProvider = collection.BuildServiceProvider();
            var _paymentService = serviceProvider.GetService<IPaymentService>();

            MakePaymentRequest makePaymentRequest = new()
            {
                CreditorAccountNumber = "223213213235",
                DebtorAccountNumber = "453454364535",
                Amount = Convert.ToDecimal(25.75),
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.BankToBankTransfer
            };

            var feedBackResult = _paymentService.MakePayment(makePaymentRequest);

            MakePaymentResult makePaymentResult = new()
            {
                Success = true
            };

            //Assert  
            Assert.Equal(makePaymentResult.Success, feedBackResult.Success);

            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }

        }
    }
}
