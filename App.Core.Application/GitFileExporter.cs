using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using App.Core.Domain.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Application
{
    public class GitFileExporter : FileExporterBase
    {
        private readonly IGitRepository _repository;
        private readonly ILogger<GitFileExporter> _logging;
        private readonly AppSettings _appSettings;

        private IEnumerable<GitRepoDetails> _repoList = new List<GitRepoDetails>();

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

        public override async Task<bool> ReadDataAsync()
        {
            try
            {
                _logging.LogInformation("Sending request to read data...");

                GitApiRequest gitApiRequest = new GitApiRequest()
                {
                    Uri = _appSettings.GitUri,
                    Accept = _appSettings.GitAccept,
                    UserAgent = _appSettings.GitUserAgent
                };

                _repoList = await _repository.GetAsync(gitApiRequest);

                _logging.LogInformation("Completed request successfully");
            }
            catch (Exception ex)
            {
                _logging.LogError(ex, "An error occurred when getting the reading the data.");

                return false;
            }

            return true;
        }

        public override async Task<bool> ExportDataAsync()
        {
            try
            {
                _logging.LogInformation("Exporting data...");

                await WriteToCSVFile(_repoList, _appSettings.GitExportFilePath);

                foreach (GitRepoDetails item in _repoList)
                {
                    _logging.LogInformation(item.GitHubHomeUrl.ToString());
                }

                _logging.LogInformation("Completed request successfully.");
            }
            catch (Exception ex)
            {
                _logging.LogError(ex, "An error occurred when exporting the data.");

                return false;
            }

            return true;
        }

        public override void End()
        {
            _logging.LogInformation("End GitFileExporter");
        }
    }
}