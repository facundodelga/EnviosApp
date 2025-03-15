using EnviosApp.Models;
using EnviosApp.Repository;
using EnviosApp.Repository.Implementation;
using EnviosApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Log para debugear
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDbContext<EnviosDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HomeBankingConexion")));

//inyeccion de independencias repositorios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IZoneRepository, ZoneRepository>();
builder.Services.AddScoped<IZoneCountryRepository, ZoneCountryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();

//inyeccion de independencias servicios
builder.Services.AddScoped<IProviderService, ProviderService>();

// Configuración de CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowSpecificOrigin",
        builder => {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

//Servicios de autenticacion
var jwtSecretKey = builder.Configuration.GetConnectionString("Jwt-Key");
if (string.IsNullOrEmpty(jwtSecretKey)) {
    throw new ArgumentNullException(nameof(jwtSecretKey), "La clave secreta JWT no puede ser nula o vacía.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

    opt.RequireHttpsMetadata = false;

    opt.TokenValidationParameters = new TokenValidationParameters() {
        IssuerSigningKey = signingKey,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero //asegurar que no haya tolerancia para diferencias de tiempo entre el emisor y el validador del token
    };

    opt.Events = new JwtBearerEvents {
        OnTokenValidated = context => {
            var claims = context.Principal?.Claims;
            if (claims != null) {
                Console.WriteLine("Claims después de validación:");
                foreach (var claim in claims) {
                    Console.WriteLine($"Claim validado - Type: {claim.Type}, Value: {claim.Value}");
                }
            }
            return Task.CompletedTask;
        }
    };

});

//Agrego servicios de autorizacion
builder.Services.AddAuthorization(options => {
    options.AddPolicy("userOnly", policy => policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, "user", "admin"));
    options.AddPolicy("adminOnly", policy => policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {

    //Aqui obtenemos todos los services registrados en la App
    var services = scope.ServiceProvider;
    try {

        // En este paso buscamos un service que este con la clase HomeBankingContext
        var context = services.GetRequiredService<EnviosDBContext>();
        EnviosDBContext.Initialize(context);
    }
    catch (Exception ex) {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ha ocurrido un error al enviar la información a la base de datos!");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
}
else {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDefaultFiles(); // Esto buscará archivos como index.html de forma predeterminada
app.UseStaticFiles();
app.MapControllers();

app.UseRouting();
app.UseCors("AllowSpecificOrigin"); // Habilitar CORS con la política específica
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
