using System;

namespace P02._Virtuals_Before
{
    class Program
    {
        static void Main(string[] args)
        {
            var animal = new Animal();

            Console.WriteLine(animal.Name);

            var cat = new Cat();

            Console.WriteLine(cat.Name);
        }
    }
}
