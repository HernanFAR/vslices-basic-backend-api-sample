using Core;
using CrossCutting;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Views;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddViewDependencies()
    .AddCoreDependencies()
    .AddCrossCuttingDependencies();

var host = builder.Build();

await host.RunAsync();
