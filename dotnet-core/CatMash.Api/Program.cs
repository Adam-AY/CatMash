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
    options.AddPolicy("AllowCatMashClient", policy =>
    {
        var allowedOrigin = builder.Configuration["Cors:AllowedOrigins"];

        if (string.IsNullOrEmpty(allowedOrigin))
        {
            throw new Exception("CORS origin is not configured");
        }

        policy.WithOrigins(allowedOrigin)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowCatMashClient");

app.MapHub<NotificationHub>("/notificationHub");

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var catService = scope.ServiceProvider.GetRequiredService<CatService>();

    try
    {
        await catService.InitializeAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading cats: {ex.Message}");
    }
}

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
    service.Vote(vote);
    var winners = service.GetWinners();
    var totalVotes = service.GetTotalVotes();

    await hubContext.Clients.All.SendAsync("VoteIncremented", totalVotes);
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