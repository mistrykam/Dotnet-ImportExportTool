using App.Core.Domain.Entities;
using App.Core.Domain.Repository;
using App.Infrastructure.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.App.Core.Application
{
    [TestClass]
    public class UnitTest_JsonPlaceholderRepository
    {
        [TestMethod]
        public async Task Test_JsonPlaceholderRepository_GetAsync()
        {
            System.Diagnostics.Debug.WriteLine("Starting...");

            //GitApiRequest gitApiRequest = new GitApiRequest()
            //{
            //    Uri = "https://api.github.com/orgs/dotnet/repos",
            //    Accept = "application/vnd.github.v3+json",
            //    UserAgent = ".NET Foundation Repository Reporter"
            //};

            IJsonPlaceholderRepository repository = new JsonPlaceholderRepository();

            IEnumerable<JsonPlaceholderUserDetails> list = await repository.GetAsync();

            Assert.IsNotNull(list);

            if (list != null)
            {
                foreach (JsonPlaceholderUserDetails item in list)
                { 
                    System.Diagnostics.Debug.WriteLine($"{item.Id} | {item.UserName} | {item.Name} | {item.Email}");
                    System.Diagnostics.Debug.WriteLine($"{item.Address.Street}, {item.Address.ZipCode}");
                    System.Diagnostics.Debug.WriteLine($"{item.Address.Geo.Lattitude}, {item.Address.Geo.Longitude}");
                    System.Diagnostics.Debug.WriteLine($"{item.Company.Name} | {item.Company.CatchPhrase} \n");
                }
            }
        }
    }
}