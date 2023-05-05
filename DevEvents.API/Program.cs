using DevEvents.API.Data;
using DevEvents.API.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Singleton - Funciona como se fosse um banco de dados em memória
builder.Services.AddSingleton<DevEventsContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
