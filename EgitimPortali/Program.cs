using EgitimPortali.Context;
using EgitimPortali.Models;
using EgitimPortali.Repository.Anasayfa;
using EgitimPortali.Repository.Ders;
using EgitimPortali.Repository.DersIcerik;
using EgitimPortali.Repository.DersTakipleri;
using EgitimPortali.Repository.Hakkýmýzda;
using EgitimPortali.Repository.Iletisimler;
using EgitimPortali.Repository.Kategori;
using EgitimPortali.Repository.Konu;
using EgitimPortali.Repository.Kullanici;
using EgitimPortali.Repository.KullaniciRol;
using EgitimPortali.Repository.Reklam;
using EgitimPortali.Repository.Rol;
using EgitimPortali.Repository.Soru;
using EgitimPortali.Repository.SoruCevap;
using EgitimPortali.Repository.TestCevaplari;
using EgitimPortali.Repository.Testler;
using EgitimPortali.Repository.TestSorulari;
using EgitimPortali.Repository.Yorum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IKategorilerRepository, KategorilerRepository>();
builder.Services.AddScoped<IDerslerRepository, DerslerRepository>();
builder.Services.AddScoped<IDersIcerikRepository, DersIcerikRepository>();
builder.Services.AddScoped<IKonularRepository, KonularRepository>();
builder.Services.AddScoped<IAnasayfaRepository, AnasayfaRepository>();
builder.Services.AddScoped<IHakkimizdaRepository, HakkimizdaRepository>();
builder.Services.AddScoped<IReklamRepository, ReklamRepository>();
builder.Services.AddScoped<IYorumRepository, YorumRepository>();
builder.Services.AddScoped<ISoruRepository, SoruRepository>();
builder.Services.AddScoped<ISoruCevapRepository, SoruCevapRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IIletisimRepository, IletisimRepository>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IKullaniciRolRepository, KullaniciRolRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<ITestSoruRepository, TestSoruRepository>();
builder.Services.AddScoped<ITestCevapRepository, TestCevapRepository>();
builder.Services.AddScoped<IDersTakipRepository, DersTakipRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/GirisYap";
        options.AccessDeniedPath = "/auth/accessdenied";
        options.Cookie.IsEssential = true;
        options.SlidingExpiration = true;
        options.Cookie.Name = $".Uygulama.auth";   // todo : deðiþtirin.

        options.Cookie.HttpOnly = true;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddSession(options =>
{
    options.Cookie.Name = $"Uygulama.auth"; // todo : deðiþtirin.
    options.IdleTimeout = TimeSpan.FromMinutes(180);
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<SqlServerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
