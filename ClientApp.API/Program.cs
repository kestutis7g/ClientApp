using Autofac;
using Autofac.Extensions.DependencyInjection;
using ClientApp.API;
using ClientApp.Core;
using ClientApp.Infrastructure;
using ClientApp.Infrastructure.Data;
using ClientApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog(new NLogAspNetCoreOptions
{
    RemoveLoggerFactoryFilter = false,
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("Db")!;

builder.Services.AddAppDbContext(connectionString!);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration.GetSection("PostitApiUrl").Value ?? string.Empty)
    }; 
    return client;
});

builder.Services.AddCors();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientApp.API", Version = "v1" });
    c.EnableAnnotations();
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new CoreDIModule());
    containerBuilder.RegisterModule(new InfrastructureDIModule());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseRouting();

app.MapControllers();

MigrateDb(app);

app.Run();

static void MigrateDb(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<DatabaseContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred migrating the database.");
        }
    }
}
