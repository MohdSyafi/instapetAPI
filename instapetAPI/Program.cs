using instapetService.Configs;
using instapetService.Repositories;
using instapetService.Services;
using instapetService.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

AwsConfig config = new AwsConfig() 
{ 
    S3ImageDir = builder.Configuration.GetValue<string>("AWSConfig:Directory"),
    accessKey = builder.Configuration.GetValue<string>("AWSConfig:AccessKey"),
    accessSecret = builder.Configuration.GetValue<string>("AWSConfig:AccessSecretKey"),
    S3Bucket = builder.Configuration.GetValue<string>("AWSConfig:S3BucketName"),
};
builder.Services.AddSingleton<AwsConfig>(config);
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
