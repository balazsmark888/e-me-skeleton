using System;

namespace e_me.Shared.DTOs.User
{
    public class UserSessionInfo
    {
        public UserSessionInfo()
        {
            Timestamp = DateTime.Now.ToString("o");
        }

        public Guid Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Timestamp { get; set; }

        public string ConnectionId { get; set; }

        public string AvatarUrl { get; set; }
    }
}
