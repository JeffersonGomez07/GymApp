using BL;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuracion de Api para enviar emails 
builder.Services.Configure<BrevoSettings>(builder.Configuration.GetSection("Brevo"));
builder.Services.AddScoped<EmailManager>();

//configuracion del cors policy 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCorsPolicy",
        policy =>
        {
            policy.AllowAnyOrigin(); //www.mypage.com, www.mypage.net, etc
            policy.AllowAnyHeader(); //application/json  application/xml application/text
            policy.AllowAnyMethod(); //GET, POST, PUT, DELETE
        });
});

// Configuracion del uso de sesiones
builder.Services.AddDistributedMemoryCache(); // Necesario para la sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Configura el tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Configuración de seguridad para cookies
    options.Cookie.IsEssential = true; // Necesario para GDPR compliance
});

// Agregar Razor Pages, MVC, etc.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();

app.UseCors("MyCorsPolicy");

app.UseSession(); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

