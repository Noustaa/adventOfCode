namespace _2024.Day8
{
    internal class Day8
    {
        public enum Direction { N, NE, E, SE, S, SW, W, NW }
        public static Direction GetOppositeDirection(Direction direction)
        {
            return direction switch
            {
                Direction.N => Direction.S,
                Direction.NE => Direction.SW,
                Direction.E => Direction.W,
                Direction.SE => Direction.NW,
                Direction.S => Direction.N,
                Direction.SW => Direction.NE,
                Direction.W => Direction.E,
                Direction.NW => Direction.SE
            };
        }

        public static double Distance(double x1, double y1, double x2, double y2)
            => Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));

        public static List<(int y, int x, Direction direction)> FindValidPairs(char[][] map, (int y, int x) antenna)
        {
            List<(int y, int x, Direction direction)> validPairs = new();
            (int y, int x) target = (antenna.y, antenna.x);
            while (true)
            {
                try
                {
                    target.x++;
                    double distance = Distance(antenna.x, antenna.y, target.x, target.y);
                    if (map[antenna.y][antenna.x] == map[target.y][target.x])
                    {
                        if (antenna.x < target.x)
                            validPairs.Add((target.y, target.x, Direction.E));
                        else if (antenna.x > target.x)
                            validPairs.Add((target.y, target.x, Direction.W));
                        else
                            validPairs.Add((target.y, target.x, Direction.S));
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    target.x = -1;
                    if (target.y++ >= map.Length)
                        break;
                }
            }
            return validPairs;
        }

        public static (int y, int x) FindAntinode(char[][] map, (int y, int x) antenna, double distance, Direction direction)
        {
            (int y, int x) antinode = (antenna.y, antenna.x);
            while (true)
            {
                try
                {
                    switch (GetOppositeDirection(direction))
                    {
                        case Direction.N:
                            antinode.y--;
                            break;
                        case Direction.NE:
                            antinode.y--;
                            antinode.x++;
                            break;
                        case Direction.E:
                            antinode.x++;
                            break;
                        case Direction.SE:
                            antinode.y++;
                            antinode.x++;
                            break;
                        case Direction.S:
                            antinode.y++;
                            break;
                        case Direction.SW:
                            antinode.y++;
                            antinode.x--;
                            break;
                        case Direction.W:
                            antinode.x--;
                            break;
                        case Direction.NW:
                            antinode.y--;
                            antinode.x--;
                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }
            }
        }

        public static bool IsPointInMap(char[][] map, (int y, int x) point)
        {
            return point.y >= 0 && point.y < map.Length && point.x >= 0 && point.x < map[point.y].Length;
        }

        public static void SolvePart1()
        {
            string fileContent = File.ReadAllText("test");
            char[][] antennasMap = fileContent.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();
            char[][] antinodesMap = new char[antennasMap.Length][];
            for (int i = 0; i < antennasMap.Length; i++)
                antinodesMap[i] = new char[antennasMap[i].Length];

            for (int i = 0; i < antennasMap.Length; i++)
            {
                for (int j = 0; j < antennasMap[i].Length; j++)
                {
                    if (antennasMap[i][j] == '.')
                        continue;

                    List<(int y, int x, Direction direction)> validPairs = FindValidPairs(antennasMap, (i, j));

                    int a = 0;
                }
            }

            int result = 0;

            Console.WriteLine(result);
        }

        public static void SolvePart2()
        {
            string fileContent = File.ReadAllText("input");
            List<string> equations = fileContent.Split(Environment.NewLine).ToList();

            int result = 0;

            Console.WriteLine(result);
        }
    }
}
