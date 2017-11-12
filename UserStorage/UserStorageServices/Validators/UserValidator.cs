using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Validators;

namespace UserStorageServices
{
    public class UserValidator : IUserValidator
    {
        private readonly IUserValidator[] validators;

        public UserValidator()
        {
            validators = new IUserValidator[]
            {
                new AgeValidator(), 
                new FirstNameValidator(), 
                new LastNameValidator()
            };
        }

        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            foreach (var x in validators)
            {
                x.Validate(user);
            }
        }
    }
}
