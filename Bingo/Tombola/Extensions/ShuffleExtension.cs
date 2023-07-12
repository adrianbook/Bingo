namespace Accessories.Extensions
{
    public static class ShuffleExtension
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IList<T> ShuffleAndReturn<T>(this IList<T> list)
        {
            list.Shuffle();
            return list;
        }
    }
}
