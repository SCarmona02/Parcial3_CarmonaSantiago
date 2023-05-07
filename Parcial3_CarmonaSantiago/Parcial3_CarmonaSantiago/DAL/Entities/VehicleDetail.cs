using System.ComponentModel.DataAnnotations;

namespace Parcial3_CarmonaSantiago.DAL.Entities
{
    public class VehicleDetail : Entity
    {
        [Display(Name = "Fecha de creación")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Fecha de entrega")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Vehiculo")]
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Usuario")]
        public User User { get; set; }
    }
}
