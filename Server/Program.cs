using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.EntityFrameworkCore;
using SETraining.Server;
using SETraining.Server.Contexts;
using SETraining.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SETrainingContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("SETrainingDB")));

builder.Services.AddScoped<ISETrainingContext, SETrainingContext>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IUploadRepository, UploadRepository>();
builder.Services.AddScoped<IProgrammingLanguagesRepository, ProgrammingLanguagesRepository>();
builder.Services.AddSwaggerGen();

var blobContainerConns = builder.Configuration.GetConnectionString("AzureBlob");
builder.Services.AddScoped<BlobContainerClient>(_ => new BlobContainerClient(blobContainerConns, "setrainingupload"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Seed();

app.Run();
