using InterviewScheduler.API.Configuration;
using InterviewScheduler.API.HttpService;
using InterviewScheduler.Repository;
using InterviewScheduler.Repository.Context;
using InterviewScheduler.Service.BookingService;
using InterviewScheduler.Service.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenTelemetry.Metrics;
using Polly;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
MapperConfig.ConfigMapster();
builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddControllers();
string connectionString = builder?.Configuration.GetConnectionString("DefaultConnection"); // "Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=admin;Password=admin;";
builder.Services.AddDbContext<InterviewSchedulerDbContext>(options=>options.UseNpgsql(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//------Logging Configuration----
Log.Logger=new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Log/log.txt")
    .CreateLogger();
builder.Host.UseSerilog();
//---------Open Telemetry, for Health metrics--------
builder.Services.AddOpenTelemetry()
    .WithMetrics(x => {
        x.AddPrometheusExporter();
        x.AddMeter("Microsoft.AspNetCore.Hosting",
            "Microsoft.AspNetCore.Server.Kestrel");
        x.AddView("request-duration",new ExplicitBucketHistogramConfiguration
        {
            Boundaries = new []{0,0.005,0.01,0.025,0.075,0.1,0.250,0.5,0.75}
        });
    });
//-----------Graceful Shutdown------
builder.WebHost.UseShutdownTimeout(TimeSpan.FromSeconds(30));

//-----Resilience------
builder.Services.AddHttpClient<IWeatherService, WeatherService>(c=>
{
    c.BaseAddress = new Uri("https://api.weatherapi.com/v1/current.json");
}).AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(3))); ;

//Add MediatR assemblies
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(GetAllBookingQueries).Assembly));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<InterviewSchedulerDbContext>();
        //db.Database.Migrate();
    }
}
app.MapPrometheusScrapingEndpoint();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseAuthorization();

app.MapControllers();

app.Run();
