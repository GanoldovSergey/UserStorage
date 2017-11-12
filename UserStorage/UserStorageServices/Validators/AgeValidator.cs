using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Exeptions;

namespace UserStorageServices.Validators
{
    class AgeValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (user.Age < 1)
            {
                throw new AgeExceedsLimitsException("Age cannot be less than 1");
            }
        }
    }
}
