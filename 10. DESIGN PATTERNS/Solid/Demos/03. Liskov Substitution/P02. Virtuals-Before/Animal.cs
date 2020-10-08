namespace P02._Virtuals_Before
{
    public class Animal
    {
        // ReSharper disable VirtualMemberCallInConstructor
        public Animal(string name = null)
        {
            this.Name = name ?? this.SetDefaultName();
        }

        public string Name { get; private set; }

        protected virtual string SetDefaultName()
        {
            return "Animal";
        }
    }
}
