using WebApplicationBilling.Models.DTO;
using WebApplicationBilling.Repository.Interfaces;

namespace WebApplicationBilling.Repository
{
    public class EstudianteRepository : Repository<EstudianteDTO>, IEstudiantesRepository
    {
        public EstudianteRepository(IHttpClientFactory httpClientFactory) 
            : base(httpClientFactory)
        {

        }
    }
}
