using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Enum;
using UserStorageServices.Exeptions;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Concrete
{
    public class UserStorageServiceSlave : UserStorageServiceBase, INotificationSubscriber
    {
        public UserStorageServiceSlave(IUserIdGenerator userIdGenerator = null,
            IUserValidator userValidator = null)
            : base(userIdGenerator, userValidator)
        {

        }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.SlaveNode;
        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public override void Add(User user)
        {
            if (!HaveMaster())
            {
                throw new NotSupportedException("This action is notallowed");
            }

            base.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public override bool Remove(User user)
        {
            if (!HaveMaster())
            {
                throw new NotSupportedException("This action is notallowed");
            }

            return base.Remove(user);
        }


        public void UserAdded(User user)
        {
            Add(user);
        }

        public void UserRemoved(User user)
        {
            Remove(user);
        }

        private bool HaveMaster()
        {
            var stTrace = new StackTrace();
            var currentCalled = stTrace.GetFrame(1).GetMethod();
            var calledMetod = typeof(UserStorageServiceMaster).GetMethod(currentCalled.Name);
            var frames = stTrace.GetFrames();
            bool flag;
            if (frames != null)
            {
                flag = frames.Select(x => x.GetMethod()).Contains(calledMetod);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return flag;
        }
    }
}
