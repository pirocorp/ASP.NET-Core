using System;

namespace P01._Square_Before
{
    class Program
    {
        static void Main(string[] args)
        {
            var rect = new Rectangle();

            rect.Width = 10;
            rect.Height = 20;

            var sq = new Square();
            sq.Width = 10;
            sq.Height = 20;

            Console.WriteLine(rect.Area);
            Console.WriteLine(sq.Area);
        }
    }
}
