using System.Collections.Concurrent;

namespace _2024.Day11
{
    internal class Day11
    {
        public void Solve(int blinkCount)
        {
            string fileContent = File.ReadAllText("input");
            ConcurrentDictionary<string, long> stones = [];
            foreach (string stone in fileContent.Split(' '))
                stones.AddOrUpdate(stone, 1, (_, value) => value += 1);

            for (int i = 0; i < blinkCount; i++)
            {
                Dictionary<string, long> bufferStones = new(stones);
                foreach (string stone in bufferStones.Keys)
                {
                    long count = bufferStones[stone];
                    stones[stone] -= count;

                    if (long.Parse(stone) == 0)
                        stones.AddOrUpdate("1", count, (_, value) => value += count);
                    else if (stone.Length % 2 == 0)
                    {
                        string first = long.Parse(stone[0..(stone.Length / 2)]).ToString();
                        string second = long.Parse(stone[(stone.Length / 2)..]).ToString();
                        stones.AddOrUpdate(first, count, (_, value) => value += count);
                        stones.AddOrUpdate(second, count, (_, value) => value += count);
                    }
                    else
                        stones.AddOrUpdate(
                            (long.Parse(stone) * 2024).ToString(),
                            count,
                            (_, value) => value += count);
                }
            }

            Console.WriteLine(stones.Values.Sum());
        }
    }
}
