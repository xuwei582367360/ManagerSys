namespace ManagerSys.EntityFrameworkCore.Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
            Test1Async();
        }

        [Test]
        public async Task Test1Async()
        {
            Assert.Pass();
        }
    }
}