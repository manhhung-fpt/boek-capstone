using Boek.Api;
using Boek.Api.AppStart;
using Boek.Api.Filters;
using Boek.Api.Middlewares;
using Boek.Core.Constants;
using Boek.Core.Data;
using Boek.Service.Hubs;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Reso.Core.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(o => o.LowercaseUrls = true);
builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(policy =>
    {
        policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(builder.Configuration.GetSection(MessageConstants.PROGRAM_CORS_ORIGINS).Get<string[]>())
        .AllowCredentials();
    });

    o.AddPolicy("AllOrigins", policy =>
    {
        policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});
//Dependency Injection
builder.Services.ConfigureDependencyInjection();
//Hosted services
builder.Services.HostedServices();
//AutoMapper
builder.Services.ConfigureAutoMapper();
//Redis
builder.Services.ConfigureStackExchangeRedis(builder.Configuration);
//Authen, Author
builder.Services.ConfigureAuthServices(builder.Configuration);
//Error Handling
builder.Services.ConfigureFilter<ErrorHandlingFilter>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<BoekCapstoneContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString(MessageConstants.BOEK_CONNECTION_STRING),
    option => option.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
);
//Swagger
builder.Services.ConfigureSwaggerServices();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});


if (builder.Environment.IsDevelopment())
{

}
else
{
    builder.Logging.ClearProviders();
    builder.Logging.AddFile(builder.Configuration.GetSection(MessageConstants.PROGRAM_FILE));
}
var app = builder.Build();
if (app.Environment.IsDevelopment())
{

}
else
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    app.UseMiddleware<LoggingMiddleware>();
}

//Firebase config
var pathToKey = Path.Combine(Directory.GetCurrentDirectory(), MessageConstants.PROGRAM_PATH_KEY_2, MessageConstants.PROGRAM_PATH_KEY_3);
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(pathToKey)
});

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseRouting();

app.UseCors();
//Swagger
SwaggerConfig.Configure(app);

//Authen, Author
AuthConfig.Configure(app);

app.UseSession();

//JWT
app.UseMiddleware<JwtMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationHub>(MessageConstants.NOTI_URL);
});

app.Run();
