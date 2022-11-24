using Blazored.Toast;
using BMC.Security.Blazor.Data;
using BMC.Security.Blazor.Helpers;
using BMC.Security.Tools;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Azure.Devices;
using ServiceStack;
using ServiceStack.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET, PATCH, DELETE, PUT, POST, OPTIONS"));
});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<MqttService>();
builder.Services.AddBlazoredToast();
builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.MaximumReceiveMessageSize = 128 * 1024; // 1MB
});
var configBuilder = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false);
IConfiguration Configuration = configBuilder.Build();
AppConstants.BlobConString = Configuration["ConnectionStrings:BlobConString"];
AppConstants.IoTCon = Configuration["ConnectionStrings:IoTCon"];
MqttInfo.MqttHost = Configuration["MqttInfo:MqttHost"];
MqttInfo.MqttUser = Configuration["MqttInfo:MqttUser"];
MqttInfo.MqttPass = Configuration["MqttInfo:MqttPass"];
AppConstants.RedisCon = Configuration["ConnectionStrings:RedisCon"];
AppConstants.AdminAccounts = Configuration.GetSection("AdminAccount").Get<List<Account>>();


// ******
// BLAZOR COOKIE Auth Code (begin)
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
// BLAZOR COOKIE Auth Code (end)
// ******
// ******
// BLAZOR COOKIE Auth Code (begin)
// From: https://github.com/aspnet/Blazor/issues/1554
// HttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<HttpContextAccessor>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddSingleton(new RedisManagerPool(AppConstants.RedisCon));

// BLAZOR COOKIE Auth Code (end)
// ******

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting(); 
app.UseCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true) // allow any origin  
           .AllowCredentials());               // allow credentials 

// ******
// BLAZOR COOKIE Auth Code (begin)
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
// BLAZOR COOKIE Auth Code (end)
// ******


//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");

app.UseEndpoints(endpoints =>
{
    // BLAZOR COOKIE Auth Code (begin)
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
    // BLAZOR COOKIE Auth Code (end)
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

Encryption enc = new Encryption();
var pass = enc.Decrypt("PyudoE0InirdNemJJ4aOSw==");
Console.WriteLine(pass);
app.Run();


