using CaddyTrack.Hubs;
using CaddyTrack.Services;
using CaddyTrack.Services.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ChatroomService>();
builder.Services.AddScoped<TrackerService>();

// Allows connection to a database
var connectionString = builder.Configuration.GetConnectionString("SillyConnection");

builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));

builder.Services.AddCors(Options => {
    Options.AddPolicy("BlogPolicy",
    builder => {
        builder.WithOrigins("http://localhost:5211, http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SharedDB>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("BlogPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>(pattern:"/Chat");

app.Run();
