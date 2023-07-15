using Accessories.TombolaCreation;

namespace BingoHall.Tombolas.Services;

public class TombolaService : ITombolaService
{
    private readonly Dictionary<string, ITombola> _tombolas = new();
    private readonly ITombolaFactory _tombolaFactory;

    public TombolaService()
    {
        _tombolas = new();
        _tombolaFactory = new TombolaFactory();
    }

    public int GetNextNumber(string tombolaId)
    {
        if (!_tombolas.ContainsKey(tombolaId))
        {
            throw new ArgumentException("Tombola not found", nameof(tombolaId));
        }
        return _tombolas[tombolaId].NextNumber();
    }

    public string RegisterTombola()
    {
        var tombolaId = Guid.NewGuid().ToString("N");
        var tombola = _tombolaFactory.GetRegularTombola();
        _tombolas.Add(tombolaId, tombola);
        return tombolaId;
    }
}
