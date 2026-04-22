using CatMash.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<CatService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCatMashClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowCatMashClient");

app.UseHttpsRedirection();

app.MapGet("/cats", (CatService service) =>
{
    return service.Cats;
});

app.Run();