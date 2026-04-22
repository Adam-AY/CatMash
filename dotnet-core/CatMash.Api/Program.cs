using CatMash.Api;
using CatMash.Api.Models;

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
    return service.Cats.OrderByDescending(c => c.Score).ToList();
});


app.MapGet("/cats/random", (CatService service) =>
{
    var rnd = new Random();

    var cats = service.Cats
        .OrderBy(x => rnd.Next())
        .Take(2)
        .Distinct()
        .ToList();

    return cats;
});


app.MapPost("/cats/vote", (Vote vote, CatService service) =>
{
    var winner = service.Cats.FirstOrDefault(c => c.Id == vote.WinnerId);
    var loser = service.Cats.FirstOrDefault(c => c.Id == vote.LoserId);

    if (winner == null || loser == null)
        return Results.NotFound("Cat not found");

    winner.Score += 1;

    return Results.Ok();
});

app.Run();