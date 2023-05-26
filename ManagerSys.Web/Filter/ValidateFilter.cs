using ManagerSys.Application.Contracts.Log;
using ManagerSys.Domain.Shared.BaseAttribute;
using ManagerSys.Domain.Shared.Consts;
using ManagerSys.Domain.Shared.Enums;
using ManagerSys.Domain.Shared.Enums.Util;
using ManagerSys.Domain.Shared.PageModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Volo.Abp.Uow;

namespace ManagerSys.Web.Filter
{
    public class ValidateFilter : IActionFilter
    {
        #region
        private readonly ISysLogService _LogService;
        public ValidateFilter(ISysLogService logService)
        {
            this._LogService = logService;
        }

        #endregion

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task InsertLog(ActionExecutingContext context)
        {
            var actions = context.ActionDescriptor as ControllerActionDescriptor;
            var explain = actions.MethodInfo.GetCustomAttributes(typeof(ExplainAttribute), true);
            if (explain.Length > 0)
            {
                var explaion = (ExplainAttribute)(explain.FirstOrDefault());
                if (explaion.OptionName != EnumUtils.GetDescription((Enum)OperateType.查询))
                {
                    SysLogAddDto model = new SysLogAddDto();
                    model.IsDelete = false;
                    model.Page = context.HttpContext.Request.Path.Value;
                    model.OperateType = explaion.OptionName;
                    model.LogType = 1;
                    model.MacAddress = context.HttpContext.GetMacBySendARP();
                    model.OperateUserIp = context.HttpContext.GetClientIp();
                    var str = JsonConvert.SerializeObject(context.ActionArguments);
                    model.OperateContent = explaion.Explain + "，参数信息:" + str;
                    await this._LogService.Add(model);
                }
            }
        }



        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = new BasePage();
                foreach (var item in context.ModelState.Values)
                {
                    if (item.ValidationState == ModelValidationState.Invalid)
                    {
                        result.Code = HttpResultCode.Fail;
                        result.Message = item.Errors.FirstOrDefault().ErrorMessage;
                        //result.Message = "数据类型错误";
                        break;
                    }
                }
                context.Result = new JsonResult(result);
            }
            var actions = context.ActionDescriptor as ControllerActionDescriptor;
            var explain = actions.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true);
            if (explain.Length > 0)
            {
                return;
            }
            InsertLog(context).Wait();
            return;
            //var domainId = context.HttpContext.Session.GetString("DomainId");
            ////如果用户已经登陆
            //if (!string.IsNullOrEmpty(domainId))
            //{
            //    InsertLog(context).Wait();
            //    return;
            //}
            //else//如果没有登陆请球登陆接口
            //{
            //    context.Result = new RedirectResult("/Home/Auth");
            //}
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

       }
    }

}
