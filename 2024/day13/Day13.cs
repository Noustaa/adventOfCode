using System.Text.RegularExpressions;

namespace _2024.Day13
{
    internal class Day13
    {
        public void Solve(int part)
        {
            string fileContent = File.ReadAllText("input");
            List<string> lines = [.. fileContent.Split(Environment.NewLine)];

            double ax = 0;
            double ay = 0;
            double bx = 0;
            double by = 0;

            double prize1 = 0;
            double prize2 = 0;

            double minimumOfTokenToSpend = 0;

            Regex findNumbers = new(@"\d+\d");
            foreach (var line in lines)
            {
                var numbers = findNumbers.Matches(line);
                if (line.StartsWith("Button A"))
                {
                    ax = double.Parse(numbers[0].Value);
                    ay = double.Parse(numbers[1].Value);
                }
                else if (line.StartsWith("Button B"))
                {
                    bx = double.Parse(numbers[0].Value);
                    by = double.Parse(numbers[1].Value);
                }
                else if (line.StartsWith("Prize"))
                {
                    if (part == 1)
                    {
                        prize1 = double.Parse(numbers[0].Value);
                        prize2 = double.Parse(numbers[1].Value);
                    }
                    else if (part == 2)
                    {
                        prize1 = double.Parse(numbers[0].Value) + 10000000000000;
                        prize2 = double.Parse(numbers[1].Value) + 10000000000000;
                    }
                }
                else
                {
                    double aButtonPressCount = (prize1 * by - prize2 * bx) / (ax * by - ay * bx);
                    double bButtonPressCount = (ax * prize2 - ay * prize1) / (ax * by - ay * bx);

                    if (aButtonPressCount % 1 == 0 && bButtonPressCount % 1 == 0)
                        minimumOfTokenToSpend += (aButtonPressCount * 3) + bButtonPressCount;
                }
            }
            Console.WriteLine(minimumOfTokenToSpend);
        }
    }
}
