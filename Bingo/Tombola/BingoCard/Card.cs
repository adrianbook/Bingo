using System.Collections.Immutable;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AccessoriesTest")]
namespace Accessories.BingoCard;

public class Card
{
    public static readonly int rowLength = 5;
    public static readonly int columnLength = 5;
    public string Id { get; init; }
    public List<ImmutableArray<int>> Rows => GetRows().ToList();
    public List<ImmutableArray<int>> Columns => GetColumns().ToList();

    internal Card(IEnumerable<int> numbers)
    {
        if (numbers.Count() != rowLength * columnLength)
        {
            throw new ArgumentException($"numbers must contain {rowLength * columnLength} elements");
        }
        Id = numbers.Select(n => n < 10 ? "0"+n.ToString(): n.ToString()).Aggregate((prev, cur)=> prev + cur);
        
        Numbers = numbers.ToImmutableArray();
    }
    public override int GetHashCode() => Id.GetHashCode();
    public override bool Equals(object? obj) => obj is Card c && c.Id == Id;
    public bool HasBingo(HashSet<int> drawnNumbers, int requiredNumberOfRows)
    {
        if (requiredNumberOfRows < 1 || requiredNumberOfRows > columnLength)
        {
            throw new ArgumentException($"requiredNumberOfRows must be between 1 and {columnLength}");
        }
        if (drawnNumbers.Count < rowLength)
        {
            return false;
        }
        return IsBingo(drawnNumbers,requiredNumberOfRows, GetRows());
    }

    private static bool IsBingo(HashSet<int> drawnNumbers, int requiredMatches, ImmutableArray<int>[] bingoConditions)
    {
        int matches = 0;
        for (int i = 0; i < bingoConditions.Count(); i++)
        {
            if (drawnNumbers.IsSupersetOf(bingoConditions[i]))
            {
                matches++;
                if (matches == requiredMatches)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private ImmutableArray<int>[] GetRows()
    {
        var result = new ImmutableArray<int>[rowLength];
        for (int i = 0; i < rowLength; i++)
        {
            result[i] = GetRow(i);
        }
        return result;
    }

    private ImmutableArray<int>[] GetColumns()
    {
        var result = new ImmutableArray<int>[columnLength];
        for (int i = 0; i < columnLength; i++)
        {
            result[i] = GetColumn(i);
        }
        return result;
    }

    internal ImmutableArray<int> GetRow(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex > 4)
        {
            throw new ArgumentException("rowIndex must be between 0 and 4");
        }
        int[] result = new int[rowLength];
        for (int i = 0; i < rowLength; i++)
        {
            int index = rowIndex + (columnLength * i);
            result[i] = Numbers[index];
        }
        return result.ToImmutableArray();
    }

    internal ImmutableArray<int> GetColumn(int columnIndex)
    {
        if (columnIndex < 0 || columnIndex > 4)
        {
            throw new ArgumentException("columnIndex must be between 0 and 4");
        }
        int[] result = new int[columnLength];
        for (int i = 0; i < columnLength; i++)
        {
            result[i] = Numbers[(columnIndex * columnLength) + i];
        }
        return result.ToImmutableArray();
    }


}
