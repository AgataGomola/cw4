using System;
using LegacyApp.Interfaces;

namespace LegacyApp
{
    public class UserService
    {
        private IClientRepository _clientRepository;
        private ICreditLimitService _creditLimitService;

        [Obsolete]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditLimitService = new UserCreditService();
        }
        public UserService(IClientRepository clientRepository, ICreditLimitService creditLimitService)
        {
            _clientRepository = clientRepository;
            _creditLimitService = creditLimitService;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!User.ValidateUserData(firstName, lastName, email))
            {
                return false;
            }
            if (User.GetAge(dateOfBirth) < 21)
            {
                return false;
            }
            
            var client = _clientRepository.GetById(clientId);

            var user = new User(client, dateOfBirth, email, firstName, lastName);

            if (!user.CheckCreditLimit(client, _creditLimitService))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
