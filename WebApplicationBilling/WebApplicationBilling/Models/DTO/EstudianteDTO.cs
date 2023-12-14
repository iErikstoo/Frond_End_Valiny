using System.ComponentModel.DataAnnotations;

namespace WebApplicationBilling.Models.DTO
{
    public class EstudianteDTO
    {
        public int EstudianteId { get; set; }
       
        public string P_Nombre { get; set; }
        
        public string S_Nombre { get; set; } = string.Empty;
     
        public string T_Nombre { get; set; } = string.Empty;
      
        public string P_Apellido { get; set; }
     
        public string S_Apellido { get; set; }

        public string T_Documento { get; set; }

    }
}
    