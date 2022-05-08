using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Configuration;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IAccountDataStore, AccountDataStore>();
            IServiceProvider serviceProvider = collection.BuildServiceProvider();
            var _accountDataStore = serviceProvider.GetService<IAccountDataStore>();

            Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

            var result = new MakePaymentResult();

            switch (request.PaymentScheme)
            {
                case PaymentScheme.BankToBankTransfer:
                    if (account == null)
                    {
                        result.Success = false;
                    }
                    else if (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer))
                    {
                        result.Success = true;
                    }
                    break;

                case PaymentScheme.ExpeditedPayments:
                    if (account == null)
                    {
                        result.Success = false;
                    }
                    else if (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments))
                    {
                        result.Success = true;
                    }
                    else if (account.Balance < request.Amount)
                    {
                        result.Success = false;
                    }
                    break;

                case PaymentScheme.AutomatedPaymentSystem:
                    if (account == null)
                    {
                        result.Success = false;
                    }
                    else if (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem))
                    {
                        result.Success = true;
                    }
                    else if (account.Status != AccountStatus.Live)
                    {
                        result.Success = false;
                    }
                    break;
            }

            if (result.Success)
            {
                account.Balance -= request.Amount;
                _accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
