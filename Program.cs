using CharityScanWebApp.Entities;
using CharityScanWebApp.Helpers;
using CharityScanWebApp.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<JsHelperService>();
builder.Services.AddScoped<BarcodeReaderService>();
builder.Services.AddScoped<APIService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddBootstrapBlazor();
builder.Services.AddMudServices();

// Add WebAPI
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<CharityscanDevContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("WebApiDatabase") ?? ""));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CharityScan API v1");
    c.DocumentTitle = "CharityScanAPI v1";
    c.RoutePrefix = "api";
}
);

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseAuthorization();
app.MapControllers();

app.Run();
