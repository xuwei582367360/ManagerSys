using ManagerSys.Application.Contracts.Schedule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.HttpApi.Controllers
{
    /// <summary>
    /// 调度任务
    /// </summary>
    public class ScheduleController : ControllerBase
    {

        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService) {
            _scheduleService = _scheduleService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("");
        }
    }
}
