using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Parcial3_CarmonaSantiago.DAL.Entities
{
    public class Vehicle : Entity
    {
        [Display(Name = "Propietario")]
        public string Owner { get; set; }

        [Display(Name = "Placa")]
        public string NumberPlate { get; set; }

        [Display(Name = "Servicio")]
        public Service Service { get; set; }

        [Display(Name = "Detalles del Vehiculo")]
        public ICollection<VehicleDetail> VehicleDetails { get; set; }
    }
}
