using CatMash.Api;
using CatMash.Api.Hubs;
using CatMash.Api.Models;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<CatService>();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCatMashClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

app.UseCors("AllowCatMashClient");

app.MapHub<NotificationHub>("/notificationHub");

app.UseHttpsRedirection();

#region Endpoints

app.MapGet("/cats", (CatService service) =>
{
    var winners = service.GetWinners();

    var cats = service.Cats
        .Where(c => !winners.Any(w => w.Id == c.Id))
        .OrderByDescending(c => c.Score)
        .ToList();

    return cats;
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

app.MapPost("/cats/vote", async (Vote vote, CatService service, IHubContext<NotificationHub> hubContext) =>
{
    var winner = service.Cats.FirstOrDefault(c => c.Id == vote.WinnerId);
    var loser = service.Cats.FirstOrDefault(c => c.Id == vote.LoserId);

    if (winner == null || loser == null)
    {
        return Results.NotFound("Cat not found");
    }

    winner.Score += 1;

    await hubContext.Clients.All.SendAsync("VoteIncremented");

    var winners = service.GetWinners();
    await hubContext.Clients.All.SendAsync("WinnersUpdated", winners);

    return Results.Ok();
});

app.MapGet("/cats/winners", (CatService service) =>
{
    var winners = service.GetWinners();
    return Results.Ok(winners);
});

app.MapGet("/cats/totalVotes", (CatService service) =>
{
    var totalVotes = service.GetTotalVotes();
    return Results.Ok(totalVotes);
});

#endregion

app.Run();