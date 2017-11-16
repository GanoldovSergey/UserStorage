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
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        private readonly IList<IUserStorageService> slaveServices;
        private readonly IList<INotificationSubscriber> subscribers;

        public UserStorageServiceMaster(
            IUserIdGenerator userIdGenerator = null,
            IUserValidator userValidator = null,
            IEnumerable<IUserStorageService> services = null)
            : base(userIdGenerator, userValidator)
        {
            slaveServices = services?.ToList() ?? new List<IUserStorageService>();
            subscribers = new List<INotificationSubscriber>();
        }

        private event Action<User> AddedToStorage;

        private event Action<User> RemovedFromStorage;

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.MasterNode;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public override void Add(User user)
        {
            base.Add(user);

            if (slaveServices != null)
            {
                foreach (var t in slaveServices)
                {
                    t.Add(user);
                }
            }

            AddedToStorage?.Invoke(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public override bool Remove(User user)
        {
            if (base.Remove(user))
            {
                if (slaveServices != null)
                {
                    foreach (var t in slaveServices)
                    {
                        t.Remove(user);
                    }
                }

                RemovedFromStorage?.Invoke(user);
                return true;
            }

            return false;
        }

        public void AddSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            subscribers.Add(subscriber);
            AddedToStorage += subscriber.UserAdded;
            RemovedFromStorage += subscriber.UserRemoved;
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            subscribers.Remove(subscriber);
            AddedToStorage -= subscriber.UserAdded;
            RemovedFromStorage -= subscriber.UserRemoved;
        }
    }
}
