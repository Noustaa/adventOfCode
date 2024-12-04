namespace _2024.Day4
{
    public static class Day4
    {
        public enum Direction { N, NE, E, SE, S, SW, W, NW };

        public static bool SearchPattern(char[][] matrix, (int, int) currentPos, char nextChar, Direction direction)
        {
            (int nextPosX, int nextPosY) = (0, 0);
            switch (direction)
            {
                case Direction.N:
                    (nextPosX, nextPosY) = (-1, 0);
                    break;
                case Direction.NE:
                    (nextPosX, nextPosY) = (-1, 1);
                    break;
                case Direction.E:
                    (nextPosX, nextPosY) = (0, 1);
                    break;
                case Direction.SE:
                    (nextPosX, nextPosY) = (1, 1);
                    break;
                case Direction.S:
                    (nextPosX, nextPosY) = (1, 0);
                    break;
                case Direction.SW:
                    (nextPosX, nextPosY) = (1, -1);
                    break;
                case Direction.W:
                    (nextPosX, nextPosY) = (0, -1);
                    break;
                case Direction.NW:
                    (nextPosX, nextPosY) = (-1, -1);
                    break;
            }

            (nextPosX, nextPosY) = (currentPos.Item1 + nextPosX, currentPos.Item2 + nextPosY);

            try
            {
                if (matrix[nextPosX][nextPosY] != nextChar)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }


            return nextChar switch
            {
                'M' => SearchPattern(matrix, (nextPosX, nextPosY), 'A', direction),
                'A' => SearchPattern(matrix, (nextPosX, nextPosY), 'S', direction),
                'S' => true,
                _ => false,
            };
        }

        public static bool VerifyXAroundA(char[][] matrix, (int, int) currentPos, Direction direction)
        {
            try
            {
                return direction switch
                {
                    Direction.N => (matrix[currentPos.Item1 - 1][currentPos.Item2 - 1] == 'M' &&
                                                matrix[currentPos.Item1 - 1][currentPos.Item2 + 1] == 'M' &&
                                                matrix[currentPos.Item1 + 1][currentPos.Item2 - 1] == 'S') &&
                                                matrix[currentPos.Item1 + 1][currentPos.Item2 + 1] == 'S',
                    Direction.E => (matrix[currentPos.Item1 - 1][currentPos.Item2 + 1] == 'M' &&
                                                matrix[currentPos.Item1 + 1][currentPos.Item2 + 1] == 'M' &&
                                                matrix[currentPos.Item1 - 1][currentPos.Item2 - 1] == 'S') &&
                                                matrix[currentPos.Item1 + 1][currentPos.Item2 - 1] == 'S',
                    Direction.S => (matrix[currentPos.Item1 + 1][currentPos.Item2 - 1] == 'M' &&
                                                matrix[currentPos.Item1 + 1][currentPos.Item2 + 1] == 'M' &&
                                                matrix[currentPos.Item1 - 1][currentPos.Item2 - 1] == 'S') &&
                                                matrix[currentPos.Item1 - 1][currentPos.Item2 + 1] == 'S',
                    Direction.W => (matrix[currentPos.Item1 - 1][currentPos.Item2 - 1] == 'M' &&
                                                matrix[currentPos.Item1 + 1][currentPos.Item2 - 1] == 'M' &&
                                                matrix[currentPos.Item1 - 1][currentPos.Item2 + 1] == 'S') &&
                                                matrix[currentPos.Item1 + 1][currentPos.Item2 + 1] == 'S',
                    _ => false,
                };
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static void SolvePart1()
        {
            string fileContent = File.ReadAllText("input");
            char[][] matrix = fileContent.Split("\n").Select(x => x.ToCharArray()).ToArray();

            int result = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 'X')
                        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                            if (SearchPattern(matrix, (i, j), 'M', dir))
                                result++;
                }
            }

            Console.WriteLine(result);
        }

        public static void SolvePart2()
        {
            string fileContent = File.ReadAllText("input");
            char[][] matrix = fileContent.Split("\n").Select(x => x.ToCharArray()).ToArray();

            int result = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 'A')
                        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                            if (VerifyXAroundA(matrix, (i, j), dir))
                            {
                                result++;
                                break;
                            }
                }
            }

            Console.WriteLine(result);
        }
    }
}
