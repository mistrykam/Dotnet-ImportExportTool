using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using App.Core.Domain.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Application
{
    public class JsonPlaceholderImportExport : ImportExportBase
    {
        private readonly IJsonPlaceholderRepository _importRepository;
        private readonly IFileExportRepository _fileExportRepository;
        private readonly ILogger<JsonPlaceholderImportExport> _logging;
        private readonly AppSettings _appSettings;

        private IEnumerable<JsonPlaceholderUserDetails> _repoList = new List<JsonPlaceholderUserDetails>();

        public JsonPlaceholderImportExport(IJsonPlaceholderRepository importRepository, IFileExportRepository fileExportRepository,
                                           ILogger<JsonPlaceholderImportExport> logging, AppSettings appSettings)
        {
            _importRepository = importRepository;
            _fileExportRepository = fileExportRepository;
            _logging = logging;
            _appSettings = appSettings;
        }

        protected override void Start()
        {            
            _logging.LogInformation($"Start {nameof(JsonPlaceholderImportExport)}");
        }

        protected override async Task<bool> ReadDataAsync()
        {
            try
            {
                _logging.LogInformation("Sending request to read data...");

                JsonPlaceholderApiRequest apiRequest = new JsonPlaceholderApiRequest()
                {
                    Uri = _appSettings.JsonPlaceholderSettings.Uri,
                    Accept = _appSettings.JsonPlaceholderSettings.Accept,
                    UserAgent = _appSettings.JsonPlaceholderSettings.UserAgent
                };

                _repoList = await _importRepository.GetAsync(apiRequest);

                _logging.LogInformation("Completed request successfully.");
            }
            catch (Exception ex)
            {
                _logging.LogError(ex, "An error occurred when getting the reading the data.");

                return false;
            }

            return true;
        }

        protected override Task<bool> TransformDataAsync()
        {
            try
            {
                _logging.LogInformation("Transforming data...");

                /* Add transformation here */

                _logging.LogInformation("Completed transform successfully.");

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logging.LogError(ex, "An error occurred when transforming the data.");

                return Task.FromResult(true);  /* FIX: should just be true if method marked async */
            }
        }

        protected override async Task<bool> ExportDataAsync()
        {
            try
            {
                _logging.LogInformation("Exporting data...");

                // for debugging: write each record to the log
                if (_logging.IsEnabled(LogLevel.Debug))
                {
                    foreach (JsonPlaceholderUserDetails item in _repoList)
                        _logging.LogDebug(item.ToString());
                }

                await _fileExportRepository.WriteToCSVFileAsync(_repoList, _appSettings.JsonPlaceholderSettings.ExportFilePath);

                _logging.LogInformation("Completed request successfully.");
            }
            catch (Exception ex)
            {
                _logging.LogError(ex, "An error occurred when exporting the data.");

                return false;
            }

            return true;
        }

        protected override void Finish()
        {
            _logging.LogInformation($"Finished {nameof(JsonPlaceholderImportExport)}");
        }
    }
}