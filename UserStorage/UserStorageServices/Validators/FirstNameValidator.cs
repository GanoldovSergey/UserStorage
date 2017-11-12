using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Exeptions;

namespace UserStorageServices.Validators
{
    class FirstNameValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty or whitespace");
            }
        }
    }
}
