using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pottencial.Payment.Api.Aplicacao.Interfaces;
using Pottencial.Payment.Api.Dominio.Fabricas;
using Pottencial.Payment.Api.Infraestrutura.Context;
using Pottencial.Payment.Api.Infraestrutura.Repositorios;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adicionando Serviço do MediatR apontando para a camada de Aplicação
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Pottencial.Payment.Api.Aplicacao"));

//Banco de Dados InMemory
builder.Services.AddDbContext<VendasContext>(opt => opt.UseInMemoryDatabase("Banco"));

//Adicionando Contexto do Banco de Dados e string de conexão pelo appsettings.json - SQLSERVER
//builder.Services.AddDbContext<VendasContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"),
//x => x.MigrationsHistoryTable("__VendasMigrationsHistory")));


//fabricas
builder.Services.AddSingleton<IVendaFabrica, VendaFabrica>();
builder.Services.AddSingleton<IItemFabrica, ItemFabrica>();
builder.Services.AddSingleton<IVendedorFabrica, VendedorFabrica>();

//repositorios
builder.Services.AddScoped<IVendaRepositorio, VendaRepositorio>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Pottencial.Payment.Api",
        Description = "Teste técnico Pottencial.",
        Contact = new OpenApiContact
        {
            Name = "Cleiton Cardoso",
            Url = new Uri("https://www.linkedin.com/in/cleiton-cardoso/")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "api-docs";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pottencial.Payment.Api");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
