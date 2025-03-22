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

//DbContext Connection Bilgisi Appsettings.jsonda Verilmi�tir
//Infrastructure Katman�nda Olu�turdu�umuz Context ile Ba�da�t�r�ld�
//Repolar�n Ctorlar�nda Bulunmal�d�r
builder.Services.AddDbContext<JekirdekCrmDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("JekirdekCrmDbConnection")));

//Automapper�m�z� Belirttik
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dependency Injection ile Ya�am D�ng�leri Tek Noktada Sa�lad�k
//Burada Servis ve Repolar�n Ctorlar�ndaki Soyutlar�n Tek Noktada Y�netimi Sa�lad�k
//Transient ne zaman talep edilirse yeniden olu�ur
//Scoped farkl� isteklerde farkl� �retiler
//Singleton proje ya�ad�kca sadece bir defa �retilir
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
