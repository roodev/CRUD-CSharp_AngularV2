namespace CustomerLoan.API.Models
{
    public class CurrencyExchangeRate
    {
        public decimal ParidadeCompra { get; set; } 
        public decimal ParidadeVenda { get; set; } 
        public decimal CotacaoCompra { get; set; } 
        public decimal CotacaoVenda { get; set; } 
        public string DataHoraCotacao { get; set; } 
        public string TipoBoletim { get; set; } 
    }
}
