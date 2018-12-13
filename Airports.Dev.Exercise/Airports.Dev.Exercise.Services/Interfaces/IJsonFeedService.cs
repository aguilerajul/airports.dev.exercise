using System.Collections.Generic;
using Airports.Dev.Exercise.Services.Models;

namespace Airports.Dev.Exercise.Services.Interfaces
{
    internal interface IJsonFeedService
    {
        void DowloadJsonFile();

        bool IsJsonFileDownloaded();

        string GetJsonData();
    }
}
