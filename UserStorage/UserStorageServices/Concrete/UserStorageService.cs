using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Enum;
using UserStorageServices.Exeptions;
using UserStorageServices.Interfaces;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService : IUserStorageService
    {
        private readonly List<User> users;

        private readonly IUserIdGenerator userIdGenerator;
        private readonly IUserValidator userValidator;
        private readonly UserStorageServiceMode mode;
        private readonly IList<IUserStorageService> slaveServices;
        private readonly IList<INotificationSubscriber> subscribers;

        public UserStorageService(IUserIdGenerator userIdGenerator = null,
            IUserValidator userValidator = null,
            UserStorageServiceMode mode = UserStorageServiceMode.MasterNode,
            IEnumerable<IUserStorageService> services = null)
        {
            users = new List<User>();

            this.userIdGenerator = userIdGenerator ?? new UserIdGenerator();
            this.userValidator = userValidator ?? new UserValidator();
            this.mode = mode;
            slaveServices = services?.ToList() ?? new List<IUserStorageService>();
            subscribers = new List<INotificationSubscriber>();
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => users.Count;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (mode == UserStorageServiceMode.SlaveNode)
            {
                throw new NotSupportedException("This action is notallowed");
            }

            userValidator.Validate(user);
            user.Id = userIdGenerator.Generate();
            users.Add(user);

            if (slaveServices != null)
            {
                foreach (var t in slaveServices)
                {
                    t.Add(user);
                }
            }

            foreach (var t in subscribers)
            {
                t.UserAdded(user);
            }
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public bool Remove(User user)
        {
            if (mode == UserStorageServiceMode.SlaveNode)
            {
                throw new NotSupportedException("This action is notallowed");
            }

            userValidator.Validate(user);
            
            if (users.Remove(user))
            {
                if (slaveServices != null)
                {
                    foreach (var t in slaveServices)
                    {
                        t.Remove(user);
                    }
                }

                foreach (var t in subscribers)
                {
                    t.UserRemoved(user);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty or whitespace");
            }

            return users.FindAll(x => x.FirstName == firstName);
        }

        public IEnumerable<User> SearchByFirstNameAndLastName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty or whitespace");
            }

            return users.FindAll(x => x.FirstName == firstName && x.LastName == lastName);
        }

        public IEnumerable<User> SearchByFirstNameAndAge(string firstName, int age)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty or whitespace");
            }

            if (age < 1)
            {
                throw new AgeExceedsLimitsException("Age cannot be less than 1");
            }

            return users.FindAll(x => x.FirstName == firstName && x.Age == age);
        }

        public IEnumerable<User> SearchByLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty or whitespace");
            }

            return users.FindAll(x => x.LastName == lastName);
        }

        public IEnumerable<User> SearchByLastNameAndAge(string lastName, int age)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty or whitespace");
            }

            if (age < 1)
            {
                throw new AgeExceedsLimitsException("Age cannot be less than 1");
            }

            return users.FindAll(x => x.LastName == lastName && x.Age == age);
        }

        public IEnumerable<User> SearchByAge(int age)
        {
            if (age < 1)
            {
                throw new AgeExceedsLimitsException("Age cannot be less than 1");
            }

            return users.FindAll(x => x.Age == age);
        }

        public IEnumerable<User> SearchByFirstNameAndLastNameAndAge(string firstName, string lastName, int age)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty or whitespace");
            }

            if (age < 1)
            {
                throw new AgeExceedsLimitsException("Age cannot be less than 1");
            }

            return users.FindAll(x => x.FirstName == firstName && x.LastName == lastName && x.Age == age);
        }
    }
}
