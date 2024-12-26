namespace _2024.Day21
{
    internal class Day21
    {
        public void Solve()
        {
            string input = File.ReadAllText("input");
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            char[][] keypad = {
                new char[] { '7', '8', '9' },
                new char[] { '4', '5', '6' },
                new char[] { '1', '2', '3' },
                new char[] { '#', '0', 'A' },
            };

            char[][] dirPad =
            {
                new char[] { '#', '^', 'A' },
                new char[] { '<', 'v', '>' }
            };

            (int, int) keypadStartPos = (3, 2);
            (int, int) dirPadStartPos = (0, 2);

            Dictionary<char, List<(char, string)>> buttons = [];
            buttons.Add('7', [
                ('8', ">"),
                ('9', ">>"),
                ('4', "v"),
                ('5', "v>"),
                ('6', "v>>"),
                ('1', "vv"),
                ('2', "vv>"),
                ('3', "vv>>>"),
                ('0', ">vvv"),
                ('A', ">>vvv"),
                ('7', "")
                ]);
            buttons.Add('8', [
                ('9', ">"),
                ('7', "<"),
                ('5', "v"),
                ('4', "<v"),
                ('6', "v>"),
                ('2', "vv"),
                ('1', "v<v"),
                ('3', "vv>"),
                ('0', "vvv"),
                ('A', "vvv>"),
                ('8', "")
                ]);
            buttons.Add('9', [
                ('8', "<"),
                ('7', "<<"),
                ('6', "v"),
                ('5', "<v"),
                ('4', "<<v"),
                ('3', "vv"),
                ('2', "<vv"),
                ('1', "<<vv"),
                ('0', "<vvv"),
                ('A', "vvv"),
                ('9', "")
                ]);
            buttons.Add('4', [
                ('5', ">"),
                ('6', ">>"),
                ('7', "^"),
                ('8', ">^"),
                ('9', ">>^"),
                ('1', "v"),
                ('2', "v>"),
                ('3', "v>>"),
                ('0', ">vv"),
                ('A', ">>vv"),
                ('4', "")
                ]);
            buttons.Add('5', [
                ('4', "<"),
                ('6', ">"),
                ('8', "^"),
                ('7', "<^"),
                ('9', ">^"),
                ('2', "v"),
                ('1', "<v"),
                ('3', "v>"),
                ('0', "vv"),
                ('A', "vv>"),
                ('5', "")
                ]);
            buttons.Add('6', [
                ('5', "<"),
                ('4', "<<"),
                ('9', "^"),
                ('8', "<^"),
                ('7', "<<^"),
                ('3', "v"),
                ('2', "<v"),
                ('1', "<v"),
                ('0', "<vv"),
                ('A', "vv"),
                ('6', "")
                ]);
            buttons.Add('1', [
                ('2', ">"),
                ('3', ">>"),
                ('4', "^"),
                ('5', "^>"),
                ('6', ">>^"),
                ('7', "^^"),
                ('8', ">^^"),
                ('9', ">>^^"),
                ('0', ">v"),
                ('A', ">>v"),
                ('1', "")
                ]);
            buttons.Add('2', [
                ('1', "<"),
                ('3', ">"),
                ('5', "^"),
                ('4', "<^"),
                ('6', ">^"),
                ('8', "^^"),
                ('7', "<^^"),
                ('9', ">^^"),
                ('0', "v"),
                ('A', "v>"),
                ('2', "")
                ]);
            buttons.Add('3', [
                ('2', "<"),
                ('1', "<<"),
                ('6', "^"),
                ('5', "<^"),
                ('4', "<<^"),
                ('9', "^^"),
                ('8', "<^^"),
                ('7', "<<^^"),
                ('0', "<v"),
                ('A', "v"),
                ('3', "")
                ]);
            buttons.Add('0', [
                ('A', ">"),
                ('1', "^<"),
                ('2', "^"),
                ('3', ">^"),
                ('4', "^^<"),
                ('5', "^^"),
                ('6', ">^^"),
                ('7', "^^^<"),
                ('8', "^^^"),
                ('9', ">^^^"),
                ('0', "")
                ]);
            buttons.Add('A', [
                ('0', "<"),
                ('1', "^<<"),
                ('2', "<^"),
                ('3', "^"),
                ('4', "^^<<"),
                ('5', "<^^"),
                ('6', "^^"),
                ('7', "^^^<<"),
                ('8', "<^^^"),
                ('9', "^^^"),
                ('A', ""),
                ]);

            Dictionary<char, List<(char, string)>> buttonsDir = [];
            buttonsDir.Add('A', [
                ('^', "<"),
                ('>', "v"),
                ('<', "v<<"),
                ('v', "<v"),
                ('A', ""),
                ]);
            buttonsDir.Add('>', [
                ('^', "<^"),
                ('<', "<<"),
                ('v', "<"),
                ('A', "^"),
                ('>', ""),
                ]);
            buttonsDir.Add('<', [
                ('^', ">^"),
                ('>', ">>"),
                ('v', ">"),
                ('A', ">>^"),
                ('<', ""),
                ]);
            buttonsDir.Add('v', [
                ('^', "^"),
                ('>', ">"),
                ('<', "<"),
                ('A', "^>"),
                ('v', ""),
                ]);
            buttonsDir.Add('^', [
                ('>', "v>"),
                ('<', "v<"),
                ('v', "v"),
                ('A', ">"),
                ('^', ""),
                ]);

            int result = 0;
            foreach (var code in lines)
            {
                string firstSequence = "";
                string secondSequence = "";
                string thirdSequence = "";

                (int, int) currentPos = keypadStartPos;

                foreach (char c in code)
                {
                    (int, int) nextPos = (0, 0);
                    for (int i = 0; i < keypad.Length; i++)
                        for (int j = 0; j < keypad[0].Length; j++)
                            if (keypad[i][j] == c)
                            {
                                nextPos = (i, j);
                                break;
                            }

                    firstSequence += buttons[keypad[currentPos.Item1][currentPos.Item2]].First(x => x.Item1 == c).Item2;

                    firstSequence += "A";
                    currentPos = nextPos;
                }

                currentPos = dirPadStartPos;
                foreach (char c in firstSequence)
                {
                    (int, int) nextPos = (0, 0);
                    for (int i = 0; i < dirPad.Length; i++)
                        for (int j = 0; j < dirPad[0].Length; j++)
                            if (dirPad[i][j] == c)
                            {
                                nextPos = (i, j);
                                break;
                            }

                    secondSequence += buttonsDir[dirPad[currentPos.Item1][currentPos.Item2]].First(x => x.Item1 == c).Item2;

                    secondSequence += "A";
                    currentPos = nextPos;
                }

                currentPos = dirPadStartPos;
                foreach (char c in secondSequence)
                {
                    (int, int) nextPos = (0, 0);
                    for (int i = 0; i < dirPad.Length; i++)
                        for (int j = 0; j < dirPad[0].Length; j++)
                            if (dirPad[i][j] == c)
                            {
                                nextPos = (i, j);
                                break;
                            }

                    thirdSequence += buttonsDir[dirPad[currentPos.Item1][currentPos.Item2]].First(x => x.Item1 == c).Item2;

                    thirdSequence += "A";
                    currentPos = nextPos;
                }

                result += thirdSequence.Length * int.Parse(code[0..3]);
            }

            Console.WriteLine(result);
        }
    }
}