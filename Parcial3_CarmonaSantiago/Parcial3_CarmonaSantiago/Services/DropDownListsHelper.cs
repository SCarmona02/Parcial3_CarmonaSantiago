using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial3_CarmonaSantiago.DAL;
using Parcial3_CarmonaSantiago.Helpers;

namespace Parcial3_CarmonaSantiago.Services
{
    public class DropDownListsHelper : IDropDownListsHelper
    {
        private readonly DatabaseContext _context;

        public DropDownListsHelper(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLServices()
        {
            List<SelectListItem> listServices = await _context.Services
                .Select(service => new SelectListItem
                {
                    Text = service.Name,
                    Value = service.Id.ToString(),
                })
                .OrderBy(service => service.Text)
                .ToListAsync();

            listServices.Insert(0, new SelectListItem
            {
                Text = "Seleccione un servicio...",
                Value = "0",
            });

            return listServices;
        }
    }
}
