using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using e_me.Model.Resources;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace e_me.Model.Models
{
    public class User : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string LoginName { get; set; }

        [MaxLength(200)]
        public string Password { get; set; }

        [MaxLength(50)]
        [Required]
        public string FullName { get; set; }


        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }

        [JsonIgnore]
        public DateTime CreationDate { get; set; }

        [Timestamp]
        [JsonIgnore]
        public byte[] RowVersion { get; set; }

        public bool ChangePasswordNextLogon { get; set; }

        [MaxLength(50)]
        public string PersonalNumericCode { get; set; }

        [MaxLength(200)]
        [NotMapped]
        public string OldPassword { get; set; }

        [MaxLength(200)]
        [NotMapped]
        public string PasswordConfirmation { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Guid SecurityRoleId { get; set; }

        [NotMapped]
        public SecurityRole SecurityRole { get; set; }
    }
}
