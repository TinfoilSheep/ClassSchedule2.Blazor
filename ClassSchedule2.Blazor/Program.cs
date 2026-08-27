using ClassSchedule2.Blazor.Components;
using ClassSchedule2.Blazor.Interfaces;
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
builder.Services.AddAuthorization();

//Providers
builder.Services.AddScoped<SchoolAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<SchoolAuthenticationStateProvider>()); //Sikrer at man får fat i samme instans af SchoolAuthenticationStateProvider når man beder om Authentication
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
//Services
builder.Services.AddHttpClient<IInstitutionService, InstitutionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<ITermService, TermService>();
builder.Services.AddScoped<IPeriodService, PeriodService>();
builder.Services.AddScoped<INonTeachingDayService, NonTeachingDayService>();
builder.Services.AddScoped<IHoldService, HoldService>();
builder.Services.AddScoped<IHoldMemberService, HoldMemberService>();
builder.Services.AddScoped<IStudentGroupService, StudentGroupService>();
builder.Services.AddScoped<IStudentGroupMemberService, StudentGroupMemberService>();
builder.Services.AddScoped<ILessonTemplateService, LessonTemplateService>();
builder.Services.AddScoped<ILessonGenerationService, LessonGenerationService>();
builder.Services.AddScoped<IScheduleService, DummyScheduleService>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<BrowserAuthService>();

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
