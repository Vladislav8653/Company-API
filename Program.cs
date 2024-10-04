using CompanyEmployees.Extensions;
using LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using CompanyEmployees;
using NLog;
using Npgsql.Replication.PgOutput.Messages;

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters()
.AddCustomCSVFormatter();
builder.Services.ConfigureRepositoryManager();
builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();


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
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
