using CheapGasFinderAPI.Domain;

namespace CheapGasFinderAPI.Services;

public class GasStationsService 
{
    private static readonly List<GasStation> GasStationsRepository = [];

    public void Create(GasStation gasStation)
    {
        GasStationsRepository.Add(gasStation);
    }

    public GasStation? Get(Guid id) 
    {
        return GasStationsRepository.Find(x => x.Id == id);
    }
}