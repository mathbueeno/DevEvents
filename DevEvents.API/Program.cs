using DevEvents.API.Data;
using DevEvents.API.Entities;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Singleton - Funciona como se fosse um banco de dados em memória
builder.Services.AddSingleton<DevEventsContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	// Criação da documentação referente a API 
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "DevEvents.API",
		Version = "v1",
		Contact = new OpenApiContact
		{
			Name = "Matheus de Abreu",
			Email = "mathbueeno@gmail.com",
			Url = new Uri("https://github.com/mathbueeno")
		}
	});
	// Criação do documento XML referente a API
	var xmlFile = "DevEvents.API.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
