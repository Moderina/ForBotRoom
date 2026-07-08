using BotChat.Api.Controllers;
using BotChat.Api.Hubs;
using BotChat.App.BotLogic;
using BotChat.App.ChatLogic;
using BotChat.App.ConversationLogic;
using BotChat.App.DI;
using BotChat.App.LlmLogic;
using BotChat.App.Services;
using BotChat.App.UserLogic;
using BotChat.Infrastructure.DI;
using BotChat.Infrastructure.Persistant;
using BotChat.Infrastructure.Persistant.Repositories;

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

builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatMemberRepository, ChatMemberRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddScoped<IBotRepository, BotRepository>();
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<IChatNotifier, WebSocketChatNotifier>();
builder.Services.AddSingleton<IConversationQueue, ConversationQueue>();

builder.Services.AddSignalR();
builder.Services.AddHostedService<ConversationWorker>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await DbInit.InitializeAsync(db);


app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("AllowVueDev");
}

app.UseHttpsRedirection();
app.MapHub<ChatHub>("/chatHub");

app.Run();