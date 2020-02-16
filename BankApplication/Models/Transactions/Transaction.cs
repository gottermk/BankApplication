using BankApplication.Interfaces;
using BankApplication.Models.Accounts;
using System;

namespace BankApplication.Models.Transactions
{
    public abstract class Transaction
    {
        protected readonly Account _account;
        protected readonly decimal _amount;

        public Transaction(Account account, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Transactions require a positive amount");
            }

            _account = account;
            _amount = amount;
        }

        public abstract ITransactionStatus Process();
    }
}
