using Microsoft.AspNetCore.Mvc.Rendering;
using Parcial3_CarmonaSantiago.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parcial3_CarmonaSantiago.Models
{
    public class AddVehicleServiceViewModel : Entity
    {
        [Display(Name = "Propietario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Owner { get; set; }

        [Display(Name = "Placa")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string NumberPlate { get; set; }

        [Display(Name = "Servicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid ServiceId { get; set; }

        public IEnumerable<SelectListItem> Services { get; set; }
    }
}
