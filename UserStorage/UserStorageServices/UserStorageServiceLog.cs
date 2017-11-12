using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserStorageServiceLog : UserStorageServiceDecorator
    {
        private readonly BooleanSwitch logging = new BooleanSwitch("enableLogging", "switch in app.config");

        public UserStorageServiceLog(IUserStorageService storageService) : base(storageService) { }

        public override int Count
        {
            get
            {
                if (logging.Enabled)
                {
                    Trace.WriteLine("Count() method is called.");
                }

                return storageService.Count;
            }
        }

        public override void Add(User user)
        {
            if (logging.Enabled)
            {
                Trace.WriteLine("Add() method is called.");
            }

            storageService.Add(user);
        }
        public override bool Remove(User user)
        {
            if (logging.Enabled)
            {
                Trace.WriteLine("Remove() method is called.");
            }

            return storageService.Remove(user);
        }
        public override IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (logging.Enabled)
            {
                Trace.WriteLine("SearchByFirstName() method is called.");
            }

            return storageService.SearchByFirstName(firstName);
        }
        public override IEnumerable<User> SearchByFirstNameAndLastName(string firstName, string lastName)
        {
            if (logging.Enabled)
            {
                Trace.WriteLine("SearchByFirstNameAndLastName() method is called.");
            }

            return storageService.SearchByFirstNameAndLastName(firstName, lastName);
        }
        public override IEnumerable<User> SearchByFirstNameAndAge(string firstName, int age)
        {
            if (logging.Enabled)
            {
                Trace.WriteLine("SearchByFirstNameAndAge() method is called.");
            }

            return storageService.SearchByFirstNameAndAge(firstName, age);
        }
        public override IEnumerable<User> SearchByLastName(string lastName)
        {
            if (logging.Enabled)
            {
                Trace.WriteLine("SearchByLastName() method is called.");
            }

            return storageService.SearchByLastName(lastName);
        }
        public override IEnumerable<User> SearchByLastNameAndAge(string lastName, int age)
        {
            if (logging.Enabled)
            {
                Trace.WriteLine("SearchByLastNameAndAge() method is called.");
            }

            return storageService.SearchByLastNameAndAge(lastName, age);
        }
        public override IEnumerable<User> SearchByAge(int age)
        {
            if (logging.Enabled)
            {
                Trace.WriteLine("SearchByAge() method is called.");
            }

            return storageService.SearchByAge(age);
        }
        public override IEnumerable<User> SearchByFirstNameAndLastNameAndAge(string firstName, string lastName, int age)
        {
            if (logging.Enabled)
            {
                Trace.WriteLine("SearchByFirstNameAndLastNameAndAge() method is called.");
            }

            return storageService.SearchByFirstNameAndLastNameAndAge(firstName, lastName, age);
        }
    }
}
