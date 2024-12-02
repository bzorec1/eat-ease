using EatEase.Contracts;
using EatEase.BlazorApp.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoginConsumer = EatEase.BlazorApp.Infrastructure.LoginConsumer;
using RegisterConsumer = EatEase.BlazorApp.Infrastructure.RegisterConsumer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddSingleton<PasswordHasher<User>>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddDbContext<UserContext>(options =>
    options.UseInMemoryDatabase("InMemoryAppDb"));
builder.Services.AddLogging();
builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingInMemory((context, factoryConfigurator) =>
    {
        factoryConfigurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(false));
    });

    configurator.AddConsumer<RegisterConsumer>();
    configurator.AddConsumer<LoginConsumer>();

    configurator.AddRequestClient<RegisterRequest>();
    configurator.AddRequestClient<LoginRequest>();
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();