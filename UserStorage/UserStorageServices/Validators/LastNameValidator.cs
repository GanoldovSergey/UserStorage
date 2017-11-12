using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Exeptions;

namespace UserStorageServices.Validators
{
    class LastNameValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty or whitespace");
            }
        }
    }
}
