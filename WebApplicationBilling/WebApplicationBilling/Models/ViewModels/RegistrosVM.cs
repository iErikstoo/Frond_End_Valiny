using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationBilling.Models.DTO;

namespace WebApplicationBilling.Models.ViewModels
{
    public class RegistrosVM
    {
        public IEnumerable<SelectListItem> ListRegistros { get; set; }
        public RegistroDTO Registro { get; set; }

    }
}
