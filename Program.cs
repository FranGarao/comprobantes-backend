using comprobantes_back.Models;
using comprobantes_back.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Injection of services
builder.Services.AddSingleton<ICommonService<Invoice>, InvoiceService>();
builder.Services.AddSingleton<ICommonService<Job>, JobService>();
builder.Services.AddSingleton<ICommonService<Customer>, CustomerService>();
builder.Services.AddSingleton<ILoginService<User>, UserService>();

// Obtain the secret key from configuration
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret") ?? "askop[owike90234812opk@@#%$^$idouj23--32193jdaijhd";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Add services to the container
builder.Services.AddControllers();

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline

// Enable Swagger only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS policy
app.UseCors("AllowAll");

// Support for authentication
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
