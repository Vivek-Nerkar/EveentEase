//using EventEase.Client.Pages;
//using EventEase.Components;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents()
//    .AddInteractiveWebAssemblyComponents();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseWebAssemblyDebugging();
//}
//else
//{
//    app.UseExceptionHandler("/Error", createScopeForErrors: true);
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
//app.UseHttpsRedirection();

//app.UseAntiforgery();

//app.MapStaticAssets();
//app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode()
//    .AddInteractiveWebAssemblyRenderMode()
//    .AddAdditionalAssemblies(typeof(EventEase.Client._Imports).Assembly);

//app.Run();
// Program.cs (Blazor WASM client or server host as appropriate)
using EventEase.Services;

var builder = WebApplication.CreateBuilder(args);

// ... builder setup ...
builder.Services.AddSingleton<IEventService, InMemoryEventService>(); // existing
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddSingleton<IAttendanceService, AttendanceService>();
// JS runtime is available by default in Blazor apps


// For server-hosted apps register server-side service
builder.Services.AddSingleton<IEventService, InMemoryEventService>();

// For Blazor WebAssembly client project, register in Program.cs:
// builder.Services.AddSingleton<IEventService, InMemoryEventService>();

builder.Services.AddAuthorizationCore();
// plus register an AuthenticationStateProvider if you have one, otherwise use a stub.

builder.Services.AddLogging();


var app = builder.Build();
// ... existing middleware
app.Run();
