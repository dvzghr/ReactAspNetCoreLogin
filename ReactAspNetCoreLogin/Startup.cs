using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReactAspNetCoreLogin.Service;

namespace ReactAspNetCoreLogin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<TokenService>();

            services.AddMvc();

            var secretKey = Encoding.ASCII.GetBytes("dotnetcoreoauthsecret");
            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                                                            {
                                                                ValidateIssuerSigningKey = true,
                                                                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                                                                ValidateIssuer = false,
                                                                ValidateAudience = false,
                                                                ValidateLifetime = true
                                                                //ClockSkew = TimeSpan.Zero
                                                            };
                        options.IncludeErrorDetails = true;
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                                            {
                                                HotModuleReplacement = true,
                                                ReactHotModuleReplacement = true
                                            });
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
