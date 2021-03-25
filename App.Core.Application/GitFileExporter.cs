using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using App.Core.Domain.Repository;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Application
{
    public class GitFileExporter : FileExporterBase
    {
        private readonly IGitRepository _repository;
        private readonly ILogger<GitFileExporter> _logging;
        private readonly AppSettings _appSettings;

        private IEnumerable<GitRepoDetails> _repoDetailsList = new List<GitRepoDetails>();

        public GitFileExporter(IGitRepository repository, ILogger<GitFileExporter> logging, AppSettings appSettings)
        {
            _repository = repository;
            _logging = logging;
            _appSettings = appSettings;
        }

        public override void Start()
        {
            _logging.LogInformation("Start GitFileExporter");
        }

        public override async Task ReadDataAsync()
        {
            _logging.LogInformation("Read Data GitFileExporter");

            GitApiRequest gitApiRequest = new GitApiRequest()
            {
                Uri = _appSettings.GitUri,
                Accept = _appSettings.GitAccept,
                UserAgent = _appSettings.GitUserAgent
            };

            _repoDetailsList = await _repository.GetAsync(gitApiRequest);
        }

        public override async Task ExportDataAsync()
        {
            _logging.LogInformation("Export Data GitFileExporter");

            await WriteToCSVFile(_repoDetailsList, _appSettings.GitExportFilePath);

            foreach (GitRepoDetails item in _repoDetailsList)
            {
                _logging.LogInformation(item.GitHubHomeUrl.ToString());
            }
        }

        public override void End()
        {
            _logging.LogInformation("End GitFileExporter");
        }
    }
}