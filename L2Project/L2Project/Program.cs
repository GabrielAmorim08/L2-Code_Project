using L2Project.Business;
using L2Project.Interfaces;
using L2Project.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region CONNECTION STRING
string connectionString = builder.Configuration.GetConnectionString("String");
builder.Services.AddDbContext<Db_L2_project_context>(op => op.UseSqlServer(connectionString));
#endregion

#region INJEÇÃO DE DEPENDENCIA
builder.Services.AddScoped<IPedido, PedidoBO>();
#endregion

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
