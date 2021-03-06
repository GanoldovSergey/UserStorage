﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Enum;

namespace UserStorageServices
{
    public abstract class UserStorageServiceDecorator : IUserStorageService
    {
        protected readonly IUserStorageService storageService;

        protected UserStorageServiceDecorator(IUserStorageService storageService)
        {
            this.storageService = storageService;
        }

        public abstract int Count { get; }
        public abstract UserStorageServiceMode ServiceMode { get; }
        public abstract void Add(User user);
        public abstract bool Remove(User user);
        public abstract IEnumerable<User> SearchByFirstName(string firstName);
        public abstract IEnumerable<User> SearchByFirstNameAndLastName(string firstName, string lastName);
        public abstract IEnumerable<User> SearchByFirstNameAndAge(string firstName, int age);
        public abstract IEnumerable<User> SearchByLastName(string lastName);
        public abstract IEnumerable<User> SearchByLastNameAndAge(string lastName, int age);
        public abstract IEnumerable<User> SearchByAge(int age);
        public abstract IEnumerable<User> SearchByFirstNameAndLastNameAndAge(string firstName, string lastName, int age);
    }
}
