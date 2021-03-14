using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace e_me.Model.Models
{
    public class ApplicationSetting : BaseModel
    {
        [Timestamp]
        [JsonIgnore]
        public byte[] RowVersion { get; set; }

        [MaxLength(100)]
        [Required]
        public string Code { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Value { get; set; }
    }
}
