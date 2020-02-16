namespace BankApplication.Models.Accounts
{
    public class IndividualInvestment : Account
    {
        public IndividualInvestment(AccountOwner owner) : base(owner)
        {
        }

        public override decimal GetWithdrawLimit() => 1000m;
    }
}
