using ManagerSys.Domain.Shared.Localization;
using ManagerSys.Domian.Business;
using Microsoft.Extensions.Localization;
using Serilog;
using Volo.Abp.Domain.Repositories;

namespace ManagerSys.Application.Test
{
    public class TestService : ApplicationAppService
    {
        private IStringLocalizer<MyLocalizationResource> _l;
        private readonly IRepository<SystemUserEntity> _appUserRepository;
        public TestService(IStringLocalizer<MyLocalizationResource> l, IRepository<SystemUserEntity> appUserRepository)
        {
            _l = l;
            _appUserRepository = appUserRepository;
        }

        public async Task<string> GetTestAsync()
        {
            var a = _appUserRepository.GetListAsync().Result.ToList();
            var adminUser = (await _appUserRepository.GetQueryableAsync())
                   .OrderBy(e => e.Id).ToList().Skip(0).Take(5).ToList();
            var order = _order.GetListAsync().Result.ToList();

            Log.Debug("测试Debug");
            Log.Information("测试Information");
            Log.Warning("测试Warning");
            Log.Fatal("测试Fatal");
            return "test service";
        }

        public string GetWellCome()
        {
            return _l["LongWelcomeMessage"].Value;
        }
    }
}
