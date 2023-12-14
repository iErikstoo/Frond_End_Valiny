using System.ComponentModel.DataAnnotations;

namespace WebApplicationBilling.Models.DTO
{
    public class RegistroDTO
    {
        public int RegistroId { get; set; }
        public int TiposRegistroId { get; set; }
        public int MatriculaId { get; set; }
        public int AdministrativoId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
