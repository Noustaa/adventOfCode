namespace _2024.Day1
{
    public static class Day1
    {
        public static void SolvePart1()
        {
            string fileContent = File.ReadAllText("input");
            string[] lines = fileContent.Split("\n");

            List<int> leftCol = lines.Select(x => int.Parse(x.Split("   ")[0])).ToList();
            List<int> rightCol = lines.Select(x => int.Parse(x.Split("   ")[1])).ToList();

            leftCol.Sort();
            rightCol.Sort();

            int result = 0;

            for (int i = 0; i < leftCol.Count; i++)
            {
                int higherValue = (leftCol[i] > rightCol[i]) ? leftCol[i] : rightCol[i];
                int lowerValue = (leftCol[i] < rightCol[i]) ? leftCol[i] : rightCol[i];

                result += higherValue - lowerValue;
            }

            Console.WriteLine(result);
        }

        public static void SolvePart2()
        {
            string fileContent = File.ReadAllText("input");
            string[] lines = fileContent.Split("\n");

            List<int> leftCol = lines.Select(x => int.Parse(x.Split("   ")[0])).ToList();
            List<int> rightCol = lines.Select(x => int.Parse(x.Split("   ")[1])).ToList();

            int result = 0;

            for (int i = 0; i < leftCol.Count; i++)
            {
                int leftColValue = leftCol[i];
                int leftColValueCountInRightCol = rightCol.Count(x => x == leftColValue);

                result += leftColValue * leftColValueCountInRightCol;
            }

            Console.WriteLine(result);
        }
    }
}