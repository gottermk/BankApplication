using BankApplication.Factories;
using BankApplication.Interfaces;
using BankApplication.Models;
using BankApplication.Models.Accounts;
using BankApplication.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApplication
{
    public class Bank : IBank
    {
        private readonly ITransactionFactory _transactionFactory;
        private readonly ICollection<Account> _accounts;

        public Bank(string name, ICollection<Account> accounts)
        {
            _transactionFactory = new TransactionFactory();
            _accounts = accounts;
            Name = name;
        }

        public string Name { get; }

        public AccountOwner GetOwner(string firstName, string lastName)
        {
            var owners = _accounts.Select(x => x.Owner).Distinct();

            return owners.FirstOrDefault(x => x.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                x.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Account> GetAccounts<T>(Guid ownerId) where T : Account =>
            _accounts.Where(x => x.Owner.Id == ownerId && x is T);

        public ITransactionStatus Deposit(Account account, decimal amount) =>
            _transactionFactory.CreateDepositTransaction(account, amount).Process();

        public ITransactionStatus Withdraw(Account account, decimal amount) =>
            _transactionFactory.CreateWithdrawTransaction(account, amount).Process();

        public ITransactionStatus Transfer(Account source, Account destination, decimal amount)
        {
            var status = _transactionFactory.CreateTransferFromTransaction(source, amount).Process();

            return (status is Approved) ? _transactionFactory.CreateTransferToTransaction(destination, amount).Process() : status;
        }
    }
}
