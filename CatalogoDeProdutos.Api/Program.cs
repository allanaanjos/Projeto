using CatalogoDeProdutos.Api.Data;
using CatalogoDeProdutos.Api.Repository;
using CatalogoDeProdutos.Core.Handler;
using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Repository;
using CatalogoDeProdutos.Core.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=produtos.db"));

builder.Services.AddTransient<IProdutoHandler, ProdutoHandler>();
builder.Services.AddTransient<IRepository<Produto>, Repository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", builder =>
    {
        builder.WithOrigins("http://localhost:5165") 
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});



var app = builder.Build();


app.MapGet("/Helth-Check", () => "OK");

app.MapGet("/v1/produtos", async
([FromServices] IProdutoHandler produtoHandler,
int pageNumber = 1,
int pageSize = 10) =>
{
    var produtoPage = new PagedRequest
    {
        PageNumber = pageNumber,
        PageSize = pageSize
    };

    var produtos = await produtoHandler.GetProdutos(produtoPage);

    return Results.Ok(produtos);
});


app.MapGet("/v1/produtos/{id}", async
([FromRoute] long id, [FromServices] IProdutoHandler handler) =>
{
    if (id <= 0)
        return Results.BadRequest("Id inválido.");

    var request = new GetProdutoByIdRequest { Id = id };

    var response = await handler.GetProdutoById(request);

    if (response.Data == null)
        return Results.NotFound(response.Message);

    return Results.Ok(response.Data);
});

app.MapPost("/v1/produtos/criar", async
([FromServices] IProdutoHandler handler,
[FromBody] CreateProdutoRequest request) =>
{
    if (request is null)
        return Results.BadRequest("Produto inválido.");

    var produto = await handler.CreateProduto(request);

    if (produto is null)
        return Results.Problem
        ("Erro ao criar o produto.", statusCode: 500);



    return Results.Created($"/v1/produtos/{produto.Data!.Id}", produto);
});


app.MapPut("/v1/produtos/{id}", async
([FromRoute] long id,
[FromServices] IProdutoHandler handler,
[FromBody] UpdateProdutoRequest request) =>
{
    if (id <= 0)
        return Results.BadRequest("ID inválido.");

    if (request is null)
        return Results.BadRequest
        ("Dados do produto inválidos.");

    request.ProdutoId = id;

    var produtoAtualizado = await handler.UpdateProduto(request);

    if (produtoAtualizado is null)
        return Results.NotFound
        ($"Produto com ID {id} não encontrado.");

    return Results.Ok(produtoAtualizado);
});


app.MapDelete("/v1/produtos/{id}", async
([FromRoute] long id,
[FromServices] IProdutoHandler handler) =>
{
    if (id <= 0)
        return Results.BadRequest("ID inválido.");

    var resultado = await
    handler.DeleteProduto
    (new DeleteProdutoRequest { Id = id });

    if (resultado == null)
        return Results.NotFound
        ($"Produto com ID {id} não encontrado.");

    return Results.NoContent();
});



app.UseCors("AllowBlazorApp");

app.Run();
