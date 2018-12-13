using Airports.Dev.Exercise.Services.Models;
using System.Collections.Generic;

namespace Airports.Dev.Exercise.Services.Interfaces
{
    public interface IAirportService
    {
        IEnumerable<Airport> GetAiports();

        string GetDistanceBetweenTwoAirport(Airport airport1, Airport airport2);
    }
}
