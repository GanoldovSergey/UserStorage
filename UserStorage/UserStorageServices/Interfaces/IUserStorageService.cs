﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Enum;

namespace UserStorageServices
{
    public interface IUserStorageService
    {
        UserStorageServiceMode ServiceMode { get; }
        int Count { get; }
        void Add(User user);
        bool Remove(User user);
        IEnumerable<User> SearchByFirstName(string firstName);
        IEnumerable<User> SearchByFirstNameAndLastName(string firstName, string lastName);
        IEnumerable<User> SearchByFirstNameAndAge(string firstName, int age);
        IEnumerable<User> SearchByLastName(string lastName);
        IEnumerable<User> SearchByLastNameAndAge(string lastName, int age);
        IEnumerable<User> SearchByAge(int age);
        IEnumerable<User> SearchByFirstNameAndLastNameAndAge(string firstName, string lastName, int age);

    }
}
