global using System.Net.Http.Json;
global using VehicleSpeedControlSystem.Client.Services.Contracts;
global using VehicleSpeedControlSystem.Shared.Entities;

global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using VehicleSpeedControlSystem.Client;
global using VehicleSpeedControlSystem.Client.Services;

global using System.Security.Claims;
global using System.Text.Json;
global using Microsoft.AspNetCore.Components;

global using Blazored.LocalStorage;
global using dymaptic.GeoBlazor.Core;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<ICoordinateService, CoordinateService>();
// builder.Services.AddScoped<IAdminService, AdminService>();
// builder.Services.AddScoped<ILandlordService, LandlordService>();
// builder.Services.AddScoped<IPurchaseService, PurchaseService>();
// builder.Services.AddScoped<ITitleDeedService, TitleDeedService>();
// builder.Services.AddScoped<IOwnershipService, OwnershipService>();
builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<AuthenticationStateProvider, LOAuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddScoped<IDisputeService, DisputeService>();


builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddGeoBlazor();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();