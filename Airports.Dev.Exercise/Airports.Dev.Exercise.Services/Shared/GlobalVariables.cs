using System.IO;

namespace Airports.Dev.Exercise.Services.Shared
{
    public static class GlobalVariables
    {
        public const string JSON_FEED_FILE_NAME = "Airports-Feed.json";

        public static string JsonFeedUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Airports.Dev.Json.Url"];
            }
        }

        public static string JsonFilePathToDownload
        {
            get
            {
                return Path.Combine(Utility.GetExecutingDirectoryName(), System.Configuration.ConfigurationManager.AppSettings["Airports.Dev.Json.Downloaded.Path"]);
            }
        }
    }
}
