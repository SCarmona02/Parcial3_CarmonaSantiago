using System.ComponentModel.DataAnnotations;

namespace Parcial3_CarmonaSantiago.DAL.Entities
{
    public class Entity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
