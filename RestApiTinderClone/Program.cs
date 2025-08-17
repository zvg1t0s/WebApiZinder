using RestApiTinderClone.Data;
using RestApiTinderClone.Services;

using Amazon;
using Amazon.S3;
using RestApiTinderClone.Tools;
using RestApiTinderClone.Tools.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IPhotosService, AS3PhotosService>();
builder.Services.AddTransient<IUsersService, UserService>();
builder.Services.AddTransient<IMatchesService, MatchesService>();
builder.Services.AddTransient<IJWTProvider, JwtProvider>();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddDbContext<TinderDataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});




//kkk
var app = builder.Build();
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<TinderDataContext>();
await dbContext.Database.EnsureCreatedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
