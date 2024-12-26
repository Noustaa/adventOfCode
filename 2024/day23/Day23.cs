namespace _2024.Day23
{
    class ListComparer<T> : IEqualityComparer<List<T>>
    {
        public bool Equals(List<T> x, List<T> y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (x == null || y == null) return false;

            return x.SequenceEqual(y);
        }

        public int GetHashCode(List<T> obj)
        {
            if (obj == null) return 0;

            int hash = 17;
            foreach (var item in obj)
            {
                hash = hash * 31 + (item == null ? 0 : item.GetHashCode());
            }
            return hash;
        }
    }

    internal class Day23
    {
        public void Solve(int part)
        {
            string fileContent = File.ReadAllText("input");
            string[] lines = fileContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, List<string>> connections = [];

            foreach (string line in lines)
            {
                string[] parts = line.Split('-');

                if (connections.ContainsKey(parts[0]))
                    connections[parts[0]].Add(parts[1]);
                else
                    connections[parts[0]] = [parts[1]];

                if (connections.ContainsKey(parts[1]))
                    connections[parts[1]].Add(parts[0]);
                else
                    connections[parts[1]] = [parts[0]];
            }

            if (part == 1)
            {
                HashSet<List<string>> triplets = new(new ListComparer<string>());

                foreach (var connection in connections)
                {
                    string firstNode = connection.Key;

                    foreach (string secondNode in connection.Value)
                    {
                        foreach (string thirdNode in connections[secondNode])
                        {
                            if (thirdNode != firstNode)
                            {
                                foreach (string finalNode in connections[thirdNode])
                                {
                                    if (finalNode == firstNode)
                                    {
                                        List<string> triplet = [firstNode, secondNode, thirdNode];
                                        triplet.Sort();
                                        triplets.Add(triplet);
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine(triplets.Where(triplet => triplet.Any(computerName => computerName.StartsWith('t'))).Count());
            }
            else
            {
                List<HashSet<string>> bigLan = [];

                foreach (var node in connections)
                {
                    HashSet<string> computers = [node.Key];

                    foreach (string computer in node.Value)
                    {
                        var computerNode = connections[computer];

                        foreach (string linkComputer in computerNode)
                        {
                            if (computers.Contains(linkComputer))
                                computers.Add(computer);
                        }
                    }

                    foreach (string computer1 in computers)
                    {
                        bool allHaveConnectionWithMe = true;

                        foreach (string computer2 in computers)
                        {
                            if (computer1 == computer2)
                                continue;

                            if (!connections[computer2].Contains(computer1))
                            {
                                allHaveConnectionWithMe = false;
                                break;
                            }
                        }

                        if (!allHaveConnectionWithMe)
                            computers.Remove(computer1);
                    }

                    bigLan.Add(computers);
                }


                List<string> biggestLan = bigLan
                    .Where(lan => lan.Count == bigLan.Max(lan => lan.Count))
                    .First()
                    .ToList();
                biggestLan.Sort();
                Console.WriteLine(string.Join(",", biggestLan));
            }
        }
    }
}
