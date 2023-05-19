using ManagerSys.Application.Contracts.Log;
using ManagerSys.Domain.Shared.BaseAttribute;
using ManagerSys.Domain.Shared.Consts;
using ManagerSys.Domain.Shared.PageModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Text;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Json;

namespace ManagerSys.Host.Filter
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {

        private readonly ISysLogService _SysLogService;
        //private readonly LogCommonHelper _LogHelper;
        //private readonly ITokenUserService _TokenUserService;
        public ExceptionFilter(
            ISysLogService sysLogService
            //,LogCommonHelper logHelper,
            //ITokenUserService tokenUserService
            )
        {
            this._SysLogService = sysLogService;
            //this._LogHelper = logHelper;
            //this._TokenUserService = tokenUserService;
        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (!ShouldHandleException(context))
            {
                return;
            }
            await HandleAndWrapException(context);
        }

        protected virtual bool ShouldHandleException(ExceptionContext context)
        {
            if (context.ActionDescriptor.IsControllerAction() &&
                context.ActionDescriptor.HasObjectResult())
            {
                return true;
            }

            if (context.HttpContext.Request.CanAccept(MimeTypes.Application.Json))
            {
                return true;
            }

            if (context.HttpContext.Request.IsAjax())
            {
                return true;
            }

            return false;
        }

        protected virtual async Task HandleAndWrapException(ExceptionContext context)
        {
            //TODO: Trigger an AbpExceptionHandled event or something like that.

            var exceptionHandlingOptions = context.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;
            var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
            var remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(context.Exception, options =>
            {
                options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
                options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
            });

            var logLevel = context.Exception.GetLogLevel();

            var remoteServiceErrorInfoBuilder = new StringBuilder();
            remoteServiceErrorInfoBuilder.AppendLine(context.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

            var logger = context.GetService<ILogger<AbpExceptionFilter>>(NullLogger<AbpExceptionFilter>.Instance);

            logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());

            logger.LogException(context.Exception, logLevel);
            await InsertLog(context);
            await context.GetRequiredService<IExceptionNotifier>().NotifyAsync(new ExceptionNotificationContext(context.Exception));
            context.HttpContext.Response.Headers.Add(AbpHttpConsts.AbpErrorFormat, "true");
            context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            var result = new BasePage()
            {
                Code = HttpResultCode.Error,
                Message = context.Exception.Message
            };
            context.Result = new JsonResult(result);
            context.Exception = null; //Handled!
        }

        private async Task InsertLog(ExceptionContext context)
        {
            var actions = context.ActionDescriptor as ControllerActionDescriptor;
            var explain = actions.MethodInfo.GetCustomAttributes(typeof(ExplainAttribute), true);

            if (explain.Length > 0)
            {
                var error = $"，错误信息:{context.Exception.Message}堆栈信息:{context.Exception.StackTrace}";
                var explaion = (ExplainAttribute)explain.FirstOrDefault();
                SysLogAddDto model = new SysLogAddDto();
                model.IsDelete = false;
                model.Page = context.HttpContext.Request.Path.Value;
                model.OperateType = explaion.OptionName;
                model.LogType = 2;
                model.MacAddress = context.HttpContext.GetMacBySendARP();
                model.OperateUserIp = context.HttpContext.GetClientIp();
                model.OperateContent = explaion.Explain + error;
                await this._SysLogService.Add(model);
            }
        }
    }
}
