using ManagerSys.Domain.Shared.Localization;
using Microsoft.Extensions.Localization;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Application.Test
{
    public class TestService : ApplicationAppService
    {
        private IStringLocalizer<MyLocalizationResource> _l;
        public TestService(IStringLocalizer<MyLocalizationResource> l)
        {
            _l = l;
        }

        public string GetTest()
        {
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
