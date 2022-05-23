using InfluxDb.Exercise.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InfluxDb.Exercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirplaneCoordianteController : ControllerBase
    {
        private readonly IAirplaneCoordinateRepository _airplaneCoordinateRepository;

        public AirplaneCoordianteController(IAirplaneCoordinateRepository airplaneCoordinateRepository)
        {
            _airplaneCoordinateRepository = airplaneCoordinateRepository;
        }

        [HttpPost("{count}")]
        public async Task<ActionResult> CreateAirplaneCoordinates(int count)
        {
            await _airplaneCoordinateRepository.CreateAirplaneCooridnates(count);

            return new OkResult();
        }

        [HttpGet]
        public async Task<List<AirplaneCoordinate>> GetAirplaneCooridnates() 
        {
            var results = await _airplaneCoordinateRepository.ReadAirplaneCoordinates();

            return results;
        }

    }
}