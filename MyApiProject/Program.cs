using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyApiProject.Services;
using MyApiProject_Application.Services;
using MyApiProject_Core.Interfaces;
using MyApiProject_Infrastructure.Database;
using MyApiProject_Infrastructure.Repositories;
using Microsoft.OpenApi.Models;  
using System.Text;

var builder = WebApplication.CreateBuilder(args);






// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);


// DI
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<Db>();
builder.Services.AddScoped<IUrunRepository, UrunRepository>(); // interface üzerinden
builder.Services.AddScoped<UrunService>();


 
builder.Services.AddEndpointsApiExplorer();




// --- 3. JWT CONFIGURATION ---

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);



// 2. Key nesnesini oluştur
var securityKey = new SymmetricSecurityKey(key);



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),

        // 🔥 Rol ve İsim eşleşmesi için hayati önem taşır
        RoleClaimType = System.Security.Claims.ClaimTypes.Role,
        NameClaimType = System.Security.Claims.ClaimTypes.Name
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            // Token neden kabul edilmedi? (Süresi mi doldu? Key mi yanlış?)
            Console.WriteLine("HATA: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            // Token başarıyla geçti!
            Console.WriteLine("Token doğrulandı!");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

// --- 4. SWAGGER (Kilit Butonu İçin) ---
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // Küçük harf yapıldı
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Sadece Token'ı girin (Örn: eyJ...)"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" // Definition'daki isimle aynı
                }
            },
            Array.Empty<string>()
        }
    });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // 🔥 önce bu
app.UseAuthorization();

app.MapControllers();

app.Run();