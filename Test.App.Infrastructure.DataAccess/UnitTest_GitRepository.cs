using App.Core.Domain.Entities;
using App.Core.Domain.Repository;
using App.Infrastructure.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.App.Core.Application
{
    [TestClass]
    public class UnitTest_GitRepository
    {
        [TestMethod]
        public async Task Test_GitRepositoryAsync_GetAsync()
        {
            System.Diagnostics.Debug.WriteLine("Starting...");

            GitApiRequest gitApiRequest = new GitApiRequest()
            {
                Uri = "https://api.github.com/orgs/dotnet/repos",
                Accept = "application/vnd.github.v3+json",
                UserAgent = ".NET Foundation Repository Reporter"
            };

            IGitRepository repository = new GitRepository();

            IEnumerable<GitRepoDetails> list = await repository.GetAsync(gitApiRequest);

            Assert.IsNotNull(list);

            if (list != null)
            {
                foreach (GitRepoDetails item in list)
                    System.Diagnostics.Debug.WriteLine($" {item.Name}\n {item.Description}\n {item.Homepage}\n {item.GitHubHomeUrl}\n {item.LastPush}\n");
            }
        }
    }
}