using EAFCCoinsManager;
using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using EAFCCoinsManager.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ConnectionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddSwaggerGen(c =>
{

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
    {
        new OpenApiSecurityScheme
        {
        Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
        }
    });


});
builder.Services.AddTransient<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddTransient<ICarrinhoRepository, CarrinhoRepository>();
builder.Services.AddTransient<IMoedaRepository, MoedasRepository>();
builder.Services.AddTransient<IPlataformaRepository, PlataformaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddTransient<IItemPedido, ItemPedidoRepository>();
builder.Services.AddTransient<PagamentoRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "MyPolicy",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    );
});

var key = Encoding.ASCII.GetBytes(Key.Secret);




builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();

app.MapControllers();

app.Run();
