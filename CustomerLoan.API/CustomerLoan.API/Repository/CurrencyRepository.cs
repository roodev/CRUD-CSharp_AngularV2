using CustomerLoan.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerLoan.API.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly HttpClient _httpClient;

        public CurrencyRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Currency>> GetCurrenciesAsync()
        {
            var response = await _httpClient.GetAsync("https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/Moedas?$top=100&$format=json");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                var jsonObject = JObject.Parse(content);
                var currenciesJsonArray = jsonObject["value"].ToString();
                var currencies = JsonConvert.DeserializeObject<List<Currency>>(currenciesJsonArray);
                return currencies;
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                Console.WriteLine($"Erro ao desserializar o JSON: {ex.Message}");
                throw;
            }
        }

    }
}
