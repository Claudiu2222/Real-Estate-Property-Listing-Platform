using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealEstateListingPlatform.App;
using RealEstateListingPlatform.App.Auth;
using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using Havit.Blazor.Components.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddHxServices();

//for property controller
builder.Services.AddHttpClient<IPropertyDataService, PropertyDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7187/");
});

builder.Services.AddHttpClient<IListingDataService, ListingDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7187/");
});

//for creating an Admin acc
builder.Services.AddHttpClient<IUserService, UserDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7187/");
});


builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7187/");
});

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
