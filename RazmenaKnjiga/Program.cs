using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RazmenaKnjiga.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    return new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
});

builder.Services.AddSingleton<BookService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<MessagesService>();
builder.Services.AddSingleton<RequestService>();




builder.Services.AddControllers();
builder.Services.AddRazorPages();      // <--- Dodato za Razor Pages
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();      // <--- Dodato za Razor Pages

// Preusmeri root na Login stranicu
app.MapGet("/", context =>
{
    context.Response.Redirect("/login");
    return Task.CompletedTask;
});

app.Run();

