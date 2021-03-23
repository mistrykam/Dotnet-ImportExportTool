using App.Core.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace App.Core.Application
{
    public class FileExporterGit : FileExporterBase
    {
        private readonly IGitRepository _repository;
        private readonly ILogger<FileExporterGit> _logging;

        public FileExporterGit(IGitRepository repository, ILogger<FileExporterGit> logging)
        {
            _repository = repository;
            _logging = logging;
        }

        public override void Start()
        {
            _logging.LogInformation("Git Start");
        }

        public override void ReadData()
        {
            _logging.LogInformation("Git Read Data");
        }

        public override void ExportData()
        {
            _logging.LogInformation("Git Export Data");
        }

        public override void End()
        {
            _logging.LogInformation("Git End");
        }


    }
}
