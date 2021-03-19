using App.Domain.Interfaces;
using System;

namespace App.Core.Application
{
    public class FileExporterGit : FileExporterBase
    {
        private readonly IWebApiRequest _webApiRequest;

        public FileExporterGit(IWebApiRequest webApiRequest)
        {
            _webApiRequest = webApiRequest;
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
