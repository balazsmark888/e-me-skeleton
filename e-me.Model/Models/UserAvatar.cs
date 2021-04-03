using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace e_me.Model.Models
{
    [Serializable]
    public class UserAvatar : BaseModel
    {
        public Guid UserId { get; set; }

        [JsonIgnore]
        public byte[] Avatar { get; set; }

        [JsonIgnore]
        public bool HasAvatar => Avatar != null && Avatar.Length > 0;

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User User { get; set; }
    }
}
