using CatalogoDeProdutos.Api.Data;
using CatalogoDeProdutos.Core.Handler;
using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Request;
using CatalogoDeProdutos.Core.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=produtos.db"));

var app = builder.Build();

app.MapGet("/Helth-Check", () => "OK");

app.MapGet("/v1/produtos", async ([FromServices] IProdutoHandler produtoHandler) =>
{
  
});


app.Run();
