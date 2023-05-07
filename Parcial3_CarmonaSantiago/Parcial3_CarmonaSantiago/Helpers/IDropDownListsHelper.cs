using Microsoft.AspNetCore.Mvc.Rendering;

namespace Parcial3_CarmonaSantiago.Helpers
{
    public interface IDropDownListsHelper
    {
        Task<IEnumerable<SelectListItem>> GetDDLServices();
    }
}
