using LogDashboard.Repository;
using ManagerSys.Domain.Shared.Common;
using ManagerSys.Domian.Business;
using Volo.Abp.Domain.Repositories;

namespace ManagerSys.Domian
{
    public class DefaultDataSeederContributor : IDefaultDataSeederContributor
    {
        public readonly IRepository<SystemUserEntity, int> _repository;

        public DefaultDataSeederContributor(IRepository<SystemUserEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task SeedAsync()
        {
            await CreateResourceDataAsync();
        }

        /// <summary>
        /// Resource表静态资源初始化
        /// </summary>
        /// <returns></returns>
        public async Task CreateResourceDataAsync()
        {
            //1、清空所有数据
            //await _resources.DeleteAsync(p => p.Id == "CruiseType"); //删除无效 不知为何
            //2、提取种子数据
            var tmpDataLst = GetResourceData();
            //3、循环插入数据
            foreach (var item in tmpDataLst)
            {
                var tmpEntity = await _repository.FirstOrDefaultAsync(p => p.Id == item.Id);
                if (tmpEntity != null)
                {
                    //数据当前数据存在删除当前记录DeleteAsync(tmpEntity);
                    //如果 继承了软删除 ISoftDelete 则需要调用 强制删除：HardDeleteAsync(tmpEntity) ;
                    await _repository.DeleteAsync(tmpEntity);
                }
                await _repository.InsertAsync(item);
            }
        }

        /// <summary>
        /// Resource静态资源配置
        /// </summary>
        /// <returns></returns>
        private List<SystemUserEntity> GetResourceData()
        {
            List<SystemUserEntity> tmpDataLst = new List<SystemUserEntity>();
            //1、任务资源类型：
            tmpDataLst.AddRange(new List<SystemUserEntity>{
              new SystemUserEntity()
                {
                    Id = 1,
                    CreateTime = DateTime.Now,
                    Status = (int)SystemUserStatus.Available,
                    UserName = "admin",
                    RealName = "admin",
                    Password = SecurityHelper.MD5("111111"),
                    IsDeleted = false
                }
          });
            return tmpDataLst;
        }
    }

}
