using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Application
{
    public class GitFileExporter : FileExporterBase
    {
        private readonly IGitRepository _repository;
        private readonly ILogger<GitFileExporter> _logging;

        private IEnumerable<GitRepoDetails> _repoDetailsList = new List<GitRepoDetails>();

        public GitFileExporter(IGitRepository repository, ILogger<GitFileExporter> logging)
        {
            _repository = repository;
            _logging = logging;
        }

        public override void Start()
        {
            _logging.LogInformation("Git Start");
        }

        public override async Task ReadDataAsync()
        {
            _logging.LogInformation("Git Read Data");

            GitApiRequest gitApiRequest = new GitApiRequest()
            {
                Uri = "https://api.github.com/orgs/dotnet/repos",
                Accept = "application/vnd.github.v3+json",
                UserAgent = ".NET Foundation Repository Reporter"
            };

            _repoDetailsList = await _repository.Get(gitApiRequest);
        }

        public override void ExportData()
        {
            _logging.LogInformation("Git Export Data");

            foreach (GitRepoDetails item in _repoDetailsList)
            {
                _logging.LogInformation(item.GitHubHomeUrl.ToString());
            }
        }

        public override void End()
        {
            _logging.LogInformation("Git End");
        }
    }
}