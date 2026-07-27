using System.Text.Json;
using BotChat.Api.Hubs;
using BotChat.App.DI;
using BotChat.App.LlmLogic;
using BotChat.App.RespondLogic;
using BotChat.Infrastructure.DI;
using BotChat.Infrastructure.Persistant;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowVueDev", policy =>
    {
        policy.WithOrigins(["http://localhost:5174", "http://192.168.100.3:5174"]) 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddOpenApi();
builder.Services.AddHttpClient<ILlmService, LlamaCppService>(client =>
{
    client.BaseAddress =
        new Uri("http://localhost:11435");
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy =
            JsonNamingPolicy.CamelCase;

        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IChatNotifier, WebSocketChatNotifier>();

builder.Services.AddSignalR();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await DbInit.InitializeAsync(db);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("AllowVueDev");
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

app.MapHub<ChatHub>("/chatHub");

app.Run();