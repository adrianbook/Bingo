namespace BingoHall.Tombolas.Services;

public interface ITombolaService
{
    public string RegisterTombola();
    public int GetNextNumber(string tombolaId);
}
