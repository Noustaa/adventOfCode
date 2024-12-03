using System.Text.RegularExpressions;

namespace _2024.Day3
{
    public static class Day3
    {
        public static void SolvePart1()
        {
            string fileContent = File.ReadAllText("input");
            string[] lines = fileContent.Split("\n");

            string operationRegex = @"mul\(\d{1,3},\d{1,3}\)";
            string numbersRegex = @"(\d{1,3}),(\d{1,3})";

            int result = 0;

            foreach (string line in lines)
            {

                var regs = Regex.Matches(
                    line,
                    operationRegex);

                foreach (var match in regs)
                {
                    var numbers = Regex.Match(
                        match.ToString()!,
                        numbersRegex);

                    int first = int.Parse(numbers.Groups[1].Value);
                    int second = int.Parse(numbers.Groups[2].Value);

                    result += first * second;
                }
            }

            Console.WriteLine(result);
        }

        public static void SolvePart2()
        {
            string fileContent = File.ReadAllText("input");
            string[] lines = fileContent.Split("\n");

            string operationRegex = @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)";
            string numbersRegex = @"(\d{1,3}),(\d{1,3})";

            int result = 0;
            bool isMulEnabled = true;

            foreach (string line in lines)
            {

                var regs = Regex.Matches(
                    line,
                    operationRegex);

                foreach (var match in regs)
                {
                    if (match.ToString() == "do()")
                    {
                        isMulEnabled = true;
                        continue;
                    }

                    if (match.ToString() == "don't()")
                    {
                        isMulEnabled = false;
                        continue;
                    }

                    if (!isMulEnabled)
                        continue;

                    var numbers = Regex.Match(
                        match.ToString()!,
                        numbersRegex);

                    int first = int.Parse(numbers.Groups[1].Value);
                    int second = int.Parse(numbers.Groups[2].Value);

                    result += first * second;
                }
            }

            Console.WriteLine(result);
        }
    }
}
