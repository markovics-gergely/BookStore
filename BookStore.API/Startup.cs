using BookStore.BLL.DTO;
using BookStore.BLL.Interfaces;
using BookStore.BLL.Services;
using BookStore.DAL;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BLL.Exceptions;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using BookStore.API.ProblemDetails;
using NSwag;
using NSwag.Generation.Processors.Security;
using NSwag.AspNetCore;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using System.Diagnostics;
using Microsoft.IdentityModel.Logging;

namespace BookStore.API
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
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = AzureADB2CDefaults.JwtBearerAuthenticationScheme;
                
            })
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"), AzureADB2CDefaults.JwtBearerAuthenticationScheme);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.Authority = "https://bookstoreb2c.b2clogin.com/bookstoreb2c.onmicrosoft.com/b2c_1_signupsignin/v2.0/";
                    jwtOptions.Audience = Configuration["AzureAdB2C:ClientId"];
                    jwtOptions.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = AuthenticationFailed
                    };
                });

            services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.
                    Converters.Add(new JsonStringEnumConverter());
                });
            IdentityModelEventSource.ShowPII = true;
            services.AddDbContext<NorthwindContext>(o =>
                    o.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IBorrowingService, BorrowingService>();
            services.AddAutoMapper(typeof(BookStoreProfile));
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => false;
                options.Map<EntityNotFoundException>(
                    (ctx, ex) =>
                    {
                        var pd = StatusCodeProblemDetails.Create(StatusCodes.Status404NotFound);
                        pd.Title = ex.Message;
                        return pd;
                    }
                );
                options.Map<DbUpdateConcurrencyException>(
                    ex => new ConcurrencyProblemDetails(ex));
            }); 
            services.AddOpenApiDocument(document =>
            {
                document.Title = "Book Store";
                document.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Description = "B2C authentication",
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            Scopes = new Dictionary<string, string>
                        {
                            { "https://bookstoreb2c.onmicrosoft.com/api/allusers", "Allows access to services every user can use" },
                        },
                            AuthorizationUrl = "https://bookstoreb2c.b2clogin.com/bookstoreb2c.onmicrosoft.com/oauth2/v2.0/authorize?p=b2c_1_signupsignin",
                            TokenUrl = "https://bookstoreb2c.b2clogin.com/bookstoreb2c.onmicrosoft.com/oauth2/v2.0/token?p=b2c_1_signupsignin"
                        },
                    }
                });
                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AllUsers", policy =>
                    policy.RequireClaim(
                        "http://schemas.microsoft.com/identity/claims/scope",
                        "allusers"
                    ));
                options.AddPolicy("Admin", policy =>
                    policy.RequireClaim(
                        "http://schemas.microsoft.com/identity/claims/objectidentifier",
                        "cd7115d0-0d79-42d6-ad30-fd0ae319a5cb", "8604d53b-5134-43c3-b23a-0c513c77a46d"
                    ));
                
            });
        }

        private Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            Debug.WriteLine(arg);
            return Task.CompletedTask;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = "93e9955c-93da-4da1-8b37-1eeec5e62644",
                    AppName = "swagger-ui-client"
                };
            });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
