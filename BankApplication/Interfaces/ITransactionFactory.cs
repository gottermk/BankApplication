using BankApplication.Models.Accounts;
using BankApplication.Models.Transactions;

namespace BankApplication.Interfaces
{
    public interface ITransactionFactory
    {
        Transaction CreateDepositTransaction(Account account, decimal amount);
        Transaction CreateWithdrawTransaction(Account account, decimal amount);
        Transaction CreateTransferFromTransaction(Account account, decimal amount);
        Transaction CreateTransferToTransaction(Account account, decimal amount);
    }
}
