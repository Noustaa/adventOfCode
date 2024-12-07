namespace _2024.Day7
{
    internal class Day7
    {
        static IEnumerable<string> CombinationsWithRepetition(IEnumerable<int> input, int length)
        {
            if (length <= 0)
                yield return "";
            else
            {
                foreach (var i in input)
                    foreach (var c in CombinationsWithRepetition(input, length - 1))
                        yield return i.ToString() + c;
            }
        }

        public static void SolvePart1()
        {
            string fileContent = File.ReadAllText("input");
            List<string> equations = fileContent.Split(Environment.NewLine).ToList();

            Int64 result = 0;
            foreach (string equation in equations)
            {
                string[] parts = equation.Split(":");

                Int64 resultToFind = Int64.Parse(parts[0]);
                List<Int64> numbers = parts[1].Split(" ").Skip(1).Select(x => Int64.Parse(x)).ToList();

                List<string> combinations = CombinationsWithRepetition([0, 1], numbers.Count - 1).ToList();

                for (int i = 0; i < combinations.Count; i++)
                {
                    Int64 total = numbers[0];

                    string combination = combinations[i];
                    for (int j = 0; j < combination.Length; j++)
                    {
                        switch (combination[j])
                        {
                            case '0':
                                total += numbers[j + 1];
                                break;
                            case '1':
                                total *= numbers[j + 1];
                                break;
                            default:
                                break;
                        }
                    }

                    if (total == resultToFind)
                    {
                        result += resultToFind;
                        break;
                    }
                }
            }

            Console.WriteLine(result);
        }

        public static void SolvePart2()
        {
            string fileContent = File.ReadAllText("input");
            List<string> equations = fileContent.Split(Environment.NewLine).ToList();

            Int64 result = 0;
            foreach (string equation in equations)
            {
                string[] parts = equation.Split(":");

                Int64 resultToFind = Int64.Parse(parts[0]);
                List<Int64> numbers = parts[1].Split(" ").Skip(1).Select(x => Int64.Parse(x)).ToList();

                List<string> combinations = CombinationsWithRepetition([0, 1, 2], numbers.Count - 1).ToList();

                for (int i = 0; i < combinations.Count; i++)
                {
                    Int64 total = numbers[0];

                    string combination = combinations[i];
                    for (int j = 0; j < combination.Length; j++)
                    {
                        switch (combination[j])
                        {
                            case '0':
                                total += numbers[j + 1];
                                break;
                            case '1':
                                total *= numbers[j + 1];
                                break;
                            case '2':
                                total = Int64.Parse(total.ToString() + numbers[j + 1].ToString());
                                break;
                            default:
                                break;
                        }
                    }

                    if (total == resultToFind)
                    {
                        result += resultToFind;
                        break;
                    }
                }
            }

            Console.WriteLine(result);
        }
    }
}