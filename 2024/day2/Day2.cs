namespace _2024.Day2
{
    public static class Day2
    {
        private static bool IsIncreasing(int current, int next) => current <= next;
        private static bool IsInRange(int current, int next) =>
            Math.Abs(current - next) >= 1 && Math.Abs(current - next) <= 3;
        private static bool IsSafeLevelStep(int current, int next, bool isIncreasing) =>
            (IsIncreasing(current, next) == isIncreasing) && IsInRange(current, next);
        private static bool IsSafeReport(List<int> report)
        {
            bool isIncreasing = IsIncreasing(report[0], report[1]);
            for (int i = 0; i < report.Count - 1; i++)
            {
                int current = report[i];
                int next = report[i + 1];

                if (!IsSafeLevelStep(current, next, isIncreasing))
                    return false;
            }
            return true;
        }

        private static bool IsSafeReportTolerateOneError(List<int> report)
        {
            int errorCount = 0;
            bool isIncreasing = IsIncreasing(report[0], report[1]);
            for (int i = 0; i < report.Count - 1; i++)
            {
                int current = report[i];
                int next = report[i + 1];

                if (!IsSafeLevelStep(current, next, isIncreasing))
                    errorCount++;

                if (errorCount > 1)
                    return false;
            }
            return true;
        }


        public static void SolvePart1()
        {
            string fileContent = File.ReadAllText("input");
            string[] lines = fileContent.Split("\n");

            int safeCount = 0;

            foreach (string line in lines)
            {
                if (IsSafeReport(line.Split(" ").Select(int.Parse).ToList()))
                {
                    safeCount++;
                }
            }

            Console.WriteLine(safeCount);
        }

        public static void SolvePart2()
        {
            string fileContent = File.ReadAllText("input");
            string[] lines = fileContent.Split("\n");

            int safeCount = 0;

            foreach (string line in lines)
            {
                if (IsSafeReportTolerateOneError(line.Split(" ").Select(int.Parse).ToList()))
                {
                    safeCount++;
                }
            }

            Console.WriteLine(safeCount);
        }
    }
}
