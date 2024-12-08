namespace _2024.Day8
{
    internal class Day8
    {
        public static List<(int y, int x)> FindValidPairs(char[][] map, (int y, int x) antenna)
        {
            List<(int y, int x)> validPairs = [];
            (int y, int x) target = (antenna.y, antenna.x);
            while (true)
            {
                if (++target.x >= map[0].Length)
                {
                    target.x = 0;
                    if (++target.y >= map.Length)
                        break;
                }

                if (map[antenna.y][antenna.x] == map[target.y][target.x])
                    validPairs.Add((target.y, target.x));
            }
            return validPairs;
        }

        public static bool IsPointInMap(char[][] map, (int y, int x) point)
            => point.y >= 0 && point.y < map.Length && point.x >= 0 && point.x < map[point.y].Length;

        public static void SolvePart1()
        {
            string fileContent = File.ReadAllText("input");
            char[][] antennasMap = fileContent.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();

            HashSet<(int y, int x)> antinodes = [];
            for (int i = 0; i < antennasMap.Length; i++)
            {
                for (int j = 0; j < antennasMap[i].Length; j++)
                {
                    if (antennasMap[i][j] == '.')
                        continue;

                    List<(int y, int x)> validPairs = FindValidPairs(antennasMap, (i, j));

                    foreach (var point in validPairs)
                    {
                        (int y, int x) distance = (Math.Abs(point.y - i), Math.Abs(point.x - j));

                        if (point.x > j)
                        {
                            if (IsPointInMap(antennasMap, (point.y + distance.y, point.x + distance.x)))
                                antinodes.Add((point.y + distance.y, point.x + distance.x));
                            if (IsPointInMap(antennasMap, (i - distance.y, j - distance.x)))
                                antinodes.Add((i - distance.y, j - distance.x));
                        }
                        else
                        {
                            if (IsPointInMap(antennasMap, (point.y + distance.y, point.x - distance.x)))
                                antinodes.Add((point.y + distance.y, point.x - distance.x));
                            if (IsPointInMap(antennasMap, (i - distance.y, j + distance.x)))
                                antinodes.Add((i - distance.y, j + distance.x));
                        }
                    }
                }
            }
            Console.WriteLine(antinodes.Count);
        }

        public static void SolvePart2()
        {
            string fileContent = File.ReadAllText("input");
            char[][] antennasMap = fileContent.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();

            HashSet<(int y, int x)> antinodes = [];
            for (int i = 0; i < antennasMap.Length; i++)
            {
                for (int j = 0; j < antennasMap[i].Length; j++)
                {
                    if (antennasMap[i][j] == '.')
                        continue;
                    else
                        antinodes.Add((i, j));

                    List<(int y, int x)> validPairs = FindValidPairs(antennasMap, (i, j));

                    foreach (var point in validPairs)
                    {
                        (int y, int x) distance = (Math.Abs(point.y - i), Math.Abs(point.x - j));
                        (int y, int x) initialPoint = (i, j);
                        (int y, int x) targetPoint = (point.y, point.x);
                        do
                        {
                            if (point.x > j)
                            {
                                initialPoint = (initialPoint.y - distance.y, initialPoint.x - distance.x);
                                targetPoint = (targetPoint.y + distance.y, targetPoint.x + distance.x);
                            }
                            else
                            {
                                initialPoint = (initialPoint.y - distance.y, initialPoint.x + distance.x);
                                targetPoint = (targetPoint.y + distance.y, targetPoint.x - distance.x);
                            }
                            if (IsPointInMap(antennasMap, targetPoint))
                                antinodes.Add(targetPoint);
                            if (IsPointInMap(antennasMap, initialPoint))
                                antinodes.Add(initialPoint);
                        } while (IsPointInMap(antennasMap, initialPoint) || IsPointInMap(antennasMap, targetPoint));
                    }
                }
            }
            Console.WriteLine(antinodes.Count);
        }
    }
}
