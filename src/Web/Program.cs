using DepartmentProto;
using HrmBaharu.Application.Services;
using HrmBaharu.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

// Register gRPC Client
builder.Services.AddGrpcClient<DepartmentService.DepartmentServiceClient>(o =>
{
    o.Address = new Uri("https://localhost:5002"); // ServerService address
});

builder.Services.AddGrpcClient<CompanyService.CompanyServiceClient>(o =>
{
    o.Address = new Uri("https://localhost:5002"); // ServerService address
});

// (Optional) Register your own wrapper service
//builder.Services.AddScoped<DepartmentClientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/api"));

app.MapEndpoints();

app.Run();

public partial class Program { }
