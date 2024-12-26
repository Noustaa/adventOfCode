using Position = (int x, int y);

namespace _2024.Day20
{
    internal class Day20
    {
        public enum Direction
        {
            Left,
            Down,
            Right,
            Up,
        }

        public List<Position> FindBestPath(char[][] map, Position start, Position end)
        {
            int width = map[0].Length;
            int height = map.Length;
            int[,] distance = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    distance[i, j] = int.MaxValue;
                }
            }
            distance[start.y, start.x] = 0;

            List<Position> bestPath = [];

            Queue<Position> queue = new();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Position current = queue.Dequeue();

                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    Position next = (current.x, current.y);

                    switch (direction)
                    {
                        case Direction.Right:
                            next.x++;
                            break;
                        case Direction.Left:
                            next.x--;
                            break;
                        case Direction.Up:
                            next.y--;
                            break;
                        case Direction.Down:
                            next.y++;
                            break;
                    }

                    if (next.x < 0 || next.x >= width || next.y < 0 || next.y >= height)
                        continue;

                    if (map[next.y][next.x] == '#')
                        continue;

                    if (distance[next.y, next.x] > distance[current.y, current.x] + 1)
                    {
                        if (distance[next.y, next.x] != int.MaxValue)
                            queue = new(queue.Where(point => point.x != next.x || point.y != next.y));

                        bestPath.Add(current);
                        distance[next.y, next.x] = distance[current.y, current.x] + 1;
                        queue.Enqueue(next);
                    }
                }
            }

            return bestPath;
        }

        public void Solve()
        {
            string input = File.ReadAllText("input");
            char[][] map = input.Split(Environment.NewLine).Select(line => line.ToCharArray()).ToArray();

            Position start = (0, 0);
            Position end = (0, 0);

            for (int i = 0; i < map.Length; i++)
                for (int j = 0; j < map[0].Length; j++)
                {
                    if (map[i][j] == 'S')
                        start = (j, i);
                    if (map[i][j] == 'E')
                        end = (j, i);
                }

            List<Position> bestPath = FindBestPath(map, start, end);
            bestPath.Add(end);
            List<int> possibleTimeSaveCheat = [];

            possibleTimeSaveCheat = [];
            int width = map[0].Length;
            int height = map.Length;
            foreach (Position point in bestPath)
            {
                Position current = point;
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    Position next = (current.x, current.y);

                    switch (direction)
                    {
                        case Direction.Right:
                            next.x++;
                            break;
                        case Direction.Left:
                            next.x--;
                            break;
                        case Direction.Up:
                            next.y--;
                            break;
                        case Direction.Down:
                            next.y++;
                            break;
                    }

                    if (next.x < 0 || next.x >= width || next.y < 0 || next.y >= height)
                        continue;

                    if (map[next.y][next.x] == '#')
                    {
                        switch (direction)
                        {
                            case Direction.Right:
                                next.x++;
                                break;
                            case Direction.Left:
                                next.x--;
                                break;
                            case Direction.Up:
                                next.y--;
                                break;
                            case Direction.Down:
                                next.y++;
                                break;
                        }

                        if (next.x < 0 || next.x >= width || next.y < 0 || next.y >= height)
                            continue;

                        if (map[next.y][next.x] == '.' || map[next.y][next.x] == 'E')
                        {
                            int nextPointIndex = bestPath.IndexOf((next.x, next.y));
                            int currentPointIndex = bestPath.IndexOf((current.x, current.y));
                            if (nextPointIndex > currentPointIndex)
                                possibleTimeSaveCheat.Add(nextPointIndex - currentPointIndex - 2);
                        }
                    }
                }
            }
            Console.WriteLine(possibleTimeSaveCheat.Where(savedTime => savedTime >= 100).Count());
        }
    }
}
