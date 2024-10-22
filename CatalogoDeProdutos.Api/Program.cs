using CatalogoDeProdutos.Core.Handler;
using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Repository;
using CatalogoDeProdutos.Core.Request;
using CatalogoDeProdutos.Infra.Data;
using CatalogoDeProdutos.Infra.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProdutosDb"));

builder.Services.AddTransient<IProdutoService, ProdutoService>();
builder.Services.AddTransient<IGenericRepository<Produto>, ProdutoRepository>();

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


app.MapGet("/", () => "UrlProdutos: http://localhost:5064/v1/produtos");

app.MapGet("/v1/produtos", async
([FromServices] IProdutoService service,
int pageNumber = 1,
int pageSize = 10) =>
{
    var produtoPage = new PagedRequest
    {
        PageNumber = pageNumber,
        PageSize = pageSize
    };

    var produtos = await service.GetProdutos(produtoPage);

    return Results.Ok(produtos);
});


app.MapGet("/v1/produtos/{id}", async
([FromRoute] long id, [FromServices] IProdutoService service) =>
{
    if (id <= 0)
        return Results.BadRequest("Id inválido.");

    var request = new GetProdutoByIdRequest { Id = id };

    var response = await service.GetProdutoById(request);

    if (response.Data == null)
        return Results.NotFound(response.Message);

    return Results.Ok(response.Data);
});

app.MapPost("/v1/produtos/criar", async
([FromServices] IProdutoService service,
[FromBody] CreateProdutoRequest request) =>
{
    if (request is null)
        return Results.BadRequest("Produto inválido.");

    await service.CreateProduto(request);

    return Results.Created();
});


app.MapPut("/v1/produtos/{id}", async
([FromRoute] long id,
[FromServices] IProdutoService service,
[FromBody] UpdateProdutoRequest request) =>
{
    if (id <= 0)
        return Results.BadRequest("ID inválido.");

    if (request is null)
        return Results.BadRequest
        ("Dados do produto inválidos.");

    request.ProdutoId = id;

    await service.UpdateProduto(request);

    return Results.Ok();

});


app.MapDelete("/v1/produtos/{id}", async
([FromRoute] long id,
[FromServices] IProdutoService service) =>
{
    if (id <= 0)
        return Results.BadRequest("ID inválido.");

    await service.ExcluirProduto(new DeleteProdutoRequest { Id = id });

    return Results.NoContent();
});


app.UseCors("AllowBlazorApp");

app.Run();
