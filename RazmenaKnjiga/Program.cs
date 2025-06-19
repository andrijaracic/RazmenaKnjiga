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

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.MapGet("/", context =>
{
    context.Response.Redirect("/login");
    return Task.CompletedTask;
});

app.Run();
