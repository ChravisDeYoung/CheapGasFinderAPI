using CheapGasFinderAPI.Domain;
using CheapGasFinderAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CheapGasFinderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GasStationsController(ILogger<GasStationsController> logger, GasStationsService gasStationsService) : ControllerBase
{
    private readonly ILogger<GasStationsController> _logger = logger;
    private readonly GasStationsService _gasStationsService = gasStationsService;

    [HttpPost]
    public IActionResult Create(GasStationRequest request)
    {
        var gasStation = request.ToDomain();

        _gasStationsService.Create(gasStation);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { gasStation.Id },
            value: GasStationResponse.FromDomain(gasStation));
    }

    [HttpGet("id:guid")]
    public IActionResult Get(Guid id)
    {
        var gasStation = _gasStationsService.Get(id);
        
        if (gasStation is null)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Gas Station not found (id: {id})."
            );
        }
        else 
        {
            return Ok(GasStationResponse.FromDomain(gasStation));
        }
    }

    public record GasStationRequest(
        string Name, 
        string Address)
    {
        public GasStation ToDomain() 
        {
            return new GasStation 
            {
                Name = Name,
                Address = Address,
            };
        }
    }

    public record GasStationResponse(
        Guid Id,
        string Name, 
        string Address)
    {
        public static GasStationResponse FromDomain(GasStation gasStation) 
        {
            return new GasStationResponse(
                gasStation.Id,
                gasStation.Name,
                gasStation.Address
            );
        }
    }
}
