using System;

namespace BankApplication.Models.Accounts
{
    public abstract class Account
    {
        public Account(AccountOwner owner)
        {
            Id = Guid.NewGuid();
            Owner = owner;
            Balance = 0;
        }

        public Guid Id { get; }
        public AccountOwner Owner { get; }
        public decimal Balance { get; set; }

        public virtual decimal GetWithdrawLimit() => decimal.MaxValue;
    }
}
