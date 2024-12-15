using Position = (int x, int y);

namespace _2024.Day15
{
    enum Direction { Up, Down, Left, Right }
    internal class Day15
    {
        delegate void MoveDelegate(ref Position robot, ref char[][] map, Direction direction);

        public static void MovePart1(ref Position robot, ref char[][] map, Direction direction)
        {
            Position target = direction switch
            {
                Direction.Up => (robot.x, robot.y - 1),
                Direction.Down => (robot.x, robot.y + 1),
                Direction.Left => (robot.x - 1, robot.y),
                Direction.Right => (robot.x + 1, robot.y),
                _ => (robot.x, robot.y)
            };

            if (map[target.y][target.x] == '.')
            {
                map[robot.y][robot.x] = '.';
                robot = target;
                map[robot.y][robot.x] = '@';
            }
            else if (map[target.y][target.x] == 'O')
            {
                bool foundSpace = false;
                bool foundWall = false;
                int index = 1;
                int boxesCount = 1;
                do
                {
                    char nextChar = '\0';
                    switch (direction)
                    {
                        case Direction.Up:
                            nextChar = map[target.y - index][target.x];
                            break;
                        case Direction.Down:
                            nextChar = map[target.y + index][target.x];
                            break;
                        case Direction.Left:
                            nextChar = map[target.y][target.x - index];
                            break;
                        case Direction.Right:
                            nextChar = map[target.y][target.x + index];
                            break;
                    }
                    if (nextChar == '.')
                        foundSpace = true;
                    else if (nextChar == 'O')
                        boxesCount++;
                    else if (nextChar == '#')
                        foundWall = true;

                    index++;
                } while (!foundSpace && !foundWall);

                if (foundSpace)
                {
                    map[robot.y][robot.x] = '.';
                    robot = target;
                    map[robot.y][robot.x] = '@';
                    for (int i = 1; i < boxesCount + 1; i++)
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                                map[robot.y - i][robot.x] = 'O';
                                break;
                            case Direction.Down:
                                map[robot.y + i][robot.x] = 'O';
                                break;
                            case Direction.Left:
                                map[robot.y][robot.x - i] = 'O';
                                break;
                            case Direction.Right:
                                map[robot.y][robot.x + i] = 'O';
                                break;
                        }
                    }
                }
            }
        }

        public static void MovePart2(ref Position robot, ref char[][] map, Direction direction)
        {
            Position target = direction switch
            {
                Direction.Up => (robot.x, robot.y - 1),
                Direction.Down => (robot.x, robot.y + 1),
                Direction.Left => (robot.x - 1, robot.y),
                Direction.Right => (robot.x + 1, robot.y),
                _ => (robot.x, robot.y)
            };

            if (map[target.y][target.x] == '.')
            {
                map[robot.y][robot.x] = '.';
                robot = target;
                map[robot.y][robot.x] = '@';
            }
            else if (map[target.y][target.x] == '[' || map[target.y][target.x] == ']')
            {
                bool foundSpace = false;
                bool foundWall = false;
                int index = 1;
                int boxesCount = 1;
                string nextPos = "";
                List<(Position left, Position right)> boxesToMove = map[target.y][target.x] == '['
                    ? new List<(Position left, Position right)> { ((target.x, target.y), (target.x + 1, target.y)) }
                    : new List<(Position left, Position right)> { ((target.x - 1, target.y), (target.x, target.y)) };

                do
                {
                    char nextChar = '\0';
                    List<(Position left, Position right)> newBoxesToMove = [];
                    List<(Position left, Position right)> currentBoxes = [];
                    if (direction == Direction.Up)
                        currentBoxes = boxesToMove.Where(box => box.left.y == target.y - index + 1).ToList();
                    else if (direction == Direction.Down)
                        currentBoxes = boxesToMove.Where(box => box.left.y == target.y + index - 1).ToList();
                    bool[] foundSpacePerbox = new bool[currentBoxes.Count()];
                    int k = 0;
                    switch (direction)
                    {
                        case Direction.Up:
                            foreach (var box in currentBoxes)
                            {
                                nextPos = map[box.left.y - 1][box.left.x].ToString() + map[box.right.y - 1][box.right.x].ToString();
                                if (nextPos == "..")
                                    foundSpacePerbox[k] = true;
                                else if (nextPos == "].")
                                {
                                    foundSpacePerbox[k] = false;
                                    newBoxesToMove.Add(((box.left.x - 1, box.left.y - 1), (box.left.x, box.left.y - 1)));
                                }
                                else if (nextPos == ".[")
                                {
                                    foundSpacePerbox[k] = false;
                                    newBoxesToMove.Add(((box.right.x, box.right.y - 1), (box.right.x + 1, box.right.y - 1)));
                                }
                                else if (nextPos == "[]")
                                {
                                    foundSpacePerbox[k] = false;
                                    newBoxesToMove.Add(((box.left.x, box.left.y - 1), (box.right.x, box.right.y - 1)));
                                }
                                else if (nextPos == "][")
                                {
                                    foundSpacePerbox[k] = false;
                                    newBoxesToMove.Add(((box.left.x - 1, box.left.y - 1), (box.left.x, box.left.y - 1)));
                                    newBoxesToMove.Add(((box.right.x, box.right.y - 1), (box.right.x + 1, box.right.y - 1)));
                                }
                                else if (nextPos.Contains("#"))
                                    foundWall = true;

                                if (foundWall)
                                    break;

                                k++;
                            }
                            boxesToMove.AddRange(newBoxesToMove);
                            break;
                        case Direction.Down:
                            foreach (var box in currentBoxes)
                            {
                                nextPos = map[box.left.y + 1][box.left.x].ToString() + map[box.right.y + 1][box.right.x].ToString();
                                if (nextPos == "..")
                                    foundSpacePerbox[k] = true;
                                else if (nextPos == "].")
                                {
                                    foundSpacePerbox[k] = false;
                                    newBoxesToMove.Add(((box.left.x - 1, box.left.y + 1), (box.left.x, box.left.y + 1)));
                                }
                                else if (nextPos == ".[")
                                {
                                    foundSpacePerbox[k] = false;
                                    newBoxesToMove.Add(((box.right.x, box.right.y + 1), (box.right.x + 1, box.right.y + 1)));
                                }
                                else if (nextPos == "[]")
                                {
                                    foundSpacePerbox[k] = false;
                                    newBoxesToMove.Add(((box.left.x, box.left.y + 1), (box.right.x, box.right.y + 1)));
                                }
                                else if (nextPos == "][")
                                {
                                    foundSpacePerbox[k] = false;
                                    newBoxesToMove.Add(((box.left.x - 1, box.left.y + 1), (box.left.x, box.left.y + 1)));
                                    newBoxesToMove.Add(((box.right.x, box.right.y + 1), (box.right.x + 1, box.right.y + 1)));
                                }
                                else if (nextPos.Contains("#"))
                                    foundWall = true;

                                if (foundWall)
                                    break;

                                k++;
                            }
                            boxesToMove.AddRange(newBoxesToMove);
                            break;
                        case Direction.Right:
                            nextChar = map[target.y][target.x + index * 2];
                            break;
                        case Direction.Left:
                            nextChar = map[target.y][target.x - index * 2];
                            break;
                    }

                    if (foundSpacePerbox.Length > 0)
                        foundSpace = foundSpacePerbox.All(x => x);

                    if (direction == Direction.Right || direction == Direction.Left)
                    {
                        if (nextChar == '.')
                            foundSpace = true;
                        else if (nextChar == (direction == Direction.Right ? '[' : ']'))
                            boxesCount++;
                        else if (nextChar == '#')
                            foundWall = true;
                    }

                    index++;
                } while (!foundSpace && !foundWall);

                if (foundSpace)
                {
                    map[robot.y][robot.x] = '.';
                    robot = target;
                    if (direction == Direction.Right || direction == Direction.Left)
                    {
                        for (int i = 1; i < boxesCount * 2; i += 2)
                        {
                            if (direction == Direction.Right)
                            {
                                map[robot.y][robot.x + i] = '[';
                                map[robot.y][robot.x + i + 1] = ']';
                            }
                            else
                            {
                                map[robot.y][robot.x - i] = ']';
                                map[robot.y][robot.x - i - 1] = '[';
                            }
                        }
                    }
                    else
                    {
                        boxesToMove.Reverse();
                        foreach (var box in boxesToMove)
                        {
                            map[box.left.y][box.left.x] = '.';
                            map[box.right.y][box.right.x] = '.';

                            switch (direction)
                            {
                                case Direction.Up:
                                    map[box.left.y - 1][box.left.x] = '[';
                                    map[box.right.y - 1][box.right.x] = ']';
                                    break;
                                case Direction.Down:
                                    map[box.left.y + 1][box.left.x] = '[';
                                    map[box.right.y + 1][box.right.x] = ']';
                                    break;
                            }
                        }
                        map[robot.y][robot.x] = '@';
                    }
                }
            }
        }

        public void Solve(int part)
        {
            string fileContent = File.ReadAllText("input");
            string[] lines = fileContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            if (part == 2)
            {
                for (int i = 0; i < lines.Length - 1; i++)
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        if (lines[i][j] == '#')
                            lines[i] = lines[i].Remove(j, 1).Insert(j, "##");
                        else if (lines[i][j] == 'O')
                            lines[i] = lines[i].Remove(j, 1).Insert(j, "[]");
                        else if (lines[i][j] == '.')
                            lines[i] = lines[i].Remove(j, 1).Insert(j, "..");
                        else if (lines[i][j] == '@')
                            lines[i] = lines[i].Remove(j, 1).Insert(j, "@.");

                        j++;
                    }
            }

            Position robot = (0, 0);

            char[][] map = new char[lines.Length - 1][];
            for (int i = 0; i < lines.Length - 1; i++)
            {
                map[i] = new char[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    map[i][j] = lines[i][j];
                    if (lines[i][j] == '@')
                        robot = (j, i);
                }
            }

            List<char> moveSequence = [.. lines.Last()];

            for (int i = 0; i < moveSequence.Count; i++)
            {
                MoveDelegate Move = part == 1 ? MovePart1 : MovePart2;

                switch (moveSequence[i])
                {
                    case '^':
                        Move(ref robot, ref map, Direction.Up);
                        break;
                    case 'v':
                        Move(ref robot, ref map, Direction.Down);
                        break;
                    case '<':
                        Move(ref robot, ref map, Direction.Left);
                        break;
                    case '>':
                        Move(ref robot, ref map, Direction.Right);
                        break;
                }
            }

            int result = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == (part == 1 ? 'O' : '['))
                        result += 100 * i + j;
                }
            }

            Console.WriteLine(result);
        }
    }
}