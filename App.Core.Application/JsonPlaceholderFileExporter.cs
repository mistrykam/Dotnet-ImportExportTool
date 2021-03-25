using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using App.Core.Domain.Repository;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Application
{
    public class JsonPlaceholderFileExporter : FileExporterBase
    {
        private readonly IJsonPlaceholderRepository _repository;
        private readonly ILogger<JsonPlaceholderFileExporter> _logging;
        private readonly AppSettings _appSettings;

        private IEnumerable<JsonPlaceholderUserDetails> _repoList = new List<JsonPlaceholderUserDetails>();

        public JsonPlaceholderFileExporter(IJsonPlaceholderRepository repository, ILogger<JsonPlaceholderFileExporter> logging, AppSettings appSettings)
        {
            _repository = repository;
            _logging = logging;
            _appSettings = appSettings;
        }

        public override void Start()
        {
            _logging.LogInformation("Start JsonPlaceholderFileExporter");
        }

        public override async Task ReadDataAsync()
        {
            _logging.LogInformation("Read Data JsonPlaceholderFileExporter ");

            JsonPlaceholderApiRequest gitApiRequest = new JsonPlaceholderApiRequest()
            {
                Uri = _appSettings.GitUri,
                Accept = _appSettings.GitAccept,
                UserAgent = _appSettings.GitUserAgent
            };

            _repoList = await _repository.GetAsync(gitApiRequest);
        }

        public override Task ExportDataAsync()
        {
            _logging.LogInformation("Export Data JsonPlaceholderFileExporter");

            foreach (JsonPlaceholderUserDetails item in _repoList)
            {
                _logging.LogInformation(item.Name);
            }

            return Task.CompletedTask;
        }

        public override void End()
        {
            _logging.LogInformation("End JsonPlaceholderFileExporter");
        }
    }
}