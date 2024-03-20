using CustomerLoan.API.Models;
using CustomerLoan.API.Models.DTO;
using Newtonsoft.Json;

namespace CustomerLoan.API.Repository
{
    public class CurrencyExchangeRepository : ICurrencyExchangeRepository
    {
        private readonly HttpClient _httpClient;

        public CurrencyExchangeRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<CurrencyExchangeRateDTO> GetCurrencyExchangeAsync(string type, DateTime date)
        {
            string formattedDate = date.ToString("MM-dd-yyyy");
            string apiUrl = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?%40moeda='{type}'&%40dataCotacao='{formattedDate}'&%24format=json";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var CurrencyExchange = new CurrencyExchangeRateDTO();

            dynamic responseObject = JsonConvert.DeserializeObject(jsonResponse);
            CurrencyExchange.ParidadeCompra = responseObject.value[0].paridadeCompra;
            CurrencyExchange.ParidadeVenda = responseObject.value[0].paridadeVenda;
            CurrencyExchange.CotacaoCompra = responseObject.value[0].cotacaoCompra;
            CurrencyExchange.CotacaoVenda = responseObject.value[0].cotacaoVenda;
            CurrencyExchange.DataHoraCotacao = responseObject.value[0].dataHoraCotacao;
            CurrencyExchange.TipoBoletim = responseObject.value[0].tipoBoletim;
            return CurrencyExchange;
        }
    }
}
