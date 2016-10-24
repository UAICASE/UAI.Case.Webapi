using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using UAI.Case.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Diagnostics;
using UAI.Case.Boot;
using StructureMap;
using Newtonsoft.Json.Linq;
using UAI.Case.EFProvider;
using System.IO;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Webapi
{
    public class Startup
    {

        private ICreds getCredsFromComposePostgreeSQL(string uri)
        {
            Creds cr = new Creds();



            return cr;
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("vcap-local.json", optional: false, reloadOnChange:true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            ICreds creds;
            string vcapServices = System.Environment.GetEnvironmentVariable("VCAP_SERVICES");
            if (vcapServices != null)
            {
                dynamic json = JsonConvert.DeserializeObject(vcapServices);

             
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
              


                Configuration["pgsql:0:credentials:username"] = "localhost:27017";
                Configuration["pgsql:0:credentials:password"] = "admin";
                Configuration["pgsql:0:credentials:host"] = "password";
                Configuration["pgsql:0:credentials:cs"] = "password";

            }
            else
            {
                string cs= Configuration["local_db:cs"];
                Configuration["pgsql:0:credentials:cs"] = cs;
            }





        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {


            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddMvc().AddJsonOptions(opt => {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());

                var resolver = opt.SerializerSettings.ContractResolver;


                if (resolver != null)
                {
                    var res = resolver as DefaultContractResolver;
                    res.NamingStrategy = null;  // <<!-- this removes the camelcasing
                }
            });
            services.AddSignalR(options => {
                options.Hubs.EnableDetailedErrors = true;
                options.EnableJSONP = true;
            });

            services.Configure<MvcOptions>(options => {
                var s = new JsonSerializerSettings();
            });

            //var cs= Configuration["Data:DefaultConnection:ConnectionString"];


            var creds = new Creds()
            {
                username = Configuration["pgsql:0:credentials:username"],
                password = Configuration["pgsql:0:credentials:password"],
                host = Configuration["pgsql:0:credentials:host"],
                cs = Configuration["pgsql:0:credentials:cs"]
            };





            var container = Booter.Run(creds);
            container.Populate(services);
             return container.GetInstance<IServiceProvider>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
                    // This should be much more intelligent - at the moment only expired 
                    // security tokens are caught - might be worth checking other possible 
                    // exceptions such as an invalid signature.
                    if (error != null && (error.Error is SecurityTokenExpiredException || error.Error is SecurityTokenInvalidSignatureException))
                    {
                        context.Response.StatusCode = 401;
                        // What you choose to return here is up to you, in this case a simple 
                        // bit of JSON to say you're no longer authenticated.
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(
                            JsonConvert.SerializeObject(
                                new { authenticated = false, tokenExpired = true }));
                    }
                    else if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;

                        if (error.Error.Source == "Microsoft.AspNet.Authentication.JwtBearer")
                            context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        // TODO: Shouldn't pass the exception message straight out, change this.
                        await context.Response.WriteAsync(
                            JsonConvert.SerializeObject
                            (new { success = false, error = error.Error.Message }));
                    }
                    // We're not trying to handle anything else so just let the default 
                    // handler handle.
                    else await next();
                });
            });




            var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {

                IssuerSigningKey = Keys.RSAKey,
                ValidAudience = TokenHandler.JWT_TOKEN_AUDIENCE,
                ValidIssuer = TokenHandler.JWT_TOKEN_ISSUER,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(0) //TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseStaticFiles();
            app.UseWebSockets();

            app.Map("/signalr", map =>
            {
                map.UseCors(opt =>
            opt.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );
                map.RunSignalR();
            });

            app.UseCors(opt =>
            opt.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

             app.UseSignalR();



            app.UseMvc();
        }



        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                        .UseKestrel()
                         .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseConfiguration(config)
                        .UseStartup<Startup>()
                        .Build();

            host.Run();
        }
    }




}
