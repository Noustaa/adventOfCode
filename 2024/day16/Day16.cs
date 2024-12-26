namespace _2024.Day16
{
    internal class Day16
    {
        public enum Direction
        {
            Left,
            Down,
            Right,
            Up,
        }

        public int FintBestPath(char[][] map, (int x, int y, Direction direction) start, (int x, int y) end)
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

            Queue<(int x, int y, Direction direction)> queue = new();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                (int x, int y, Direction direction) current = queue.Dequeue();

                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    (int x, int y, Direction direction) next = (current.x, current.y, direction);

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

                    int distanceGap = 1;
                    if (direction != current.direction)
                        distanceGap = 1001;

                    if (distance[next.y, next.x] > distance[current.y, current.x] + distanceGap)
                    {
                        if (distance[next.y, next.x] != int.MaxValue)
                            queue = new(queue.Where(point => point.x != next.x || point.y != next.y));

                        distance[next.y, next.x] = distance[current.y, current.x] + distanceGap;
                        queue.Enqueue(next);
                    }
                }
            }

            return distance[end.y, end.x] == int.MaxValue ? -1 : distance[end.y, end.x];
        }

        public void Solve()
        {
            string fileContent = File.ReadAllText("input");
            char[][] map = fileContent.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();
            char[,] map2 = new char[map.Length, map[0].Length];

            (int x, int y, Direction direction) start = (1, map.Length - 2, Direction.Right);
            (int x, int y) end = (map[1].Length - 2, 1);

            int bestPath = FintBestPath(map, start, end);

            Console.WriteLine(bestPath);
        }
    }
}
