using Exchange.Api.Common.Filter;
using Exchange.Application.Commands.Conversion;
using Exchange.Application.Common.Dto;
using Exchange.Application.Interfaces;
using Exchange.Core.Domain;
using Exchange.Core.Shared;
using Exchange.Infrastructure.Services;
using Exchange.Infrastructure.Storage;
using Exchange.Infrastructure.Storage.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilter));
    options.Filters.Add(new ModelStateFilter());
}).AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<ConversionCommandValidation>();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();
builder.Services.AddDbContext<ExchangeDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("connection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Host.UseSerilog((ctx, lc) => lc
     .Enrich.FromLogContext()
        .WriteTo.Console(new RenderedCompactJsonFormatter())
        .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
        .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration.GetSection("IdentityUrl").Value;
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", builder.Configuration.GetSection("WriteApis").Value);
    });
});

var assembly = AppDomain.CurrentDomain.Load("Exchange.Application");
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(assembly);

builder.Services.AddHttpClient("CoinMarketApi", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetSection("CoinMarketApiUrl").Value);
    httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration.GetSection("CoinMarketApiKey").Value);
    httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");
});

builder.Services.AddTransient<ICoinCapMarketClient, CoinCapMarketClient>();
builder.Services.AddTransient<ICoinCapMarketClient, CoinCapMarketClient>();

builder.Services.Configure<CurrenciesSetting>(builder.Configuration.GetSection("CurrenciesSetting"));

var app = builder.Build();



using (var serviceScope = ((IApplicationBuilder)app).ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
{
    using (var context = serviceScope.ServiceProvider.GetService<ExchangeDbContext>())
    {
        context.Database.EnsureCreated();
    }
}

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
