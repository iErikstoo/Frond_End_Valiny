using Newtonsoft.Json;
using System.Collections;
using System.Text;
using WebApplicationBilling.Repository.Interfaces;

namespace WebApplicationBilling.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //Inyección de dependencias e inversión de control
        private readonly IHttpClientFactory _httpClientFactory;

        public Repository(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public Task<bool> DeleteAsync(string url, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable> GetAllAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _httpClientFactory.CreateClient();

            try
            {
                HttpResponseMessage responseMessage = await client.SendAsync(request);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonString = await responseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
                }
                else
                {
                    // Opcional: Manejar diferentes códigos de estado de manera más específica
                    throw new HttpRequestException($"Request to {url} failed with status code {responseMessage.StatusCode}.");
                }
            }
            catch (Exception ex)
            {
                // Opcional: Log the exception or handle it as needed
                throw new ApplicationException($"Error fetching data from {url}", ex);
            }
        }

        public Task<T> GetByIdAsync(string url, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PostAsync(string url, T entity)
        {
           var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (entity !=null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            //Validar si se actualizo y retorna boleano
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<bool> UpdateAsync(string url, T entity)
        {
            throw new NotImplementedException();
        }
    }

}
