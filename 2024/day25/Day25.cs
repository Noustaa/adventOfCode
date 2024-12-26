namespace _2024.Day25
{
    internal class Day25
    {
        public void Solve()
        {
            string input = File.ReadAllText("input");
            List<string> grids = input.Split(Environment.NewLine + Environment.NewLine).ToList();

            List<string[]> keys = [];
            List<string[]> locks = [];

            foreach (var grid in grids)
            {
                if (grid.First() == '.')
                    keys.Add(grid.Split(Environment.NewLine).ToArray());
                else
                    locks.Add(grid.Split(Environment.NewLine).ToArray());
            }

            List<List<int>> keysFingerprints = [];
            List<List<int>> locksFingerprints = [];

            foreach (var key in keys)
            {
                List<int> fingerprint = [];
                for (int i = 0; i < key.First().Length; i++)
                {
                    int count = 0;
                    for (int j = 0; j < key.Length; j++)
                    {
                        if (key[j][i] == '#')
                            count++;
                    }
                    fingerprint.Add(count - 1);
                }
                keysFingerprints.Add(fingerprint);
            }

            foreach (var _lock in locks)
            {
                List<int> fingerprint = [];
                for (int i = 0; i < _lock.First().Length; i++)
                {
                    int count = 0;
                    for (int j = 0; j < _lock.Length; j++)
                    {
                        if (_lock[j][i] == '#')
                            count++;
                    }
                    fingerprint.Add(count - 1);
                }
                locksFingerprints.Add(fingerprint);
            }

            int matchingPairsCount = 0;
            foreach (var key in keysFingerprints)
            {
                foreach (var _lock in locksFingerprints)
                {
                    bool match = true;
                    for (int i = 0; i < key.Count; i++)
                    {
                        if (key[i] + _lock[i] > 5)
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                        matchingPairsCount++;
                }
            }

            Console.WriteLine(matchingPairsCount);
        }
    }
}
