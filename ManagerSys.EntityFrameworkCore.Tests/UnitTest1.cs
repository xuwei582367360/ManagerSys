using ManagerSys.Domian.CDZLS;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Domain.Repositories;

namespace ManagerSys.EntityFrameworkCore.Tests
{
    public class Tests
    {
        private readonly IRepository<AreaEntity, Guid> _appUserRepository;


        public Tests(IRepository<AreaEntity, Guid> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        [SetUp]
        public void Setup()
        {
            Test1Async();
        }

        [Test]
        public async Task Test1Async()
        {
            var adminUser =  (await _appUserRepository.GetQueryableAsync())
                     .Where(u => u.Code == "01")
                     .ToList();
             Assert.Pass();
        }
    }
}