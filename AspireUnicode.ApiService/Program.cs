using System.Text.Encodings.Web;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// var unicode
app.MapGet(
        "/",
        (ILogger<Program> logger) =>
        {
            SampleRecord record = new("åäöÅÄÖ");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            var json = JsonSerializer.Serialize(record, options: options);
            logger.LogInformation("Unicode characters: {unicode}", json);
            return "ÄÖÅ";
        }
    )
    .WithName("SampleEndpoint");

app.MapDefaultEndpoints();

app.Run();

record SampleRecord(string Name);
