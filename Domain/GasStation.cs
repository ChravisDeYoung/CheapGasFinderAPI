namespace CheapGasFinderAPI.Domain;

public class GasStation 
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
    public required string Address { get; init; }
}