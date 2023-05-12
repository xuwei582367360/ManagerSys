using ManagerSys.Application.Contracts.ITest;
using ManagerSys.Domain.Shared.Localization;
using ManagerSys.Domian.Business;
using Microsoft.Extensions.Localization;
using Serilog;
using Volo.Abp.Domain.Repositories;

namespace ManagerSys.Application.Test
{
    public class TestService : ApplicationAppService,ITestService
    {
        private IStringLocalizer<MyLocalizationResource> _l;
        public TestService(IStringLocalizer<MyLocalizationResource> l)
        {
            _l = l;
        }

        public async Task<string> GetTestAsync()
        {
            Log.Debug("测试Debug");
            Log.Information("测试Information");
            Log.Warning("测试Warning");
            Log.Fatal("测试Fatal");
            return  "test service";
        }

        public string GetWellCome()
        {
            return _l["LongWelcomeMessage"].Value;
        }
    }
}
