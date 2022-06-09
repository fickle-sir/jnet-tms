using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace JNet.Tms
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .Build()
                .Run();
        }

        public Startup(IConfiguration configuration)
        {
            App.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMemoryCache()
                .AddHttpContextAccessor()
                .AddDbContext<DbContext, AppDbContext>(options =>
                {
                    options.UseSqlServer(App.Configuration.GetConnectionString("Conn"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    AppDbContext.Options = options.Options as DbContextOptions<AppDbContext>;
                })
                .AddScoped<IEntityPropertyProvider, EntIdProvider>()
                .AddNjTable()
                .AddModelMetadataXProvider()
                .AddControllers(options =>
                {
                    options.ModelMetadataDetailsProviders.Add(new RequiredBindingMetadataProvider());
                    options.Filters.Add(new UnifiedFilter());
                    options.ValueProviderFactories.Add(new JsonValueProviderFactory(options));
                })
                .AddEntityServices(options =>
                {
                    options.AddRouteTemplate("[controller]/[action]");
                })
                .AddJsonOptions()
                .AddAuthorizedUser()
                .AddAppModule()
                .ProvideDbContextEntities()
                .AddXLocalizer();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = JwtParameters.Issuer,
                        ValidAudience = JwtParameters.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtParameters.SigningKey))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });

            services.AddCors(options => options.AddPolicy("AllowAny", policy => policy.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            App.Initialize(app.ApplicationServices);

            if (env.IsProduction())
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                //app.UseDeveloperExceptionPage();
                app.UseMiddleware<DeveloperExceptionPageMiddlewareX>();
                app.UseCors("AllowAny");
            }

            //app.UseHttpsRedirection();

            // for angular
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404 &&
            //       !Path.HasExtension(context.Request.Path.Value) &&
            //       (context.Request.Path.Value.EndsWith("/f") || context.Request.Path.Value.StartsWith("/f/"))
            //        )
            //    {
            //        context.Request.Path = "/f/index.html";
            //        await next();
            //    }
            //});

            //var contentTypeProvider = new FileExtensionContentTypeProvider();
            //contentTypeProvider.Mappings[".svg"] = "application/octet-stream";
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    ContentTypeProvider = contentTypeProvider
            //});

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //builder.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
