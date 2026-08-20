using TvShowsHub.Application.Extensions;
using TvShowsHub.TvMazeClient.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddHttpClient("TvMaze", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["TvMazeUrl"] 
                                     ?? throw new InvalidOperationException("TvMazeUrl app setting is missing"));
    httpClient.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddExternalClients();
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();