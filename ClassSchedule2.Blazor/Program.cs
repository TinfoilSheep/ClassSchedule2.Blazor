using ClassSchedule2.Blazor.Components;
using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Models;
using ClassSchedule2.Blazor.Providers;
using ClassSchedule2.Blazor.Services.Data;
using ClassSchedule2.Blazor.Services.UI;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("Teacher", policy =>
        policy.RequireRole("Teacher"));

    options.AddPolicy("Student", policy =>
        policy.RequireRole("Student"));
});

builder.Services.AddScoped<BrowserAuthService>();
builder.Services.AddHttpClient<IInstitutionService, InstitutionService>();
builder.Services.AddScoped<SchoolAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<SchoolAuthenticationStateProvider>()); //Sikrer at man får fat i samme instans af SchoolAuthenticationStateProvider når man beder om Authentication
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
