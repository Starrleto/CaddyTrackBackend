using CaddyTrack.Hubs;
using CaddyTrack.Services;
using CaddyTrack.Services.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ChatroomService>();
builder.Services.AddScoped<TrackerService>();
builder.Services.AddSignalR();

// Allows connection to a database
var connectionString = builder.Configuration.GetConnectionString("SillyConnection");

builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));

builder.Services.AddCors(Options => {
    Options.AddPolicy("BlogPolicy",
    builder => {
        builder.WithOrigins("http://localhost:5211, http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
