using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Test.App.Core.Application
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async Task Test_ReadGitRepositoryAsync()
        {
            System.Diagnostics.Debug.WriteLine("Starting...");

            var apiRequest = new ApiRequest()
            {
                Uri = "https://api.github.com/orgs/dotnet/repos",
                Accept = "application/vnd.github.v3+json",
                UserAgent = ".NET Foundation Repository Reporter"
            };

            IGitRepository gitRepository = new GitRepository();

            var list = await gitRepository.Get(apiRequest);

            Assert.IsNotNull(list);

            if (list != null)
            {
                foreach (var item in list)
                    System.Diagnostics.Debug.WriteLine($" {item.Name}\n {item.Description}\n {item.Homepage}\n {item.GitHubHomeUrl}\n {item.LastPush}\n");
            }
        }
    }
}