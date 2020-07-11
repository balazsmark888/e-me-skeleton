using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_me.Model.Model
{
    [Table("Setting", Schema = "dbo")]
    [Serializable]
    public class Setting : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Value { get; set; }
    }
}
