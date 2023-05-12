using Boek.Core.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Boek.Api.AppStart
{
    public static class SwaggerConfig
    {
        [Obsolete]
        public static void ConfigureSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(MessageConstants.SWAGGER_CONFIG_SECURITY_NAME, new OpenApiSecurityScheme
                {
                    Description = MessageConstants.SWAGGER_CONFIG_SECURITY_SCHEME_DESC,
                    Name = MessageConstants.SWAGGER_CONFIG_SECURITY_SCHEME_NAME,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = MessageConstants.SWAGGER_CONFIG_SECURITY_NAME
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = MessageConstants.SWAGGER_CONFIG_SECURITY_NAME
                            },
                            Scheme = MessageConstants.SWAGGER_CONFIG_SECURITY_SCHEME,
                            Name = MessageConstants.SWAGGER_CONFIG_SECURITY_NAME,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BoekCapstone", Version = "v1" });
                var XmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var FullXmlFileName = Path.Combine(AppContext.BaseDirectory, XmlFilename);
                options.IncludeXmlComments(FullXmlFileName);
                options.EnableAnnotations();
            });
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BoekCapstone v1");
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
