using ManagerSys.Application.Contracts.ITest;
using ManagerSys.Application.Contracts.Log;
using ManagerSys.Domain.Shared.BaseAttribute;
using ManagerSys.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

using Volo.Abp.AspNetCore.Mvc;

namespace ManagerSys.HttpApi.Controllers
{
    /// <summary>
    /// 测试Swagger注释
    /// </summary>
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
        /// 测试接口
        /// </summary>
        /// <param name="queryTestDto">接口</param>
        /// <returns></returns>
        [HttpGet, Route("GetTestAsync"), Explain("【测试Swagger注释】", OperateType.查询)]
        public async Task<QueryTestDto> GetTestAsync(QueryTestDto queryTestDto)
        {
            QueryTestDto dto = null;
            dto.Id = 1; dto.Name = "";
            await _testService.GetTestAsync();
            return dto;
        }
    }
}
