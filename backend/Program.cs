using Microsoft.EntityFrameworkCore;
using Portfolio.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<PortfolioContext>(builder.Configuration.GetConnectionString("ServerConnection"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

//usando swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
//permite que api chame qualquer header, metodo ou origem 
//quando front end for chamar o back end que esta aqui nao ocorrer problemas 
app.UseCors(p => p
.AllowAnyHeader()
.AllowAnyOrigin()
.AllowAnyMethod());

app.MapPost("/contatos", async (PortfolioContext context, Contato contato) =>
{
    await context.Contatos.AddAsync(contato);
    await context.SaveChangesAsync();

    return Results.Ok(contato);
})
.WithOpenApi();

app.MapGet("/contatos", async (PortfolioContext context) =>
{
    var contatos = await context.Contatos.ToListAsync();

    return Results.Ok(contatos);
})
.WithOpenApi();



app.Run();

public class Contato
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
}
