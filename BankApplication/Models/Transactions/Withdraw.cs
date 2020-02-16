using BankApplication.Interfaces;
using BankApplication.Models.Accounts;

namespace BankApplication.Models.Transactions
{
    public class Withdraw : Transaction
    {
        public Withdraw(Account account, decimal amount) : base(account, amount)
        {
        }

        public override ITransactionStatus Process()
        {
            if (_account.Balance >= _amount && _amount <= _account.GetWithdrawLimit())
            {
                _account.Balance -= _amount;

                return new Approved(_account.Id, _amount);
            }
            else
            {
                return new Declined(_account.Id, _amount);
            }
        }
    }
}
