using BankApplication.Interfaces;
using BankApplication.Models.Accounts;
using BankApplication.Models.Transactions;

namespace BankApplication.Factories
{
    public class TransactionFactory : ITransactionFactory
    {
        public Transaction CreateDepositTransaction(Account account, decimal amount)
        {
            return new Deposit(account, amount);
        }

        public Transaction CreateWithdrawTransaction(Account account, decimal amount)
        {
            return new Withdraw(account, amount);
        }

        public Transaction CreateTransferFromTransaction(Account account, decimal amount)
        {
            return new TransferFrom(account, amount);
        }

        public Transaction CreateTransferToTransaction(Account account, decimal amount)
        {
            return new TransferTo(account, amount);
        }
    }
}
