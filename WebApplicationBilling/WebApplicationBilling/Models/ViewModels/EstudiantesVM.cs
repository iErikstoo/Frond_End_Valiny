using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationBilling.Models.DTO;

namespace WebApplicationBilling.Models.ViewModels
{
    public class EstudiantesVM
    {
        public IEnumerable<SelectListItem> ListEstudiantes { get; set; }
        public EstudianteDTO Estudiante { get; set; }

    }
}
