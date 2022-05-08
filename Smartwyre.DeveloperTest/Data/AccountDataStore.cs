using Smartwyre.DeveloperTest.Types;
using System;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// Accounts store implementation
    /// </summary>
    public class AccountDataStore : IAccountDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            using (var db = new SmartwyreDBContext())
            {
                //For the test to pass, kindly change the account number values each time you compile the code
                db.Account.Add(new Account { AccountNumber = "223213213299", Balance = Convert.ToDecimal(1000.00), Status = AccountStatus.Live, AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer });
                db.Account.Add(new Account { AccountNumber = "453454364544", Balance = Convert.ToDecimal(2000.00), Status = AccountStatus.Live, AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer });
                db.SaveChanges();

                var actQuery = (from d in db.Account
                                select d).FirstOrDefault(x => x.AccountNumber.Equals(accountNumber));
                return actQuery;

            }
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
            using (var db = new SmartwyreDBContext())
            {
                var actQuery = (from d in db.Account
                                select d).FirstOrDefault(x => x.AccountNumber.Equals(account.AccountNumber));
                actQuery.Balance = account.Balance;

                db.Account.Attach(actQuery);
                db.SaveChanges();
            }
        }
    }
}
