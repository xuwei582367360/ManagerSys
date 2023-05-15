using ManagerSys.Application.Contracts.Log;
using ManagerSys.Domian.Business;
using Volo.Abp.Domain.Repositories;

namespace ManagerSys.Application.Log
{
    public class SysLogService : ApplicationAppService, ISysLogService
    {
        #region Identity
        //private readonly ITokenUserService _UserService;
        private readonly IRepository<SysLog, Guid> _Repository;

        public SysLogService(IRepository<SysLog, Guid> repository /*,ITokenUserService userService*/)
        {
            this._Repository = repository;
            // this._UserService = userService;
        }  
        #endregion


        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Add(SysLogAddDto model)
        {
            var entity = ObjectMapper.Map<SysLogAddDto, SysLog>(model);
            //var userInfo = this._UserService.GetLoginUserInfo();
            //entity.AddUserName = userInfo?.EmpName;
            //entity.CreateUserDomainID = userInfo?.DomainID;
            entity.CreateUserCode = "";
            entity.CreateUser = "";
            entity.CreateTime = DateTime.Now;
            await _Repository.InsertAsync(entity);
        }
    }
}
