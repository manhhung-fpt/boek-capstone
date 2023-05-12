using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Boek.Core.Constants;

namespace Boek.Api.AppStart
{
    public static class AuthConfig
    {
        public static void ConfigureAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfig = configuration.GetSection(MessageConstants.AUTH_CONFIG_TOKEN);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        //auto gen
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        //sign token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query[MessageConstants.NOTI_ACCESS_TOKEN];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments(MessageConstants.NOTI_URL)))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                }

                );

            services.AddAuthorization(config =>
            {
                config.AddPolicy(MessageConstants.AUTH_CONFIG_POLICY_NAME, policyBuilder =>
                    policyBuilder.RequireClaim(MessageConstants.AUTH_CONFIG_POLICY_CLAIM_ROLE)
                );
            });
            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }

    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RolesAuthorizationRequirement requirement)
        {
            if (context.User.Identity != null && !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var validRole = false;
            if (requirement.AllowedRoles.Any() == false)
            {
                validRole = true;
            }
            else
            {
                var claims = context.User.Claims;
                var userRoles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                var roles = requirement.AllowedRoles;

                validRole = roles.Any(r => userRoles.Contains(r));
            }

            if (validRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}