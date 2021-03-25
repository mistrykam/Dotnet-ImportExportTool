using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using App.Core.Domain.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Application
{
    public class GitImportExport : ImportExportBase
    {
        private readonly IGitRepository _importRepository;
        private readonly IFileExportRepository _fileExportRepository;
        private readonly ILogger<GitImportExport> _logging;
        private readonly AppSettings _appSettings;

        private IEnumerable<GitRepoDetails> _repoList = new List<GitRepoDetails>();

        public GitImportExport(IGitRepository importRepository, IFileExportRepository fileExportRepository, 
                               ILogger<GitImportExport> logging, AppSettings appSettings)
        {
            _importRepository = importRepository;
            _fileExportRepository = fileExportRepository;
            _logging = logging;
            _appSettings = appSettings;
        }

        public override void Start()
        {
            _logging.LogInformation($"Start {nameof(GitImportExport)}");
        }

        public override async Task<bool> ReadDataAsync()
        {
            try
            {
                _logging.LogInformation("Sending request to read data...");

                GitApiRequest apiRequest = new GitApiRequest()
                {
                    Uri = _appSettings.GitSettings.Uri,
                    Accept = _appSettings.GitSettings.Accept,
                    UserAgent = _appSettings.GitSettings.UserAgent
                };

                _repoList = await _importRepository.GetAsync(apiRequest);

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

                // debugging
                foreach (GitRepoDetails item in _repoList)
                    _logging.LogDebug(item.ToString());

                await _fileExportRepository.WriteToCSVFileAsync(_repoList, _appSettings.GitSettings.ExportFilePath);

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
            _logging.LogInformation($"End {nameof(GitImportExport)}");
        }
    }
}