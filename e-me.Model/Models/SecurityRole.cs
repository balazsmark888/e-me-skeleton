using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace e_me.Model.Models
{
    public class SecurityRole : Model
    {
        [Timestamp]
        [JsonIgnore]
        public byte[] RowVersion { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int SecurityType { get; set; }
    }
}
