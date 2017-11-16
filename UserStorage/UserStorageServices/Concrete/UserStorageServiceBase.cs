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
    public abstract class UserStorageServiceBase : IUserStorageService
    {
        private readonly List<User> users;

        private readonly IUserIdGenerator userIdGenerator;
        private readonly IUserValidator userValidator;

        protected UserStorageServiceBase(IUserIdGenerator userIdGenerator = null,
            IUserValidator userValidator = null,
            UserStorageServiceMode mode = UserStorageServiceMode.MasterNode,
            IEnumerable<IUserStorageService> services = null)
        {
            users = new List<User>();
            this.userIdGenerator = userIdGenerator ?? new UserIdGenerator();
            this.userValidator = userValidator ?? new UserValidator();
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => users.Count;
        public abstract UserStorageServiceMode ServiceMode { get; }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public virtual void Add(User user)
        {
            userValidator.Validate(user);
            user.Id = userIdGenerator.Generate();
            users.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public virtual bool Remove(User user)
        {
            userValidator.Validate(user);
            return users.Remove(user);
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
