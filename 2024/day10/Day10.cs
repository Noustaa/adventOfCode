namespace _2024.Day10
{
    internal class Day10
    {
        public int Result = 0;
        public const int Target = 9;
        public void CheckTrailhead(int[][] map, (int y, int x) startingPoint, HashSet<(int, int)> visited = null)
        {
            int current = map[startingPoint.y][startingPoint.x];
            int findNext = current + 1;

            if (current == Target && visited != null && !visited.Contains((startingPoint.y, startingPoint.x)))
            {
                Result++;
                visited.Add((startingPoint.y, startingPoint.x));
            }
            else if (current == Target && visited == null)
                Result++;

            if (startingPoint.y + 1 < map.Length)
                if (map[startingPoint.y + 1][startingPoint.x] == findNext)
                    CheckTrailhead(map, (startingPoint.y + 1, startingPoint.x), visited);
            if (startingPoint.x + 1 < map[startingPoint.y].Length)
                if (map[startingPoint.y][startingPoint.x + 1] == findNext)
                    CheckTrailhead(map, (startingPoint.y, startingPoint.x + 1), visited);
            if (startingPoint.y - 1 >= 0)
                if (map[startingPoint.y - 1][startingPoint.x] == findNext)
                    CheckTrailhead(map, (startingPoint.y - 1, startingPoint.x), visited);
            if (startingPoint.x - 1 >= 0)
                if (map[startingPoint.y][startingPoint.x - 1] == findNext)
                    CheckTrailhead(map, (startingPoint.y, startingPoint.x - 1), visited);
        }

        public void SolvePart1()
        {
            string fileContent = File.ReadAllText("input");
            int[][] map = fileContent
                .Split(Environment.NewLine)
                .Select(x => x.Select(y => int.Parse(y.ToString())).ToArray())
                .ToArray();

            for (int i = 0; i < map.Length; i++)
                for (int j = 0; j < map[i].Length; j++)
                    if (map[i][j] == 0)
                    {
                        HashSet<(int y, int x)> visited = [];
                        CheckTrailhead(map, (i, j), visited);
                    }

            Console.WriteLine(Result);
        }

        public void SolvePart2()
        {
            string fileContent = File.ReadAllText("input");
            int[][] map = fileContent
                .Split(Environment.NewLine)
                .Select(x => x.Select(y => int.Parse(y.ToString())).ToArray())
                .ToArray();

            for (int i = 0; i < map.Length; i++)
                for (int j = 0; j < map[i].Length; j++)
                    if (map[i][j] == 0)
                        CheckTrailhead(map, (i, j));

            Console.WriteLine(Result);
        }
    }
}
