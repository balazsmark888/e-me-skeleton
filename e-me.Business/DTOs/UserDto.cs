using System;

namespace e_me.Business.DTOs
{
    public class UserDto
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public DateTime ValidTo { get; set; }

        public string Role { get; set; }

        public string FullName { get; set; }

        public byte[] PublicKey { get; set; }
    }
}
