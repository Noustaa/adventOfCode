namespace _2024.Day19
{
    internal class Day19
    {
        public void Solve()
        {
            string input = File.ReadAllText("input");
            List<string> lines = input.Split(Environment.NewLine).ToList();
            List<string> availableTowels = lines.First().Split(", ").ToList();

            List<string> designsToDo = lines.Skip(2).ToList();

            int possibleDesigns = 0;

            List<string> permutations = new List<string>();
            List<int> lengthsDone = new List<int>();
            foreach (string design in designsToDo)
            {
                int done = 0;
                bool found = false;

                for (int i = design.Length; i > 0; i--)
                {
                    done = 0;
                    for (int offset = i; offset > 0; offset--)
                    {
                        if (offset < done)
                            break;

                        string designToCheck = design[done..offset];
                        string remainingDesign = design[offset..];

                        if (availableTowels.Contains(designToCheck))
                        {
                            if (offset == design.Length)
                            {
                                possibleDesigns++;
                                found = true;
                                break;
                            }
                            else
                            {
                                done = offset;
                                offset = design.Length + 1;
                            }
                        }
                    }
                    if (found)
                        break;
                }

                if (found)
                    continue;

                for (int i = 1; i < design.Length; i++)
                {
                    done = 0;
                    for (int offset = i; offset <= design.Length; offset++)
                    {
                        if (done > offset)
                            break;

                        string designToCheck = design[done..offset];
                        string remainingDesign = design[offset..];

                        if (availableTowels.Contains(designToCheck))
                        {
                            if (offset == design.Length)
                            {
                                possibleDesigns++;
                                found = true;
                                break;
                            }
                            else
                            {
                                done = offset;
                                if (remainingDesign.Length == 1)
                                    offset = design.Length - 1;
                                else
                                    offset = design.Length - remainingDesign.Length;
                            }
                        }
                    }

                    if (found)
                        break;
                }
            }

            Console.WriteLine(possibleDesigns);
        }
    }
}
