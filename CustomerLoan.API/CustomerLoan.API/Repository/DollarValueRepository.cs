using CustomerLoan.API.Models;
using CustomerLoan.API.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace CustomerLoan.API.Repository
{
    public class DollarValueRepository : IDollarValueRepository
    {
        private readonly HttpClient _httpClient;

        public DollarValueRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<DollarValueDTO> GetDollarValueAsync(DateTime date)
        {
            string formattedDate = date.ToString("MM-dd-yyyy");
            string apiUrl = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoDolarDia(dataCotacao=@dataCotacao)?%40dataCotacao=%27{formattedDate}%27&%24format=json";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            DollarValueDTO dollarValue = new DollarValueDTO();

            dynamic responseObject = JsonConvert.DeserializeObject(jsonResponse);
            dollarValue.CotacaoCompra = responseObject.value[0].cotacaoCompra;
            dollarValue.CotacaoVenda = responseObject.value[0].cotacaoVenda;
            dollarValue.DataHoraCotacao = responseObject.value[0].dataHoraCotacao;

            return dollarValue;
        }
    }
}
