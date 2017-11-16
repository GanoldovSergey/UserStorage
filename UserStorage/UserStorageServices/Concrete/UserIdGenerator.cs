using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserIdGenerator : IUserIdGenerator
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}
