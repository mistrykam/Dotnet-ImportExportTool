using App.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Test.App.Core.Application
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1Async()
        {
            System.Diagnostics.Debug.WriteLine("Starting...");

            var apiRequest = new ApiRequest()
            {
                Uri = "https://api.github.com/orgs/dotnet/repos",
                Accept = "application/vnd.github.v3+json",
                UserAgent = ".NET Foundation Repository Reporter"
            };

            IRepository request = new GITRepository();

            var list = await request.Get<RepoDetails>(apiRequest);

            Assert.IsNotNull(list);

            if (list != null)
            {
                foreach (var item in list)
                    System.Diagnostics.Debug.WriteLine($" {item.Name}\n {item.Description}\n {item.Homepage}\n {item.GitHubHomeUrl}\n {item.LastPush}\n");
            }
        }
    }
}