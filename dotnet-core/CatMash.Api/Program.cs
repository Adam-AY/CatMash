using CatMash.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<CatService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/cats", (CatService service) =>
{
    return service.Cats;
});

app.Run();