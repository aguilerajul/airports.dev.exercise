using Airports.Dev.Exercise.Services.Interfaces;
using Airports.Dev.Exercise.Services.Services;
using Airports.Dev.Exercise.Web.Models;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Airports.Dev.Exercise.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAirportService _airportService;

        public HomeController()
        {
            _airportService = new AirportService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Airports()
        {
            var airports = Caching.Caching.GetObjectFromCache("Airports", 30, (() => _airportService.GetAiports()));
            return View(airports);
        }

        [HttpPost]
        public ActionResult Airports(string countryName)
        {
            var airports = Caching.Caching.GetObjectFromCache("Airports", 30, (() => _airportService.GetAiports()));
            if (airports.Any())
            {
                if (string.IsNullOrEmpty(countryName))
                {
                    return View(airports);
                }

                var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
                var region = regions.FirstOrDefault(x => x.EnglishName.Contains(countryName));

                airports = airports.Where(x => x.ISO.Equals(region.Name));
            }

            return View(airports);
        }

        public ActionResult AirportsDistance()
        {
            return View(new AirportDistanceModel());
        }

        [HttpPost]
        public ActionResult AirportsDistance(string iata1, string iata2)
        {
            AirportDistanceModel airportDistanceModel = new AirportDistanceModel();
            try
            {
                var airports = Caching.Caching.GetObjectFromCache("Airports", 30, (() => _airportService.GetAiports()));
                if (airports.Any())
                {
                    var airportByIata1 = airports.FirstOrDefault(x => x.IATA.Equals(iata1, System.StringComparison.InvariantCultureIgnoreCase));
                    var airportByIata2 = airports.FirstOrDefault(x => x.IATA.Equals(iata2, System.StringComparison.InvariantCultureIgnoreCase));

                    airportDistanceModel.DistanceMessage = _airportService.GetDistanceBetweenTwoAirport(airportByIata1, airportByIata2);

                    return View(airportDistanceModel);
                }

                return View(airportDistanceModel);
            }
            catch (System.Exception ex)
            {
                airportDistanceModel.DistanceMessage = ex.Message;
                return View(airportDistanceModel);
            }
        }
    }
}