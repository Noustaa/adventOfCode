namespace _2024.Day24
{
    internal class Day24
    {
        public void Solve()
        {
            string fileContent = File.ReadAllText("input");
            string[] parts = fileContent.Split(Environment.NewLine + Environment.NewLine);

            Dictionary<string, bool> outputs = [];

            foreach (string wire in parts.First().Split(Environment.NewLine))
            {
                string[] split = wire.Split(':');
                bool value = int.Parse(split.Last()) != 0;
                string wireName = split.First();

                outputs.Add(wireName, value);
            }

            List<string> operations = parts.Last().Split(Environment.NewLine).ToList();

            while (operations.Count > 0)
            {
                for (int i = 0; i < operations.Count; i++)
                {
                    string[] split = operations[i].Split(' ');

                    string firstOperand = split[0];
                    string secondOperand = split[2];
                    string operation = split[1];
                    string output = split.Last();

                    if (!outputs.ContainsKey(firstOperand) || !outputs.ContainsKey(secondOperand))
                        continue;


                    switch (operation)
                    {
                        case "AND":
                            outputs.Add(output, outputs[firstOperand] & outputs[secondOperand]);
                            break;
                        case "OR":
                            outputs.Add(output, outputs[firstOperand] | outputs[secondOperand]);
                            break;
                        case "XOR":
                            outputs.Add(output, outputs[firstOperand] ^ outputs[secondOperand]);
                            break;
                        default:
                            break;
                    }

                    operations.RemoveAt(i--);
                }
            }

            string result = "";

            int keyIndex = 0;
            while (true)
            {
                if (!outputs.TryGetValue("z" + keyIndex.ToString("00"), out bool value))
                    break;

                result += value ? "1" : "0";
                keyIndex++;
            }

            string binary = string.Concat(result.Reverse());
            Console.WriteLine(Convert.ToInt64(binary, 2));
        }
    }
}
