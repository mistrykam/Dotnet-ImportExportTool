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

        public override void Start()
        {            
            _logging.LogInformation($"Start {nameof(JsonPlaceholderImportExport)}");
        }

        public override async Task<bool> ReadDataAsync()
        {
            try
            {
                _logging.LogInformation("Sending request to read data...");

                JsonPlaceholderApiRequest apiRequest = new JsonPlaceholderApiRequest()
                {
                    Uri = _appSettings.JsonPlaceholderUri,
                    Accept = _appSettings.JsonPlaceholderAccept,
                    UserAgent = _appSettings.JsonPlaceholderUserAgent
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

        public override async Task<bool> ExportDataAsync()
        {
            try
            {
                _logging.LogInformation("Exporting data...");

                // debugging
                foreach (JsonPlaceholderUserDetails item in _repoList)
                    _logging.LogDebug(item.ToString());

                await _fileExportRepository.WriteToCSVFileAsync(_repoList, _appSettings.JsonExportFilePath);

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
            _logging.LogInformation($"End {nameof(JsonPlaceholderImportExport)}");
        }
    }
}