using JekirdekCrm.Application.Authentication;
using JekirdekCrm.Application.Services;
using JekirdekCrm.CrossCutting.Mapper;
using JekirdekCrm.Domain.Interface.Authentication;
using JekirdekCrm.Domain.Interface.Repositories;
using JekirdekCrm.Domain.Interface.Services;
using JekirdekCrm.Infrastructure.Context;
using JekirdekCrm.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//DbContext Connection Bilgisi Appsettings.jsonda Verilmiþtir
//Infrastructure Katmanýnda Oluþturduðumuz Context ile Baðdaþtýrýldý
//Repolarýn Ctorlarýnda Bulunmalýdýr
builder.Services.AddDbContext<JekirdekCrmDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("JekirdekCrmDbConnection")));

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
