using System;
using System.IO;
using System.Net.Http;
using Airports.Dev.Exercise.Services.Interfaces;
using Airports.Dev.Exercise.Services.Shared;

namespace Airports.Dev.Exercise.Services.Services
{
    internal class JsonFeedService : IJsonFeedService
    {
        private readonly log4net.ILog _logger;
        public JsonFeedService()
        {
            _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// Download the Json file provided for the Exercise and saved in the current Library in the bin/JsonFileDownloaded folder with the name: Airports-Feed.json        
        /// </summary>
        public void DowloadJsonFile()
        {
            try
            {
                HttpClient httpClient = new HttpClient();                
                Uri jsonFeedUrl = new Uri(GlobalVariables.JsonFeedUrl);
                string finalFilePath = string.Empty;

                var response = httpClient.GetAsync(jsonFeedUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var streamDataResult = response.Content.ReadAsStreamAsync().Result;                    
                    if (!Directory.Exists(GlobalVariables.JsonFilePathToDownload))
                        Directory.CreateDirectory(GlobalVariables.JsonFilePathToDownload);

                    finalFilePath = Path.Combine(GlobalVariables.JsonFilePathToDownload, GlobalVariables.JSON_FEED_FILE_NAME);
                    if (File.Exists(finalFilePath))
                        File.Delete(finalFilePath);

                    using (FileStream fileStream = File.Create(finalFilePath, (int)streamDataResult.Length, FileOptions.WriteThrough))
                    {
                        byte[] bytesInStream = new byte[streamDataResult.Length];
                        streamDataResult.Read(bytesInStream, 0, bytesInStream.Length);
                        fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("JSONFeedService: Error downloading Airports JSON file", ex);
                throw ex;
            }
        }

        /// <summary>
        /// return true if the file was created successfully.
        /// </summary>
        /// <returns></returns>
        public bool IsJsonFileDownloaded()
        {
            try
            {
                string finalFilePath = Path.Combine(GlobalVariables.JsonFilePathToDownload, GlobalVariables.JSON_FEED_FILE_NAME);
                return File.Exists(finalFilePath);                    
            }
            catch (Exception ex)
            {
                _logger.Error("JSONFeedService: Error validating if JSON file was created", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Read the JSON file donwloaded and get the info as string.
        /// </summary>
        /// <returns></returns>
        public string GetJsonData()
        {
            string jsonFileDownloadedPath = Path.Combine(GlobalVariables.JsonFilePathToDownload, GlobalVariables.JSON_FEED_FILE_NAME);
            using (StreamReader sr = new StreamReader(jsonFileDownloadedPath))
            {
                return sr.ReadToEnd().ToString();
            }
        }
    }
}
