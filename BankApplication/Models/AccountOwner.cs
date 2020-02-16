using System;

namespace BankApplication.Models
{
    public class AccountOwner
    {
        public AccountOwner(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("First name cannot be empty");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Last name cannot be empty");
            }

            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
