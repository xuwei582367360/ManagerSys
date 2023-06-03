using ManagerSys.Application.Contracts.ITest;
using ManagerSys.Application.Contracts.Log;
using ManagerSys.Domain.Shared.BaseAttribute;
using ManagerSys.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Volo.Abp.AspNetCore.Mvc;

namespace ManagerSys.HttpApi.Controllers
{
    /// <summary>
    /// 测试Swagger注释
    /// </summary>
    [Route("api/[controller]")]
    public class TaskController : AbpControllerBase
    {
        private readonly ITestService _testService;
        private readonly ISysLogService _SysLogService;
        public TaskController(ITestService testService, ISysLogService sysLogService)
        {
            _testService = testService;
            this._SysLogService = sysLogService;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="queryTestDto">接口</param>
        /// <returns></returns>
        [HttpGet, Route("GetTestAsync"), Explain("【测试Swagger注释】", OperateType.查询)]
        public async Task<QueryTestDto> GetTestAsync(QueryTestDto queryTestDto)
        {
            QueryTestDto dto = new QueryTestDto();
            dto.Id = 1; dto.Name = "";
            await _testService.GetTestAsync();

            SysLogAddDto model = new SysLogAddDto();
            model.IsDelete = false;
            model.Page = "";//context.ActionDescriptor.AttributeRouteInfo.Template;
            model.OperateType = "";
            model.LogType = 1;
            model.MacAddress = "";
            model.OperateUserIp = "";
            model.OperateContent = "";
            await this._SysLogService.Add(model);
            return dto;
        }

        /// <summary>
        ///新增
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("AddTestAsync"), Explain("【测试Swagger注释】", OperateType.添加)]
        public async Task<int> AddTestAsync()
        {
            string a = "weqr";
            int str = int.Parse(a);
            
            return str;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UpdateTest"), Explain("【测试Swagger注释】", OperateType.修改)]
        public async Task<bool> UpdateTest()
        {
            QueryTestDto dto = null;
            dto.Id = 1;
            return true;
        }
    }
}
