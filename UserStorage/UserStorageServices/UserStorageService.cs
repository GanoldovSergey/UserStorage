using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        private readonly BooleanSwitch logging = new BooleanSwitch("enableLogging", "switch in app.config");

        public UserStorageService(IUserIdGenerator userIdGenerator = null, IUserValidator userValidator = null)
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
        
        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            userValidator.Validate(user);

            if (logging.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            user.Id = userIdGenerator.Generate();
            users.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public bool Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (logging.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            return users.Remove(user);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("FirstName is null or empty or whitespace", nameof(firstName));
            }

            if (logging.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            return users.FindAll(x => x.FirstName == firstName);
        }

        public IEnumerable<User> SearchByLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("LastName is null or empty or whitespace", nameof(lastName));
            }

            if (logging.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            return users.FindAll(x => x.LastName == lastName);
        }

        public IEnumerable<User> SearchByAge(int age)
        {
            if (age < 1)
            {
                throw new ArgumentException("Age cannot be less than 1", nameof(age));
            }

            if (logging.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            return users.FindAll(x => x.Age == age);
        }
    }
}
