using BankApplication.Interfaces;
using System;

namespace BankApplication.Models.Transactions
{
    public class Declined : ITransactionStatus
    {
        private readonly Guid _accountId;
        private readonly decimal _amount;

        public Declined(Guid accountId, decimal amount)
        {
            _accountId = accountId;
            _amount = amount;
        }

        public void ViewTransaction()
        {
            Console.WriteLine($"Declined: {_accountId.ToString()} for {_amount.ToString("C2")}");
        }
    }
}
