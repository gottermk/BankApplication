using BankApplication.Models;
using BankApplication.Models.Accounts;
using System;
using System.Collections.Generic;

namespace BankApplication.Interfaces
{
    public interface IBank
    {
        AccountOwner GetOwner(string firstName, string lastName);
        IEnumerable<Account> GetAccounts<T>(Guid id) where T : Account;
        ITransactionStatus Deposit(Account account, decimal amount);
        ITransactionStatus Withdraw(Account account, decimal amount);
        ITransactionStatus Transfer(Account source, Account destination, decimal amount);
    }
}
