using e_commerce.Server.Data;
using e_commerce.Server.Mappings;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Services;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.uniOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found. ");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));



// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Configure password requirements
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
});

// Add JWT configuration
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

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
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register generic repositories and services
/*builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(ServiceBase<,>));*/
// Add UnitOfWork, Repositories and Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>(); 
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
public partial class Program { }
