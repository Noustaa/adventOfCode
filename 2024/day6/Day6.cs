namespace _2024.Day6
{
    public static class Day6
    {
        public static List<char> GuardIndicators = ['^', 'v', '<', '>'];
        public static char WallIndicator = '#';
        public static char FreeIndicator = '.';
        public enum Direction { UP, DOWN, LEFT, RIGHT };

        public static (int, int) GetGuardPosition(char[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (GuardIndicators.Contains(map[i][j]))
                    {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

        public static Direction GetDirection(char guardIndicator)
        {
            switch (guardIndicator)
            {
                case '^':
                    return Direction.UP;
                case 'v':
                    return Direction.DOWN;
                case '<':
                    return Direction.LEFT;
                case '>':
                    return Direction.RIGHT;
                default:
                    return Direction.UP;
            }
        }

        public static (int, int) StepForward(Direction direction, (int, int) currentPosition)
        {
            switch (direction)
            {
                case Direction.UP:
                    return (currentPosition.Item1 - 1, currentPosition.Item2);
                case Direction.DOWN:
                    return (currentPosition.Item1 + 1, currentPosition.Item2);
                case Direction.LEFT:
                    return (currentPosition.Item1, currentPosition.Item2 - 1);
                case Direction.RIGHT:
                    return (currentPosition.Item1, currentPosition.Item2 + 1);
                default:
                    return currentPosition;
            }
        }

        public static (int, int) StepBackward(Direction direction, (int, int) currentPosition)
        {
            switch (direction)
            {
                case Direction.UP:
                    return (currentPosition.Item1 + 1, currentPosition.Item2);
                case Direction.DOWN:
                    return (currentPosition.Item1 - 1, currentPosition.Item2);
                case Direction.LEFT:
                    return (currentPosition.Item1, currentPosition.Item2 + 1);
                case Direction.RIGHT:
                    return (currentPosition.Item1, currentPosition.Item2 - 1);
                default:
                    return currentPosition;
            }
        }

        public static Direction TurnRight(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    return Direction.RIGHT;
                case Direction.DOWN:
                    return Direction.LEFT;
                case Direction.LEFT:
                    return Direction.UP;
                case Direction.RIGHT:
                    return Direction.DOWN;
                default:
                    return direction;
            }
        }

        public static void SolvePart1()
        {
            string fileContent = File.ReadAllText("input");
            char[][] map = fileContent.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();

            HashSet<(int, int)> visitedCoordinates = new HashSet<(int, int)>();

            (int x, int y) guardPosition = GetGuardPosition(map);
            Direction direction = GetDirection(map[guardPosition.x][guardPosition.y]);

            do
            {
                visitedCoordinates.Add(guardPosition);
                guardPosition = StepForward(direction, guardPosition);
                try
                {
                    if (map[guardPosition.x][guardPosition.y] == WallIndicator)
                    {
                        guardPosition = StepBackward(direction, guardPosition);
                        direction = TurnRight(direction);
                    }
                    else
                        continue;
                }
                catch (IndexOutOfRangeException)
                {
                    // Means the guard got out of the map
                    break;
                }
            } while (true);

            int result = visitedCoordinates.Count;

            Console.WriteLine(result);
        }

        public static void SolvePart2()
        {
            string fileContent = File.ReadAllText("input");
            char[][] map = fileContent.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();

            int result = 0;

            int limit = 10000;

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == WallIndicator || GuardIndicators.Contains(map[i][j]))
                        continue;
                    else
                        map[i][j] = WallIndicator;

                    HashSet<(int, int)> visitedCoordinates = new HashSet<(int, int)>();

                    (int x, int y) guardPosition = GetGuardPosition(map);
                    Direction direction = GetDirection(map[guardPosition.x][guardPosition.y]);

                    int steps = 0;
                    do
                    {
                        if (steps >= limit)
                        {
                            result++;
                            break;
                        }

                        visitedCoordinates.Add(guardPosition);
                        guardPosition = StepForward(direction, guardPosition);
                        try
                        {
                            if (map[guardPosition.x][guardPosition.y] == WallIndicator)
                            {
                                guardPosition = StepBackward(direction, guardPosition);
                                direction = TurnRight(direction);
                            }
                            else
                            {
                                steps++;
                                continue;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            // Means the guard got out of the map
                            break;
                        }
                    } while (true);

                    map[i][j] = FreeIndicator;
                }
            }

            Console.WriteLine(result);
        }
    }
}
