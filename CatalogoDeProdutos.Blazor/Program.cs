using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CatalogoDeProdutos.Blazor;
using CatalogoDeProdutos.Core.Handler;
using MudBlazor.Services;
using CatalogoDeProdutos.Blazor.Handler;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<IProdutoService, ProdutoHandler>();

builder.Services.AddMudServices();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
