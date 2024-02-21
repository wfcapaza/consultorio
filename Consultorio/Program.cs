using Consultorio.BusinessLogic.Service;
using Consultorio.DataAccess.Configuration;
using Consultorio.DataAccess.Repository;
using Consultorio.Entity;
using Dapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ConfigurationConnection>(builder.Configuration.GetSection("ConnectionStrings"));
DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true; //Indicar que solo será accedido por HTTP
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; //Indica que las cookies solo se enviaran a traves de conexiones seguras
        options.Cookie.SameSite = SameSiteMode.Strict;//Indica que el usario solo puede enviar peticiones de donde se solicito
        options.Cookie.IsEssential = true; //Indica que la cookie es esencial para el funcionamiento de la web
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.LoginPath = "/Login/IniciarSesion";
        options.AccessDeniedPath = "/Login/AccesoDenegado";
        options.SlidingExpiration = true; // Habilita la renovación de la cookie
    });

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(
        new ResponseCacheAttribute
        {
            NoStore = true,
            Location = ResponseCacheLocation.None
        }
    );
});

//Inyección de depencia
builder.Services.AddScoped<IAtencionRepository, AtencionRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IMedicamentoRepository, MedicamentoRepositorio>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IMedicamentoService, MedicamentoService>();
builder.Services.AddScoped<IAtencionService, AtencionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=IniciarSesion}/{id?}");

app.Run();
