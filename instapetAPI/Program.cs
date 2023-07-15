
using instapetService.Configs;
using instapetService.Interfaces;
using instapetService.Repositories;
using instapetService.Services;
using instapetService.Util;
using Microsoft.EntityFrameworkCore;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000","http://www.contoso.com").AllowAnyHeader().AllowAnyMethod();
                      });
});

// Add services to the container.

builder.Services.Configure<AwsConfig>(builder.Configuration.GetSection("AWSConfig"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILoginRepo, LoginRepo>();
builder.Services.AddScoped<IFollowRepo, FollowRepo>();
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IImageRepo, ImageRepo>();
builder.Services.AddScoped<ISearchRepo, SearchRepo>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ISearchService, SearchService>();
builder.Services.AddTransient<IFollowService, FollowService>();
builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddDbContext<InstaPetContext>(options =>
{
    var conn = builder.Configuration.GetConnectionString("instapetDB");
    options.UseMySql(conn, ServerVersion.AutoDetect(conn));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
