using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePropertyPro.Api.ActionFilters;
using AspNetCorePropertyPro.Api.Middlewares;
using AspNetCorePropertyPro.Core.Models;
using AspNetCorePropertyPro.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;
using System.Reflection;
using System.IO;
using AspNetCorePropertyPro.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AspNetCorePropertyPro.Core.Services;
using AspNetCorePropertyPro.Services;
using AspNetCorePropertyPro.Core.Repositories;
using AspNetCorePropertyPro.Data.Repositories;
using Microsoft.Extensions.Options;
using AspNetCorePropertyPro.Core;
using FluentValidation.AspNetCore;
using AspNetCorePropertyPro.Api.Resources.Response;
using AspNetCorePropertyPro.Configuration.Utils;
using AspNetCorePropertyPro.Api.Extensions;
using Hangfire;

namespace AspNetCorePropertyPro.Api
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
            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));
            services.Configure<SendGridSetting>(Configuration.GetSection("SendGridSetting"));
            services.Configure<CloudinarySetting>(Configuration.GetSection("CloudinarySetting"));
            services.AddCors();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddDbContext<TenantDbContext>();
            services.AddDbContext<GlobalDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddHangfire(config => {
                config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();
            });
            //services.AddHangfireServer();

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = (context) =>
                {
                    var errorsInModelState = context.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(x => x.ErrorMessage))
                    .ToArray();

                    var errorResponse = new ErrorResponse();

                    foreach (var error in errorsInModelState)
                        foreach (var subError in error.Value)
                            errorResponse.Errors.Add(new ErrorModel
                            {
                                FieldName = error.Key,
                                Message = subError
                            });

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(
                opt =>
                {
                    opt.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<TenantDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<IDealTypeService, DealTypeService>();
            services.AddTransient<IPropertyImageService, PropertyImageService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IResponse, Response>();
            services.AddTransient<IFavouriteService, FavouriteService>();
            services.AddTransient<IFlagService, FlagService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITenantService, TenantService>();
            services.AddScoped<IHangfireRecurringJobService, HangfireRecurringJobService>();



            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                            Configuration.GetSection("JwtSetting:Secret").Value))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Property Pro API", Version = "v1" });
                c.OperationFilter<TenantHeaderOperationFilter>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Jwt Athpurization header bearer token",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        } },
                        new List<string>()
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITenantService tenantService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseTenantIdentifier();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Property Pro API");
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.InitializeTenants(tenantService);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
