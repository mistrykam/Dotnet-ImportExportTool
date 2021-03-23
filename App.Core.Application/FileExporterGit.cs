using App.Core.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace App.Core.Application
{
    public class FileExporterGit : FileExporterBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<FileExporterGit> _logging;

        public FileExporterGit(IRepository repository, ILogger<FileExporterGit> logging)
        {
            _repository = repository;
            _logging = logging;
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void ReadData()
        {
            throw new NotImplementedException();
        }

        public override void ExportData()
        {
            throw new NotImplementedException();
        }


        public override void End()
        {
            throw new NotImplementedException();
        }


    }
}
