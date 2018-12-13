using Airports.Dev.Exercise.Services.Interfaces;
using Airports.Dev.Exercise.Services.Services;
using System;
using System.Linq;
using System.Web.Http;

namespace Airports.Dev.Exercise.Web.Api
{
    public class AirportController : ApiController
    {
        private readonly IAirportService _airportService;

        public AirportController()
        {
            _airportService = new AirportService();
        }
        
        public string GetDistance([FromUri] string iataCode1, [FromUri] string iataCode2)
        {            
            try
            {
                var airports = Caching.Caching.GetObjectFromCache("Airports", 30, (() => _airportService.GetAiports()));
                if (airports.Any())
                {
                    var airportByIata1 = airports.FirstOrDefault(x => x.IATA.Equals(iataCode1, StringComparison.InvariantCultureIgnoreCase));
                    var airportByIata2 = airports.FirstOrDefault(x => x.IATA.Equals(iataCode2, StringComparison.InvariantCultureIgnoreCase));

                    return _airportService.GetDistanceBetweenTwoAirport(airportByIata1, airportByIata2);
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
