namespace CustomerLoan.API.Models.DTO
{
    public class CurrencyExchangeRateDTO
    {
        public decimal ParidadeCompra { get; set; }
        public decimal ParidadeVenda { get; set; }
        public decimal CotacaoCompra { get; set; }
        public decimal CotacaoVenda { get; set; }
        public string DataHoraCotacao { get; set; }
        public string TipoBoletim { get; set; }
    }
}
