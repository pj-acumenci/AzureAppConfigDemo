using AzureAppConfigDemo.Api.Features.Common;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppConfigFeature();
builder.Services.AddAuthFeature();
builder.Services.AddControllers();
builder.Services.AddCorsFeature();
builder.Services.AddSwaggerFeature();

var app = builder.Build();
app.UseAppConfigFeature();
app.UseSwaggerFeature();
app.UseHttpsRedirection();
app.UseCorsFeature();
app.UseAuthFeature();
app.MapControllers().RequireAuthorization();
app.Run();
