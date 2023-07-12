using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessories;
public interface ITombola
{
    public int NextNumber();

    public ImmutableList<int> GetDrawnNumbers();
}
