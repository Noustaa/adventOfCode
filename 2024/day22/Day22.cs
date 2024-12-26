namespace _2024.Day22
{
    internal class Day22
    {

        public long Mix(long secret, long mixValue) => secret ^ mixValue;
        public long Prune(long secret) => secret % 16777216;

        public void SolvePart1()
        {
            string input = File.ReadAllText("input");
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            List<long> finalSecrets = new List<long>();
            foreach (var line in lines)
            {
                long secret = long.Parse(line);

                for (int i = 0; i < 2000; i++)
                {
                    long buffer = 0;

                    buffer = secret * 64;
                    secret = Mix(secret, buffer);
                    secret = Prune(secret);

                    buffer = (long)(secret / 32);
                    secret = Mix(secret, buffer);
                    secret = Prune(secret);

                    buffer = secret * 2048;
                    secret = Mix(secret, buffer);
                    secret = Prune(secret);
                }

                finalSecrets.Add(secret);
            }

            long result = 0;
            finalSecrets.ForEach(num => result += num);
            Console.WriteLine(result);
        }


        static void GeneratePermutations(int[] sequence, int position, List<int[]> results)
        {
            if (position == sequence.Length)
            {
                results.Add((int[])sequence.Clone());
                return;
            }

            for (int i = -9; i <= 9; i++)
            {
                if (position == 0 || (Math.Abs(i - sequence[position - 1]) >= 0 && Math.Abs(i - sequence[position - 1]) <= 9))
                {
                    sequence[position] = i;
                    GeneratePermutations(sequence, position + 1, results);
                }
            }
        }

        public void SolvePart2()
        {
            string input = File.ReadAllText("input");
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            List<long> finalSecrets = new List<long>();

            List<int[]> results = new List<int[]>();
            GeneratePermutations(new int[4], 0, results);

            List<(long, int[])> result = [];
            int index = 0;
            foreach (var sequence in results)
            {
                index++;
                long bananas = 0;
                foreach (var line in lines)
                {
                    long secret = long.Parse(line);
                    List<long> secrets = new List<long>();
                    LinkedList<long> sequenceList = new LinkedList<long>();

                    secrets.Add(secret);

                    for (int i = 0; i < 2000; i++)
                    {
                        long buffer = 0;

                        buffer = secret * 64;
                        secret = Mix(secret, buffer);
                        secret = Prune(secret);

                        buffer = (long)(secret / 32);
                        secret = Mix(secret, buffer);
                        secret = Prune(secret);

                        buffer = secret * 2048;
                        secret = Mix(secret, buffer);
                        secret = Prune(secret);

                        if (sequenceList.Count == 4)
                            sequenceList.RemoveFirst();

                        long secretLastDigit = secret % 10;

                        sequenceList.AddLast(secretLastDigit - (secrets.Last() % 10));
                        secrets.Add(secret);

                        if (sequenceList.Count == 4)
                        {
                            bool isSequenceEqual = true;

                            for (int j = 0; j < sequenceList.Count; j++)
                            {
                                if (sequenceList.ElementAt(j) != sequence[j])
                                {
                                    isSequenceEqual = false;
                                    break;
                                }
                            }
                            if (isSequenceEqual)
                            {
                                bananas += secrets.Last() % 10;
                                break;
                            }
                        }


                    }
                }

                result.Add((bananas, sequence));
            }

            result = result.OrderBy(x => x.Item1).ToList();

            foreach (var (res, seq) in result)
            {
                Console.WriteLine(res + " =>" + string.Join(",", seq));
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(result);
        }
    }
}
