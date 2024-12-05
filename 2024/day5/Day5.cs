namespace _2024.Day5
{
    public static class Day5
    {
        public static void SolvePart1()
        {
            string fileContent = File.ReadAllText("input");
            string[] sections = fileContent.Split(Environment.NewLine + Environment.NewLine);
            List<string> restrictions = sections[0].Split("\n").ToList();
            List<string> updates = sections[1].Split("\n").ToList();

            Dictionary<int, List<int>> digitOrder = new Dictionary<int, List<int>>();
            List<List<int>> validUpdates = new List<List<int>>();

            foreach (string line in restrictions)
            {
                int[] parts = line.Split("|").Select(int.Parse).ToArray();

                int digit = parts[1];
                int mustBeBeforeDigit = parts[0];

                if (!digitOrder.ContainsKey(digit))
                    digitOrder[digit] = new List<int>();

                digitOrder[digit].Add(mustBeBeforeDigit);
            }

            foreach (string line in updates)
            {
                List<int> parts = line.Split(",").Select(int.Parse).ToList();
                bool isValid = true;

                for (int i = 0; i < parts.Count; i++)
                {
                    int digit = parts[i];

                    int j = 0;
                    while (++j < parts.Count - i)
                    {
                        List<int>? order = digitOrder.GetValueOrDefault(digit);

                        if (order == null)
                            continue;

                        if (order.Contains(parts[i + j]))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

                if (isValid)
                {
                    validUpdates.Add(parts.ToList());
                    continue;
                }
            }

            int result = 0;

            foreach (List<int> update in validUpdates)
            {
                result += update[update.Count / 2];
            }

            Console.WriteLine(result);
        }

        public static void SolvePart2()
        {
            string fileContent = File.ReadAllText("test");
            string[] sections = fileContent.Split(Environment.NewLine + Environment.NewLine);
            List<string> restrictions = sections[0].Split("\n").ToList();
            List<string> updates = sections[1].Split("\n").ToList();

            Dictionary<int, List<int>> digitOrder = new Dictionary<int, List<int>>();
            List<List<int>> invalidUpdates = new List<List<int>>();

            foreach (string line in restrictions)
            {
                int[] parts = line.Split("|").Select(int.Parse).ToArray();

                int digit = parts[1];
                int mustBeBeforeDigit = parts[0];

                if (!digitOrder.ContainsKey(digit))
                    digitOrder[digit] = new List<int>();

                digitOrder[digit].Add(mustBeBeforeDigit);
            }

            foreach (string line in updates)
            {
                List<int> parts = line.Split(",").Select(int.Parse).ToList();
                bool isValid = true;

                for (int i = 0; i < parts.Count; i++)
                {
                    int digit = parts[i];

                    int j = 0;
                    while (++j < parts.Count - i)
                    {
                        List<int>? order = digitOrder.GetValueOrDefault(digit);

                        if (order == null)
                            continue;

                        if (order.Contains(parts[i + j]))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

                if (!isValid)
                {
                    invalidUpdates.Add(parts.ToList());
                    continue;
                }
            }

            for (int i = 0; i < invalidUpdates.Count; i++)
            {
                for (int j = 0; j < invalidUpdates[i].Count; j++)
                {
                    int digit = invalidUpdates[i][j];

                    int k = 0;
                    while (++k < invalidUpdates[i].Count - j)
                    {
                        List<int>? order = digitOrder.GetValueOrDefault(digit);

                        if (order == null)
                            continue;

                        if (order.Contains(invalidUpdates[i][j + k]))
                        {
                            int moveDigit = invalidUpdates[i][j + k];
                            invalidUpdates[i].Remove(invalidUpdates[i][j + k]);
                            invalidUpdates[i].Insert(j, moveDigit);
                            j = -1;
                            break;
                        }
                    }
                }
            }

            int result = 0;

            foreach (List<int> update in invalidUpdates)
            {
                result += update[update.Count / 2];
            }

            Console.WriteLine(result);
        }
    }
}
