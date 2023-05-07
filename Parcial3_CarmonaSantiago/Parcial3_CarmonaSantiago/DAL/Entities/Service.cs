using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Parcial3_CarmonaSantiago.DAL.Entities
{
    public class Service : Entity
    {
        [Display(Name = "Servicio")]
        public string Name { get; set; }

        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Display(Name = "Vehiculos")]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
