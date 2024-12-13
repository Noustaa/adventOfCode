namespace _2024.Day12
{
    internal class Day12
    {
        public static List<(int, int)> DFS(char[][] map, int y, int x, char targetChar, bool[][] visited)
        {
            if (y < 0 || y >= map.Length || x < 0 || x >= map[y].Length ||
                map[y][x] != targetChar || visited[y][x])
            {
                return [];
            }

            visited[y][x] = true;

            List<(int, int)> region = [(y, x)];
            region.AddRange(DFS(map, y - 1, x, targetChar, visited));
            region.AddRange(DFS(map, y + 1, x, targetChar, visited));
            region.AddRange(DFS(map, y, x - 1, targetChar, visited));
            region.AddRange(DFS(map, y, x + 1, targetChar, visited));

            return region;
        }

        public void Solve(int part)
        {
            string fileContent = File.ReadAllText("input");
            char[][] map = fileContent.Split(Environment.NewLine).Select(line => line.ToCharArray()).ToArray();

            char[][] extendedMap = new char[map.Length + 2][];

            for (int i = 0; i < extendedMap.Length; i++)
                extendedMap[i] = new char[map[0].Length + 2];

            for (int i = 0; i < map.Length; i++)
                for (int j = 0; j < map[i].Length; j++)
                    extendedMap[i + 1][j + 1] = map[i][j];

            map = extendedMap;


            bool[][] visited = new bool[map.Length][];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = new bool[map[i].Length];

            List<List<(int, int)>> allRegions = [];

            for (int i = 0; i < map.Length; i++)
                for (int j = 0; j < map[i].Length; j++)
                    if (!visited[i][j] && map[i][j] != '\0')
                    {
                        List<(int, int)> region = DFS(map, i, j, map[i][j], visited);
                        if (region.Count > 0)
                            allRegions.Add(region);
                    }

            int result = 0;
            for (int i = 0; i < allRegions.Count; i++)
            {
                List<(int, int)> perimeter = [];
                int cornerCount = 0;
                for (var j = 0; j < allRegions[i].Count; j++)
                {
                    (int y, int x) = allRegions[i][j];
                    char currentChar = map[y][x];

                    if (y - 1 < 0)
                        perimeter.Add((y - 1, x));
                    if (y + 1 >= map.Length)
                        perimeter.Add((y + 1, x));
                    if (x - 1 < 0)
                        perimeter.Add((y, x - 1));
                    if (x + 1 >= map[y].Length)
                        perimeter.Add((y, x + 1));

                    if (y - 1 >= 0 && map[y - 1][x] != currentChar)
                        perimeter.Add((y - 1, x));
                    if (y + 1 < map.Length && map[y + 1][x] != currentChar)
                        perimeter.Add((y + 1, x));
                    if (x - 1 >= 0 && map[y][x - 1] != currentChar)
                        perimeter.Add((y, x - 1));
                    if (x + 1 < map[y].Length && map[y][x + 1] != currentChar)
                        perimeter.Add((y, x + 1));

                    if (map[y - 1][x] != currentChar && map[y][x - 1] != currentChar)
                        cornerCount++;
                    if (map[y - 1][x] != currentChar && map[y][x + 1] != currentChar)
                        cornerCount++;
                    if (map[y + 1][x] != currentChar && map[y][x - 1] != currentChar)
                        cornerCount++;
                    if (map[y + 1][x] != currentChar && map[y][x + 1] != currentChar)
                        cornerCount++;

                    if (map[y - 1][x] == currentChar && map[y][x + 1] == currentChar && map[y - 1][x + 1] != currentChar)
                        cornerCount++;
                    if (map[y - 1][x] == currentChar && map[y][x - 1] == currentChar && map[y - 1][x - 1] != currentChar)
                        cornerCount++;
                    if (map[y + 1][x] == currentChar && map[y][x + 1] == currentChar && map[y + 1][x + 1] != currentChar)
                        cornerCount++;
                    if (map[y + 1][x] == currentChar && map[y][x - 1] == currentChar && map[y + 1][x - 1] != currentChar)
                        cornerCount++;
                }
                if (part == 1)
                    result += perimeter.Count * allRegions[i].Count;
                else if (part == 2)
                    result += cornerCount * allRegions[i].Count;
            }

            Console.WriteLine(result);
        }
    }
}
