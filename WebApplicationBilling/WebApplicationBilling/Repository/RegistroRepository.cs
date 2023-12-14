using WebApplicationBilling.Models.DTO;
using WebApplicationBilling.Repository.Interfaces;

namespace WebApplicationBilling.Repository
{
    public class RegistroRepository : Repository<RegistroDTO>, IRegistrosRepository
    {
        public RegistroRepository(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {

        }
    }
}
