namespace Accessories.TombolaCreation;
public class TombolaFactory : ITombolaFactory
{
    public ITombola GetRegularTombola()
    {
        return new Tombola(75);
    }
}
