using Asp.Versioning.Routing;
using AspNetCoreRateLimit;
using CompanyEmployees.Extensions;
using LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using CompanyEmployees;
using CompanyEmployees.ActionFilters;
using CompanyEmployees.Utility;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Repository.DataShaping;

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile {Duration = 120});

}).AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters()
    .AddCustomCSVFormatter();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.ConfigureRepositoryManager();
builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<ValidateCompanyExistsAttribute>();
builder.Services.AddScoped<ValidateEmployeeForCompanyExistsAttribute>();
builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
builder.Services.AddCustomMediaTypes();
builder.Services.AddScoped<ValidateMediaTypeAttribute>();
builder.Services.AddScoped<EmployeeLinks>();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureHttpCacheHeaders();
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
//builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.ConfigureExceptionHandler(new LoggerManager()); // я не уверен что это верно
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseResponseCaching();
app.UseHttpCacheHeaders();
app.UseIpRateLimiting();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
