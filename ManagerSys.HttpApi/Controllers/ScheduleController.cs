using ManagerSys.Application.Contracts.Schedule;
using ManagerSys.Domain.Shared.BaseAttribute;
using ManagerSys.Domain.Shared.Enums;
using ManagerSys.HttpApi.Schedule;
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
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleManager _scheduleManager;
        public ScheduleController(ScheduleManager scheduleManager)
        {
            _scheduleManager = scheduleManager;
        }

        [HttpPost, Route("start"), Explain("【任务调度】", OperateType.启动服务)]
        public async Task<IActionResult> Start([FromForm] Guid sid)
        {
            bool success = await _scheduleManager.StartWithRetry(sid);
            if (success) return Ok();
            return BadRequest();
        }


        [HttpPost, Route("stop"), Explain("【任务调度】", OperateType.停止服务)]
        public async Task<IActionResult> Stop([FromForm] Guid sid)
        {
            bool success = await _scheduleManager.Stop(sid);
            if (success) return Ok();
            return BadRequest();
        }

        [HttpPost, Route("pause"), Explain("【任务调度】", OperateType.暂停服务)]
        public async Task<IActionResult> Pause([FromForm] Guid sid)
        {
            bool success = await _scheduleManager.Pause(sid);
            if (success) return Ok();
            return BadRequest();
        }

        [HttpPost, Route("resume"), Explain("【任务调度】", OperateType.恢复运行)]
        public async Task<IActionResult> Resume([FromForm] Guid sid)
        {
            bool success = await _scheduleManager.Resume(sid);
            if (success) return Ok();
            return BadRequest();
        }

        [HttpPost, Route("runOnce"), Explain("【任务调度】", OperateType.立即运行一次)]
        public async Task<IActionResult> RunOnce([FromForm] Guid sid)
        {
            bool success = await _scheduleManager.RunOnce(sid);
            if (success) return Ok();
            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("");
        }
    }
}
