using System.Drawing;
using System.Text.RegularExpressions;
using Position = (int x, int y);
using Velocity = (int x, int y);

namespace _2024.Day14
{
    internal class Day14
    {
        public static Bitmap CreateImageFromIntArray(int[][] pixelArray)
        {
            int width = pixelArray.Length;
            int height = pixelArray[0].Length;

            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int pixelColor = 0;
                    if (pixelArray[x][y] <= 0)
                        pixelColor = 255;
                    Color color = Color.FromArgb(pixelColor, pixelColor, pixelColor);
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }

        public void Solve(int part)
        {
            string fileContent = File.ReadAllText("input");
            string[] lines = fileContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string regex = @"-?[0-9]\d*";
            List<(Position pos, Velocity vel)> robots = [];

            foreach (var line in lines)
            {
                var matches = Regex.Matches(line, regex);
                int posX = int.Parse(matches[0].Value);
                int posY = int.Parse(matches[1].Value);
                int velX = int.Parse(matches[2].Value);
                int velY = int.Parse(matches[3].Value);

                robots.Add(((posX, posY), (velX, velY)));
            }

            int mapWidth = 101;
            int mapHeight = 103;

            int[][] map = new int[mapHeight][];
            for (int i = 0; i < mapHeight; i++)
            {
                map[i] = new int[mapWidth];
                for (int j = 0; j < mapWidth; j++)
                {
                    map[i][j] = 0;
                }
            }

            foreach (var (pos, _) in robots)
            {
                map[pos.y][pos.x] += 1;
            }

            int time = part == 1 ? 100 : 10000;
            int[] quadrants = [0, 0, 0, 0];
            for (long i = 0; i < time; i++)
            {
                for (int j = 0; j < robots.Count; j++)
                {
                    var (pos, vel) = robots[j];

                    map[pos.y][pos.x]--;

                    pos.x += vel.x;
                    pos.y += vel.y;

                    if (pos.x >= mapWidth)
                        pos.x -= mapWidth;
                    if (pos.y >= mapHeight)
                        pos.y -= mapHeight;
                    if (pos.x < 0)
                        pos.x += mapWidth;
                    if (pos.y < 0)
                        pos.y += mapHeight;

                    map[pos.y][pos.x]++;

                    robots[j] = (pos, vel);
                }

                if (part == 2)
                {
                    // Part 2, look at every generated image and to find the christmas tree ! Enjoy :D
                    Bitmap image = CreateImageFromIntArray(map);
                    image.Save("images\\" + (i + 1).ToString() + ".png");
                }
            }

            if (part == 1)
            {
                foreach (var robot in robots)
                {
                    var (pos, _) = robot;
                    if (pos.x < mapWidth / 2 && pos.y < mapHeight / 2)
                        quadrants[0]++;
                    else if (pos.x > mapWidth / 2 && pos.y < mapHeight / 2)
                        quadrants[1]++;
                    else if (pos.x < mapWidth / 2 && pos.y > mapHeight / 2)
                        quadrants[2]++;
                    else if (pos.x > mapWidth / 2 && pos.y > mapHeight / 2)
                        quadrants[3]++;
                }

                Console.WriteLine(quadrants.Aggregate((acc, x) => acc *= x));
            }
        }
    }
}
