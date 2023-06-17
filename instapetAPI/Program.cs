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

ImageDirConfig config = new ImageDirConfig() { ImageDir = builder.Configuration.GetValue<string>("ImageDirectorySetting:Directory") };
builder.Services.AddSingleton<ImageDirConfig>(config);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILoginRepo, LoginRepo>();
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IImageRepo, ImageRepo>();
builder.Services.AddScoped<ISearchRepo, SearchRepo>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ISearchService, SearchService>();
builder.Services.AddDbContext<InstaPetContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("instapetDB"));
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
