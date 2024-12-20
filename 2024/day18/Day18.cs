using Position = (int x, int y);

namespace _2024.Day18
{
    internal class Day18
    {
        public enum Direction
        {
            Left,
            Down,
            Right,
            Up,
        }

        public int FindBestPath(char[][] map, Position start, Position end)
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

                        distance[next.y, next.x] = distance[current.y, current.x] + 1;
                        queue.Enqueue(next);
                    }
                }
            }

            return distance[end.y, end.x] == int.MaxValue ? -1 : distance[end.y, end.x];
        }

        public void SolvePart1(int part)
        {
            string input = File.ReadAllText("input");
            List<string> lines = input.Split(Environment.NewLine).ToList();
            List<Position> positions = new List<Position>();
            int space = 71;

            for (int i = 0; i < 1024; i++)
            {
                string[] parts = lines[i].Split(",");
                positions.Add((int.Parse(parts[0]), int.Parse(parts[1])));
            }

            char[][] map = new char[space][];
            for (int i = 0; i < space; i++)
            {
                map[i] = new char[space];
                for (int j = 0; j < space; j++)
                    map[i][j] = '.';
            }


            foreach (Position pos in positions)
                map[pos.y][pos.x] = '#';

            Position startPos = (0, 0);
            Position endPos = (space - 1, space - 1);

            Console.WriteLine(FindBestPath(map, startPos, endPos));
        }

        public void SolvePart2(int part)
        {
            string input = File.ReadAllText("input");
            List<string> lines = input.Split(Environment.NewLine).ToList();
            List<Position> positions = new List<Position>();
            int space = 71;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split(",");
                positions.Add((int.Parse(parts[0]), int.Parse(parts[1])));
            }

            for (int bytesNb = 0; bytesNb < positions.Count; bytesNb++)
            {
                char[][] map = new char[space][];
                for (int i = 0; i < space; i++)
                {
                    map[i] = new char[space];
                    for (int j = 0; j < space; j++)
                        map[i][j] = '.';
                }

                for (int i = 0; i < bytesNb; i++)
                    map[positions[i].y][positions[i].x] = '#';

                Position startPos = (0, 0);
                Position endPos = (space - 1, space - 1);

                if (FindBestPath(map, startPos, endPos) == -1)
                {
                    Console.WriteLine(positions[bytesNb - 1].x + "," + positions[bytesNb - 1].y);
                    break;
                }
            }
        }
    }
}
