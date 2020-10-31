namespace P02._Virtuals_Before
{
    using System;

    public class Cat : Animal
    {
        public Cat(string name = null)
            :base(name)
        {
        }

        protected override string SetDefaultName()
        {
            return "Name";
        }
    }
}
