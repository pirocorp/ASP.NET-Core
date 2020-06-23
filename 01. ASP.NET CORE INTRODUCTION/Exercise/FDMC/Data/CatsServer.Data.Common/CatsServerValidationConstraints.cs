namespace CatsServer.Data.Common
{
    public class CatsServerValidationConstraints
    {
        public class Cat
        {
            public const int StringMaxLength = 50;
            public const int AgeMinValue = 0;
            public const int AgeMaxValue = 30;
            public const int ImageUrlMaxLength = 2000;
        }
    }
}
