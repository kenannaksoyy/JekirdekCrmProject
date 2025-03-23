using JekirdekCrm.Application.Authentication;
using JekirdekCrm.Application.Services;
using JekirdekCrm.CrossCutting.Mapper;
using JekirdekCrm.Domain.Interface.Authentication;
using JekirdekCrm.Domain.Interface.Repositories;
using JekirdekCrm.Domain.Interface.Services;
using JekirdekCrm.Infrastructure.Context;
using JekirdekCrm.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//DbContext Connection Bilgisi Appsettings.jsonda Verilmiþtir
//Infrastructure Katmanýnda Oluþturduðumuz Context ile Baðdaþtýrýldý
//Repolarýn Ctorlarýnda Bulunmalýdýr
builder.Services.AddDbContext<JekirdekCrmDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("JekirdekCrmDbConnection")));


//Authentication Ekliyoruz
builder.Services.AddAuthentication(options =>
{
    //Þemamýzý Belirttik Dafeult Olarak Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    //Þema Sonrasý JWTBearer Burada Ekliyoruz appsettings.jsonda JWTKey Özelliðinin Altýnda Crenler(Sahip, Ýster, Secret Key) Bulunmaktadýr
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
            ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
        };
    });

//Automapperýmýzý Belirttik
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dependency Injection ile Yaþam Döngüleri Tek Noktada Saðladýk
//Burada Servis ve Repolarýn Ctorlarýndaki Soyutlarýn Tek Noktada Yönetimi Saðladýk
//Transient ne zaman talep edilirse yeniden oluþur
//Scoped farklý isteklerde farklý üretiler
//Singleton proje yaþadýkca sadece bir defa üretilir
//Maliyet Transient>Scoped>Singleton
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Sadece Localhost 3000 den Gelecek Ýsteklere Göre Ayarlandý 3001 Olursa Olmaz
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowLocalhost3000");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
