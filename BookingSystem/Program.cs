using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.ApplicationService.Services;
using BookingSystem.Storage.Interfaces;
using BookingSystem.Storage.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var serviceProvider = builder.Services.BuildServiceProvider();
var configuration = serviceProvider.GetService<IConfiguration>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton(typeof(IInMemoryRepository<>), typeof(InMemoryRepository<>));
builder.Services.AddSingleton<ISearchRepository, SearchRepository>();
builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddSingleton<ICheckStatusRepository, CheckStatusRepository>();

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IGetSearchDataFromApiService, GetSearchDataFromApiService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICheckStatusService, CheckStatusService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(routes =>
{
    routes.MapControllerRoute(
        "default",
        "api/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
