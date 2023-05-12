using ManagerSys.Application.Contracts.ITest;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.HttpApi
{
    public class TaskController: ControllerBase
    {
        private readonly ITestService _testService;
        public TaskController(ITestService testService)
        {
            _testService = testService;
        }

        /// <summary>
        /// 获取token 信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet, Route("GetTestAsync")]
        public async Task<string> GetTestAsync()
        {
            return await _testService.GetTestAsync();
        }
    }
}
