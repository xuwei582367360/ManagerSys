using ManagerSys.Application;
using ManagerSys.EntityFrameworkCore;
using ManagerSys.HttpApi;
using Microsoft.OpenApi.Models;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp;
using ManagerSys.Domain.Shared;
using ManagerSys.Domian;
using ManagerSys.Application.Contracts;
using Microsoft.AspNetCore.Cors;
using Volo.Abp.Auditing;
using Volo.Abp.AspNetCore.Mvc.Conventions;
using ManagerSys.Host.Route;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using ManagerSys.Host.Filter;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Serilog;
using ManagerSys.Application.ScheduleHosted;
using ManagerSys.Domain.Shared.QuartzNet;

namespace ManagerSys.Host
{
    [DependsOn(
        typeof(HttpApiModule),
        typeof(AbpAutofacModule),
        typeof(ApplicationModule),
        typeof(EntityFrameworkCoreModule),
        typeof(AbpSwashbuckleModule)
    )]
    public class HttpApiHostModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {

        }


        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            //自定义路由
            context.Services.AddTransient<IConventionalRouteBuilder, ConventionalRouteBuilderExt>();
            CongigurationFilter(context);
            ConfigureNewtonsoftJson(context);
            ConfigureUrls(configuration);
            ConfigureVirtualFileSystem(context);
            ConfigureConventionalControllers();
            ConfigureLocalization();
            ConfigureCors(context, configuration);
            //生产环境下禁用swagger
            if (configuration.GetSection("UseSwagger").Value == "true")
            {
                ConfigureSwaggerServices(context, configuration);
            }

            context.Services.AddSingleton<ResultfulApiJobFactory>();
            context.Services.AddHostedService<ScheduleHostedService>();
        }


        /// <summary>
        /// 全局拦截
        /// </summary>
        /// <param name="context"></param>
        private void CongigurationFilter(ServiceConfigurationContext context) 
        {
            context.Services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<ValidateFilter>(-1);
            });
        }

        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureNewtonsoftJson(ServiceConfigurationContext context)
        {
            //JSON序列化
            context.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new MyPropertyNamesContractResolver();    /*支持接收JOBject动态参数*/
                #region  例如
                //public IActionResult TestAlter([FromBody] JObject data)
                //{
                //    return Ok(data);
                //}
                #endregion
            });
        }
        public class MyPropertyNamesContractResolver : DefaultContractResolver
        {
            protected override string ResolvePropertyName(string propertyName)
            {
                //属性名全部返回小写
                return propertyName.ToLower();
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabled = true; //开启审计日志
            });

            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
                options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
                options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
            });
        }
        /// <summary>
        /// 配置 虚拟文件系统
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<DomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}ManagerSys.Domain.Shared"));

                    options.FileSets.ReplaceEmbeddedByPhysical<DomainModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}ManagerSys.Domain"));

                    options.FileSets.ReplaceEmbeddedByPhysical<ApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}ManagerSys.Application.Contracts"));

                    options.FileSets.ReplaceEmbeddedByPhysical<ApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}ManagerSys.Application"));
                });
            }
        }
        /// <summary>
        /// 配置 传统的控制器
        /// </summary>
        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(ApplicationModule).Assembly);
            });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
        {

            //context.Services.AddAbpSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ManagerSys API", Version = "v1" });
            //    options.DocInclusionPredicate((docName, description) => true);
            //    options.CustomSchemaIds(type => type.FullName);
            //}
            //);
            
            context.Services.AddSwaggerGen(options =>
            {
                var files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                foreach (var item in files)
                {
                    if (!string.IsNullOrEmpty(item))
                        options.IncludeXmlComments(Path.Combine(item), true);
                }
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ManagerSys API",
                    Description = $"API描述,v1版本"
                });
                //options.SwaggerDoc("v1", new OpenApiInfo { Title = "ManagerSys API", Version = "v1" });
                options.CustomSchemaIds(type => type.FullName);
                //options.OperationFilter<SwaggerHanderFilter>();
                options.AddServer(new OpenApiServer()
                {

                    Url = "",
                    Description = "Swagger"
                });
                options.CustomOperationIds(apidesc =>
                {
                    var controllAction = apidesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllAction?.ControllerName + "-" + controllAction?.ActionName;
                });
            });

        }
        /// <summary>
        /// 配置本地化
        /// </summary>
        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
                options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
                options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
                options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
                options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
                options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
                options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
                options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
                options.Languages.Add(new LanguageInfo("es", "es", "Español", "es"));
                options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));
            });
        }

        /// <summary>
        /// 配置Cors
        /// <para>
        /// 配置跨域请求
        /// </para>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });


        }

        public override void OnApplicationInitialization(Volo.Abp.ApplicationInitializationContext context)
        {

            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();               
            }
            app.UseAbpRequestLocalization();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseUnitOfWork();
            app.UseAuthorization();

            if (context.GetConfiguration().GetSection("UseSwagger").Value == "true")
            {
                app.UseSwagger();
                //app.UseAbpSwaggerUI(options =>
                //{
                //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ManagerSys API");
                //});
                //Swagger使用自定义UI
                app.UseKnife4UI(c =>
                {
                    c.RoutePrefix = "";
                    c.InjectJavascript("/js/init.js");
                    c.InjectStylesheet("/css/index.css");
                    c.SwaggerEndpoint("/v1/api-docs", "v1docs");
                });
            }
           
            app.UseConfiguredEndpoints(endpoints =>
            {
                endpoints.MapGet("/api/check", () => $"心跳检测成功{DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss")},当前发布时间：2022-08-09 16:17");
                endpoints.MapControllers();//
                endpoints.MapSwagger("{documentName}/api-docs");
            });
            app.UseAuditing();
            app.UseConfiguredEndpoints();
        }

    }
}
