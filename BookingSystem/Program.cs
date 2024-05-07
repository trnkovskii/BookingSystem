using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.ApplicationService.Services;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;
using BookingSystem.Storage.Repositories;
using BookingSystem.WebAPI.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using BookingSystem.Models.FluentValidation;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var serviceProvider = builder.Services.BuildServiceProvider();
var configuration = serviceProvider.GetService<IConfiguration>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton(typeof(IInMemoryRepository<>), typeof(InMemoryRepository<>));
builder.Services.AddSingleton<ISearchRepository, SearchRepository>();
builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddTransient<IValidator<SearchReq>, SearchReqValidator>();
builder.Services.AddTransient<IValidator<BookReq>, BookReqValidator>();
builder.Services.AddTransient<IValidator<CheckStatusReq>, CheckStatusReqValidator>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IGetSearchDataFromApiService, GetSearchDataFromApiService>();
builder.Services.AddScoped<IRandomGeneratorService, RandomGeneratorService>();
builder.Services.AddScoped<IBookingStatusDeterminer, BookingStatusDeterminer>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICheckStatusService, CheckStatusService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking System", Version = "v1" });

    c.AddSecurityDefinition("apiKey", new OpenApiSecurityScheme
    {
        Description = "API Key",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "apiKey"
                    }
                },
                Array.Empty<string>()
            }
        });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
           options.SlidingExpiration = true;
           options.AccessDeniedPath = "/Home/Forbidden";
       });

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking System");
});


app.UseMiddleware<HeaderAuthorizationMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseEndpoints(routes =>
{
    routes.MapControllerRoute(
        "default",
        "api/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
