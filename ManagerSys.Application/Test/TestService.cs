using ManagerSys.Application.Contracts.ITest;
using ManagerSys.Domain.Shared.Localization;
using ManagerSys.Domian.Business;
using Microsoft.Extensions.Localization;
using Serilog;
using Volo.Abp.Domain.Repositories;

namespace ManagerSys.Application.Test
{
    public class TestService : ApplicationAppService, ITestService
    {
        private IStringLocalizer<MyLocalizationResource> _l;
        private readonly IRepository<SysLog, Guid> _Repository;
        public TestService(IStringLocalizer<MyLocalizationResource> l, IRepository<SysLog, Guid> Repository)
        {
            _l = l;
            _Repository = Repository;
        }

        public async Task<string> GetTestAsync()
        {
            Serilog.Log.Debug("测试Debug");
            Serilog.Log.Information("测试Information");
            Serilog.Log.Warning("测试Warning");
            Serilog.Log.Fatal("测试Fatal");
            return "test service";
        }

        public string GetWellCome()
        {
            return _l["LongWelcomeMessage"].Value;
        }
    }
}
