using System;
using System.Threading.Tasks;

namespace App.Core.Domain.Interfaces
{
    public abstract class ImportExportBase
    {
        /// <summary>
        /// Start the tasks, peform any pre-checks steps here.
        /// </summary>
        protected abstract void Start();

        /// <summary>
        /// Read the data from the source.
        /// </summary>
        /// <returns></returns>
        protected abstract Task<bool> ReadDataAsync();

        /// <summary>
        /// Transform the data if required.
        /// </summary>
        /// <returns></returns>
        protected abstract Task<bool> TransformDataAsync();

        /// <summary>
        /// Export the data.
        /// </summary>
        /// <returns></returns>
        protected abstract Task<bool> ExportDataAsync();

        /// <summary>
        /// Finished the task, perform any clean steps here.
        /// </summary>
        protected abstract void Finish();

        /// <summary>
        /// Run the above steps in this order.  
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            Start();

            bool read = await ReadDataAsync();

            if (read == true)
            {
                bool transform = await TransformDataAsync();
                if (transform == true)
                {
                    await ExportDataAsync();
                }
            }

            Finish();
        }
    }
}