using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeOOP1
{
    class Program
    {
        static void Main(string[] args)
        {
            int score = 0;
            Walls walls = new Walls(80, 25);
            walls.Draw();

            Point snakeTail = new Point(15, 15, '.');
            Snake snake = new Snake(snakeTail, 5, Direction.RIGHT);
            snake.Draw();

            FoodGenerator foodGenerator = new FoodGenerator(80, 25, '$');
            Point food = foodGenerator.GenerateFood();
            Random rnd = new Random();
            Console.ForegroundColor = (ConsoleColor)rnd.Next(1, 3);
            food.Draw();
            
            while (true)
            {
                if(walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if(snake.Eat(food))
                {
                    food = foodGenerator.GenerateFood();
                    food.Draw();
                    score++;
                }
                else
                {
                    snake.Move();
                }

                if(Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKeys(key.Key);
                }

                Thread.Sleep(300);
            }

            string str_score = Convert.ToString(score);
            WriteGameOver(str_score);
            Console.ReadLine();
        }

        public static void WriteGameOver(string score)
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("==========================", xOffset, yOffset++);
            WriteText("       GAME OVER     ", xOffset+1, yOffset++);
            yOffset++;
            WriteText($" You scored {score} points", xOffset + 2, yOffset++);
            WriteText("", xOffset+1, yOffset++);
            WriteText("==========================", xOffset, yOffset++);

        }

        public static void WriteText(string text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}
