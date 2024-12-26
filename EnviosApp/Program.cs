using EnviosApp.Models;
using EnviosApp.Repository;
using EnviosApp.Repository.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EnviosDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HomeBankingConexion")));

//inyeccion de independencias
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Servicios de autenticacion
var jwtSecretKey = builder.Configuration.GetConnectionString("Jwt-Key");
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

});

//Agrego servicios de autorizacion
builder.Services.AddAuthorization(options => {
    options.AddPolicy("userOnly", policy => policy.RequireClaim("user"));
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("admin"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {

    //Aqui obtenemos todos los services registrados en la App
    var services = scope.ServiceProvider;
    try {

        // En este paso buscamos un service que este con la clase HomeBankingContext
        var context = services.GetRequiredService<EnviosDBContext>();
        //DBInitializer.Initialize(context);
    }
    catch (Exception ex) {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ha ocurrido un error al enviar la informaci�n a la base de datos!");
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
app.UseStaticFiles();
app.MapControllers();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();

app.Run();
