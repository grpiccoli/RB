using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MaReB.Data;
using MaReB.Models;
using MaReB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;
using AspNetCore.IServiceCollection.AddIUrlHelper;
using Microsoft.Extensions.Hosting;

namespace MaReB
{
    public class Startup
    {
        private readonly string _os;
        private const string defaultCulture = "en";
        private readonly CultureInfo[] supportedCultures;
        public Startup(IConfiguration configuration)
        {
            supportedCultures = new[]
                {
                    new CultureInfo(defaultCulture),
                    new CultureInfo("es")
                };
            Configuration = configuration;
            _os = Environment.OSVersion.Platform.ToString();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                // Formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                options.SupportedUICultures = supportedCultures;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString($"{_os}Connection")));

            services.AddHostedService<SeedBackground>();
            services.AddScoped<ISeed, SeedService>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<SpanishIdentityErrorDescriber>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = new PathString("/login");
                    o.AccessDeniedPath = new PathString("/Account/AccessDenied");
                    o.LogoutPath = new PathString("/logout");
                //})
                //.AddGoogle(o =>
                //{
                //    o.ClientId = "224197839051-tnuln037c689j19504lcav77c9o3atdd.apps.googleusercontent.com";
                //    o.ClientSecret = "2YjnwqldzmsUVpCL5tNJivz2";
                    //o.ClientId = Configuration.GetValue<string>("G_ID");
                    //o.ClientSecret = Configuration.GetValue<string>("G_SECRET");
                //})
                //.AddMicrosoftAccount(o =>
                //{
                //    //o.ClientId = Configuration["Authentication:Microsoft:ApplicationId"];
                //    //o.ClientSecret = Configuration["Authentication:Microsoft:Password"];
                //    o.ClientId = Configuration.GetValue<string>("MS_ID");
                //    o.ClientSecret = Configuration.GetValue<string>("MS_SECRET");
                //})
                //.AddOAuth("LinkedIn", o =>
                //{
                //    //o.ClientId = Configuration["Authentication:LinkedIn:ClientId"];
                //    //o.ClientSecret = Configuration["Authentication:LinkedIn:ClientSecret"];
                //    o.ClientId = Configuration.GetValue<string>("LI_ID");
                //    o.ClientSecret = Configuration.GetValue<string>("LI_SECRET");
                //    o.CallbackPath = new PathString("/signin-linkedin");
                //    o.AuthorizationEndpoint = "https://www.linkedin.com/oauth/v2/authorization";
                //    o.TokenEndpoint = "https://www.linkedin.com/oauth/v2/accessToken";
                //    o.UserInformationEndpoint = "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,email-address,picture-url,picture-urls::(original))";
                //    o.Scope.Add("r_basicprofile");
                //    o.Scope.Add("r_emailaddress");
                //    o.Events = new OAuthEvents
                //    {
                //        OnCreatingTicket = async context =>
                //        {
                //            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                //            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                //            request.Headers.Add("x-li-format", "json");

            //            var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
            //            response.EnsureSuccessStatusCode();
            //            var user = JObject.Parse(await response.Content.ReadAsStringAsync());

            //            var userId = user.Value<string>("id");
            //            if (!string.IsNullOrEmpty(userId))
            //            {
            //                context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId, ClaimValueTypes.String, context.Options.ClaimsIssuer));
            //            }

            //            var formattedName = user.Value<string>("formattedName");
            //            if (!string.IsNullOrEmpty(formattedName))
            //            {
            //                context.Identity.AddClaim(new Claim(ClaimTypes.Name, formattedName, ClaimValueTypes.String, context.Options.ClaimsIssuer));
            //            }

            //            var email = user.Value<string>("emailAddress");
            //            if (!string.IsNullOrEmpty(email))
            //            {
            //                context.Identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String,
            //                    context.Options.ClaimsIssuer));
            //            }
            //            var pictureUrl = user.Value<string>("pictureUrl");
            //            if (!string.IsNullOrEmpty(email))
            //            {
            //                context.Identity.AddClaim(new Claim("profile-picture", pictureUrl, ClaimValueTypes.String,
            //                    context.Options.ClaimsIssuer));
            //            }
            //        }
            //    };
        });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            //services.AddHsts(options =>
            //{
            //    options.Preload = true;
            //    options.IncludeSubDomains = true;
            //    options.MaxAge = TimeSpan.FromDays(60);
            //    options.ExcludedHosts.Add("mareb.cl");
            //    options.ExcludedHosts.Add("www.mareb.cl");
            //});

            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 443;
            //});

            //services.AddNodeServices(o =>
            //{
            //    o.ProjectPath = "./";
            //});

            services.AddUrlHelper();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Instituciones", policy => policy.RequireClaim("Instituciones", "Instituciones"));
                options.AddPolicy("Centros", policy => policy.RequireClaim("Centros", "Centros"));
                options.AddPolicy("Coordenadas", policy => policy.RequireClaim("Coordenadas", "Coordenadas"));
                options.AddPolicy("Producciones", policy => policy.RequireClaim("Producciones", "Producciones"));
                options.AddPolicy("Contactos", policy => policy.RequireClaim("Contactos", "Contactos"));
                options.AddPolicy("Usuarios", policy => policy.RequireClaim("Usuarios", "Usuarios"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app == null || env == null) return;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRouting();

            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
