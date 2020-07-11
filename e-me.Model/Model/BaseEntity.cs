using System.ComponentModel.DataAnnotations;

namespace e_me.Model.Model
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
