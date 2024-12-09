namespace _2024.Day9
{
    internal class Day9
    {
        public static void SolvePart1()
        {
            string diskMap = File.ReadAllText("input");

            List<string> disk = [];
            string space = ".";
            for (int i = 0; i < diskMap.Length; i++)
            {
                if (i % 2 == 0)
                {
                    int id = i >> 1;
                    int factor = int.Parse(diskMap[i].ToString());
                    for (int j = 0; j < factor; j++)
                        disk.Add(id.ToString());
                }
                else
                {
                    int factor = int.Parse(diskMap[i].ToString());
                    for (int j = 0; j < factor; j++)
                        disk.Add(space);
                }
            }

            int replaceIndex = disk.IndexOf(space);
            string strToMove = disk.Last(str => str != space);
            int indexToMove = disk.LastIndexOf(strToMove);
            while (replaceIndex < indexToMove)
            {
                disk[replaceIndex] = strToMove;
                disk[indexToMove] = space;
                replaceIndex = disk.IndexOf(space);
                strToMove = disk.Last(str => str != space);
                indexToMove = disk.LastIndexOf(strToMove);
            }

            long result = 0;
            for (int i = 0; i < disk.Count; i++)
            {
                if (disk[i] == space)
                    continue;

                result += i * int.Parse(disk[i]);
            }


            Console.WriteLine(result);
        }

        public static void SolvePart2()
        {
            string diskMap = File.ReadAllText("input");

            List<string> disk = [];
            string space = ".";
            for (int i = 0; i < diskMap.Length; i++)
            {
                if (i % 2 == 0)
                {
                    int id = i >> 1;
                    int factor = int.Parse(diskMap[i].ToString());
                    for (int j = 0; j < factor; j++)
                        disk.Add(id.ToString());
                }
                else
                {
                    int factor = int.Parse(diskMap[i].ToString());
                    for (int j = 0; j < factor; j++)
                        disk.Add(space);
                }
            }

            List<string> fileToSkip = [];
            int replaceIndex = disk.IndexOf(space);
            string strToMove = disk.Last(str => str != space);
            int lastIndexToMove = disk.LastIndexOf(strToMove);
            int firstIndexToMove = disk.IndexOf(strToMove);
            while (replaceIndex < firstIndexToMove)
            {
                bool fitFound = false;
                int fileLength = lastIndexToMove - firstIndexToMove + 1;
                for (int i = replaceIndex; i < firstIndexToMove; i++)
                {
                    if (string.Concat(disk[i..(i + fileLength)])
                        == string.Concat(Enumerable.Repeat(space, fileLength)))
                    {
                        for (int j = 0; j < fileLength; j++)
                        {
                            disk[i + j] = disk[firstIndexToMove + j];
                            disk[firstIndexToMove + j] = space;
                            fitFound = true;
                        }
                        break;
                    }
                }

                if (!fitFound)
                    fileToSkip.Add(strToMove);

                replaceIndex = disk.IndexOf(space);
                strToMove = disk.Last(str => str != space && !fileToSkip.Contains(str));
                lastIndexToMove = disk.LastIndexOf(strToMove);
                firstIndexToMove = disk.IndexOf(strToMove);
            }

            long result = 0;
            for (int i = 0; i < disk.Count; i++)
            {
                if (disk[i] == space)
                    continue;

                result += i * int.Parse(disk[i]);
            }


            Console.WriteLine(result);
        }
    }
}