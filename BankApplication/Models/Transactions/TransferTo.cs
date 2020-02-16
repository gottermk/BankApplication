using BankApplication.Interfaces;
using BankApplication.Models.Accounts;

namespace BankApplication.Models.Transactions
{
    public class TransferTo : Transaction
    {
        public TransferTo(Account account, decimal amount) : base(account, amount)
        {
        }

        public override ITransactionStatus Process()
        {
            _account.Balance += _amount;

            return new Approved(_account.Id, _amount);
        }
    }
}
