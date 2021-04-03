using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace e_me.Model.Models
{
    public class UserSecurityRole : BaseModel
    {
        [Timestamp]
        [JsonIgnore]
        public byte[] RowVersion { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public Guid SecurityRoleId { get; set; }

        [ForeignKey("SecurityRoleId")]
        public SecurityRole SecurityRole { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
