using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.ApplicationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<ISearchService, SearchService>();
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

//app.MapControllers();

app.Run();
