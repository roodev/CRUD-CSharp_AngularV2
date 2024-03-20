using Microsoft.EntityFrameworkCore;
using CustomerLoan.API.Models;
using CustomerLoan.API.Repository;
using CustomerLoan.API.Services;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerLoanContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository, CustomerRepository>(); 
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IDollarValueRepository, DollarValueRepository>();
builder.Services.AddScoped<ICurrencyExchangeRepository, CurrencyExchangeRepository>();

builder.Services.AddScoped<IService<Customer>, CustomerServices>();
builder.Services.AddScoped<ILoanService<Loan>, LoanServices>();
builder.Services.AddScoped<ICurrencyService, CurrencyServices>();
builder.Services.AddScoped<IDollarValueService, DollarValueServices>();
builder.Services.AddScoped<ICurrencyExchangeService, CurrencyExchangeServices>();


builder.Services.AddHttpClient<LoanServices>(client =>
{
    client.BaseAddress = new Uri("https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/");
});

var jsonSerializerSettings = new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
};

builder.Services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
      policy =>
      {
          policy.AllowAnyOrigin()
             .AllowAnyHeader()
             .AllowAnyMethod();
      });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
