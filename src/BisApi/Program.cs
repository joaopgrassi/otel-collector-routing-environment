using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var resource = ResourceBuilder.CreateEmpty()
    .AddService($"bis.api.{builder.Environment.EnvironmentName}")
    .AddTelemetrySdk()
    .AddAttributes(new[] { new KeyValuePair<string, object>("deployment.environment", builder.Environment.EnvironmentName) });

builder.Services.AddOpenTelemetryTracing(options =>
{
    options
        .SetResourceBuilder(resource)
        .SetSampler(new AlwaysOnSampler())
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddOtlpExporter(o =>
        {
            // The collector is running together with the APIs
            // inside docker-compose
            o.Endpoint = new Uri("http://otel-collector:4317");
            o.ExportProcessorType = ExportProcessorType.Simple;
        });
});

var app = builder.Build();
app.MapControllers();
app.Run();
