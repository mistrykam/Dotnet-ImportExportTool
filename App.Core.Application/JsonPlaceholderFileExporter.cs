using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using App.Core.Domain.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Application
{
    public class JsonPlaceholderFileExporter : FileExporterBase
    {
        private readonly IJsonPlaceholderRepository _importRepository;
        private readonly IFileExportRepository _fileExportRepository;
        private readonly ILogger<JsonPlaceholderFileExporter> _logging;
        private readonly AppSettings _appSettings;

        private IEnumerable<JsonPlaceholderUserDetails> _repoList = new List<JsonPlaceholderUserDetails>();

        public JsonPlaceholderFileExporter(IJsonPlaceholderRepository importRepository, IFileExportRepository fileExportRepository,
                                           ILogger<JsonPlaceholderFileExporter> logging, AppSettings appSettings)
        {
            _importRepository = importRepository;
            _fileExportRepository = fileExportRepository;
            _logging = logging;
            _appSettings = appSettings;
        }

        public override void Start()
        {
            _logging.LogInformation("Start JsonPlaceholderFileExporter");
        }

        public override async Task<bool> ReadDataAsync()
        {
            try
            {
                _logging.LogInformation("Sending request to read data...");

                JsonPlaceholderApiRequest gitApiRequest = new JsonPlaceholderApiRequest()
                {
                    Uri = _appSettings.GitUri,
                    Accept = _appSettings.GitAccept,
                    UserAgent = _appSettings.GitUserAgent
                };

                _repoList = await _importRepository.GetAsync(gitApiRequest);

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

                await _fileExportRepository.WriteToCSVFileAsync(_repoList, _appSettings.JsonExportFilePath);

                foreach (JsonPlaceholderUserDetails item in _repoList)
                {
                    _logging.LogInformation(item.Name);
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
            _logging.LogInformation("End JsonPlaceholderFileExporter");
        }
    }
}