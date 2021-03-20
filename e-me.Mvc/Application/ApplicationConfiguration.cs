using System;
using System.Collections.Generic;
using System.Linq;
using e_me.Business.DTOs;

namespace e_me.Mvc.Application
{
    public class ApplicationConfiguration
    {
        private static readonly object LockInstanceManager = new();

        private static volatile ApplicationConfiguration _instance;

        private readonly List<UserDetail> _connectedUsers = new List<UserDetail>();


        public static ApplicationConfiguration Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (LockInstanceManager)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }
                    _instance = new ApplicationConfiguration();
                }

                return _instance;
            }
        }

        public string ConnectionString { get; private set; }

        public string ApplicationKey { get; set; }

        public List<UserDetail> ConnectedUsers
        {
            get
            {
                lock (LockInstanceManager)
                {
                    return _connectedUsers;
                }
            }
        }

        public void EmptyConnectedUsers()
        {
            lock (LockInstanceManager)
            {
                _connectedUsers.Clear();
            }
        }

        public void AddToConnectedUsers(UserDetail userDetail)
        {
            lock (LockInstanceManager)
            {
                _connectedUsers.Add(userDetail);
            }
        }

        public void RemoveFromConnectedUsers(UserDetail userDetail)
        {
            lock (LockInstanceManager)
            {
                _connectedUsers.Remove(userDetail);
            }
        }

        public void RemoveConnectedUser(Guid userId)
        {
            lock (LockInstanceManager)
            {
                var item = _connectedUsers.FirstOrDefault(x => x.Id == userId);
                if (item != null)
                {
                    _connectedUsers.Remove(item);
                }
            }
        }
    }
}