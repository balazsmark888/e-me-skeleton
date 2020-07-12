using System.ComponentModel.DataAnnotations;

namespace e_me.Model.Model
{
    public interface IBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
