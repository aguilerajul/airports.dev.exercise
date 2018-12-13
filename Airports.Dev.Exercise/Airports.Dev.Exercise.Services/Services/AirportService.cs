using Airports.Dev.Exercise.Services.Interfaces;
using Airports.Dev.Exercise.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;

namespace Airports.Dev.Exercise.Services.Services
{
    public class AirportService : IAirportService
    {
        private readonly log4net.ILog _logger;
        private readonly IJsonFeedService _jsonFeedService;

        public AirportService()
        {
            _jsonFeedService = new JsonFeedService();
            _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public IEnumerable<Airport> GetAiports()
        {
            try
            {
                if (!_jsonFeedService.IsJsonFileDownloaded())
                {
                    _jsonFeedService.DowloadJsonFile();
                }

                string jsonAirportData = _jsonFeedService.GetJsonData();
                if (!string.IsNullOrEmpty(jsonAirportData))
                {
                    IEnumerable<Airport> airportsResult = JsonConvert.DeserializeObject<IEnumerable<Airport>>(jsonAirportData);
                    if (airportsResult.Any())
                    {
                        return airportsResult.OrderBy(x => x.Name);
                    }
                }

                return Enumerable.Empty<Airport>();
            }
            catch (Exception ex)
            {
                _logger.Error("AirportService: Error getting Airports", ex);
                throw ex;
            }
        }

        public string GetDistanceBetweenTwoAirport(Airport airport1, Airport airport2)
        {
            try
            {  
                if (airport1 == null || airport2 == null)
                {
                    return $"The distance cannot be calculated because one of IATA was not found in the IATA list: {airport1.IATA}, {airport2.IATA}";
                }

                GeoCoordinate geoIata1 = new GeoCoordinate(Math.Round(airport1.Lat, 2), Math.Round(airport1.Lon, 2));
                GeoCoordinate geoIata2 = new GeoCoordinate(Math.Round(airport2.Lat, 2), Math.Round(airport2.Lon, 2));

                double distanceTo = geoIata1.GetDistanceTo(geoIata2);

                return $"The distance between {airport1.IATA} and {airport2.IATA} is: {distanceTo.ToString()}";
            }
            catch (System.Exception ex)
            {
                _logger.Error("AirportService: Error on GetDistanceBetweenTwoAirport", ex);
                throw ex;
            }
        }
    }
}
