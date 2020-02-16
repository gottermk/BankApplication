using BankApplication;
using BankApplication.Interfaces;
using BankApplication.Models;
using BankApplication.Models.Accounts;
using BankApplication.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BankApplicationTests
{
    public class BankTests
    {
        private readonly IBank _bank;

        public BankTests()
        {
            var owners = new List<AccountOwner>()
            {
                new AccountOwner("Emily", "Rodgers")
            };

            var accounts = new List<Account>()
            {
                new Checking(owners.ElementAt(0)),
                new IndividualInvestment(owners.ElementAt(0)),
                new CorporateInvestment(owners.ElementAt(0))
            };

            _bank = new Bank("Test Bank", accounts);
        }

        [Fact]
        public void Deposit_Precondition_Failed()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var checking = _bank.GetAccounts<Checking>(owner.Id).ElementAt(0);

            try
            {
                var deposit = _bank.Deposit(checking, -100);

                Assert.Null(deposit);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentException>(e);
                Assert.Contains(e.Message, "Transactions require a positive amount");
            }
        }

        [Fact]
        public void Deposit_Approved()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var checking = _bank.GetAccounts<Checking>(owner.Id).ElementAt(0);

            var deposit = _bank.Deposit(checking, 100);

            Assert.IsType<Approved>(deposit);
            Assert.Equal(100, checking.Balance);
        }

        [Fact]
        public void Withdraw_Precondition_Failed()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var checking = _bank.GetAccounts<Checking>(owner.Id).ElementAt(0);

            try
            {
                var withdraw = _bank.Withdraw(checking, -100);

                Assert.Null(withdraw);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentException>(e);
                Assert.Contains(e.Message, "Transactions require a positive amount");
            }
        }

        [Fact]
        public void Withdraw_Overdraft_Declined()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var checking = _bank.GetAccounts<Checking>(owner.Id).ElementAt(0);

            var withdraw = _bank.Withdraw(checking, 100);

            Assert.IsType<Declined>(withdraw);
            Assert.Equal(0, checking.Balance);
        }

        [Fact]
        public void Withdraw_Limit_Declined()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var individual = _bank.GetAccounts<IndividualInvestment>(owner.Id).ElementAt(0);

            var deposit = _bank.Deposit(individual, 1500);
            var withdraw = _bank.Withdraw(individual, 1100);

            Assert.IsType<Approved>(deposit);
            Assert.IsType<Declined>(withdraw);
            Assert.Equal(1500, individual.Balance);
        }

        [Fact]
        public void Withdraw_Approved()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var checking = _bank.GetAccounts<Checking>(owner.Id).ElementAt(0);

            var deposit = _bank.Deposit(checking, 1000);
            var withdraw = _bank.Withdraw(checking, 100);

            Assert.IsType<Approved>(deposit);
            Assert.IsType<Approved>(withdraw);
            Assert.Equal(900, checking.Balance);
        }

        [Fact]
        public void Withdraw_Limit_Approved()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var corporate = _bank.GetAccounts<CorporateInvestment>(owner.Id).ElementAt(0);

            var deposit = _bank.Deposit(corporate, 1500);
            var withdraw = _bank.Withdraw(corporate, 1100);

            Assert.IsType<Approved>(deposit);
            Assert.IsType<Approved>(withdraw);
            Assert.Equal(400, corporate.Balance);
        }

        [Fact]
        public void Transfer_Precondition_Failed()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var checking = _bank.GetAccounts<Checking>(owner.Id).ElementAt(0);
            var individual = _bank.GetAccounts<IndividualInvestment>(owner.Id).ElementAt(0);

            try
            {
                var transfer = _bank.Transfer(checking, individual, -100);

                Assert.Null(transfer);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentException>(e);
                Assert.Contains(e.Message, "Transactions require a positive amount");
            }
        }

        [Fact]
        public void Transfer_Overdraft_Declined()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var checking = _bank.GetAccounts<Checking>(owner.Id).ElementAt(0);
            var individual = _bank.GetAccounts<IndividualInvestment>(owner.Id).ElementAt(0);

            var transfer = _bank.Transfer(checking, individual, 100);

            Assert.IsType<Declined>(transfer);
            Assert.Equal(0, checking.Balance);
            Assert.Equal(0, individual.Balance);
        }

        [Fact]
        public void Transfer_Approved()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var checking = _bank.GetAccounts<Checking>(owner.Id).ElementAt(0);
            var individual = _bank.GetAccounts<IndividualInvestment>(owner.Id).ElementAt(0);

            var deposit = _bank.Deposit(checking, 1000);
            var transfer = _bank.Transfer(checking, individual, 100);

            Assert.IsType<Approved>(deposit);
            Assert.IsType<Approved>(transfer);
            Assert.Equal(900, checking.Balance);
            Assert.Equal(100, individual.Balance);
        }

        [Fact]
        public void Transfer_Limit_Approved()
        {
            var owner = _bank.GetOwner("Emily", "Rodgers");
            var checking = _bank.GetAccounts<Checking>(owner.Id).ElementAt(0);
            var individual = _bank.GetAccounts<IndividualInvestment>(owner.Id).ElementAt(0);

            var deposit = _bank.Deposit(checking, 1500);
            var transfer = _bank.Transfer(checking, individual, 1100);

            Assert.IsType<Approved>(deposit);
            Assert.IsType<Approved>(transfer);
            Assert.Equal(400, checking.Balance);
            Assert.Equal(1100, individual.Balance);
        }
    }
}
