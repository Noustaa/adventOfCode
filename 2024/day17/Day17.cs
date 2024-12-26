using System.Text.RegularExpressions;

namespace _2024.Day17
{
    internal class Day17
    {
        long RegA, RegB, RegC;
        int RunningPointer = 0;
        List<long> Instructions = [];
        List<long> Output = [];

        public long GetOperand()
        {
            return Instructions[RunningPointer + 1] switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => RegA,
                5 => RegB,
                6 => RegC,
                _ => throw new Exception(),
            };
        }

        public void Divide(char register)
        {
            long divisor = GetOperand();
            switch (register)
            {
                case 'A':
                    RegA = (long)(RegA / Math.Pow(2, divisor));
                    break;
                case 'B':
                    RegB = (long)(RegA / Math.Pow(2, divisor));
                    break;
                case 'C':
                    RegC = (long)(RegA / Math.Pow(2, divisor));
                    break;
            }
        }

        public void WriteOutput()
        {
            Output.Add(GetOperand() % 8);
        }

        public void BitwiseXor()
        {
            RegB ^= Instructions[RunningPointer + 1];
        }

        public void BitwiseXorBC()
        {
            RegB ^= RegC;
        }

        public void Bst()
        {
            RegB = GetOperand() & 0b111;
        }

        public bool Jump()
        {
            if (RegA != 0)
            {
                RunningPointer = (int)Instructions[RunningPointer + 1];
                return true;
            }
            else
                return false;
        }

        public void Solve()
        {
            string fileContent = File.ReadAllText("input");
            string[] lines = fileContent.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            Regex numbers = new Regex(@"\d+");
            RegA = int.Parse(numbers.Match(lines[0]).Value);
            RegB = int.Parse(numbers.Match(lines[1]).Value);
            RegC = int.Parse(numbers.Match(lines[2]).Value);

            foreach (Match match in numbers.Matches(lines.Last()))
                Instructions.Add(int.Parse(match.Value));

            for (; RunningPointer < Instructions.Count; RunningPointer += 2)
            {
                bool goNext = true;
                do
                {
                    goNext = true;
                    switch (Instructions[RunningPointer])
                    {
                        case 0:
                            Divide('A');
                            break;
                        case 1:
                            BitwiseXor();
                            break;
                        case 2:
                            Bst();
                            break;
                        case 3:
                            if (Jump())
                                goNext = false;
                            break;
                        case 4:
                            BitwiseXorBC();
                            break;
                        case 5:
                            WriteOutput();
                            break;
                        case 6:
                            Divide('B');
                            break;
                        case 7:
                            Divide('C');
                            break;
                        default:
                            break;
                    }
                } while (!goNext);
            }
            Console.WriteLine(string.Join(',', Output));
        }
    }
}
