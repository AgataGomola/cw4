using System;
using LegacyApp.Interfaces;

namespace LegacyApp
{
    public class User
    {
        public User(object client, DateTime dateOfBirth, string emailAddress, string firstName, string lastName)
        {
            Client = client;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
        }

        public object Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }

        public static bool ValidateUserData(string firstName, string lastName, string email)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return false;

            if (string.IsNullOrEmpty(email) || !email.Contains("@") || !email.Contains("."))
                return false;

            return true;
        }

        public static int GetAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }

        public static bool HasLimitCredit(Client client)
        {
            if (client.Type == "VeryImportantClient")
            {
                return false;
            }
            return true;
        }

        public bool CheckCreditLimit(Client client, ICreditLimitService creditLimitService)
        {
            if (client.Type == "VeryImportantClient")
            {
                HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                
                int creditLimit = creditLimitService.GetCreditLimit(LastName, DateOfBirth);
                creditLimit *= 2;
                CreditLimit = creditLimit;
                
            }else
            {
                    HasCreditLimit = true;
                    int creditLimit = creditLimitService.GetCreditLimit(LastName, DateOfBirth);
                    CreditLimit = creditLimit;
                
            }return HasCreditLimit && CreditLimit < 500;
        }

    }
}
