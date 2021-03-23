using System;

namespace App.Core.Domain.Interfaces
{
    internal interface ILogger
    {
        void LogInfo(string message);

        void LogDebug(string message);

        void LogWarning(string message);

        void LogException(string message);

        void LogException(string message, Exception exception);
    }
}