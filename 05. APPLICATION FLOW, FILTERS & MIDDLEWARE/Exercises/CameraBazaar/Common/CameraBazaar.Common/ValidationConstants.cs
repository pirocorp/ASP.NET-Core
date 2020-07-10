namespace CameraBazaar.Common
{
    public static class ValidationConstants
    {
        public static class Camera
        {
            public const int ModelMaxLength = 100;

            public const int QuantityMin = 0;

            public const int QuantityMax = 100; 

            public const int MinShutterSpeedMin = 1; 

            public const int MinShutterSpeedMax = 30; 

            public const int MaxShutterSpeedMin = 2000; 

            public const int MaxShutterSpeedMax = 8000; 

            // ReSharper disable InconsistentNaming
            public const int MaxISOMin = 200; 

            public const int MaxISOMax = 409600;

            public const int VideoResolutionMaxLength = 15;

            public const int DescriptionMaxLength = 6000;

            public const int ImageUrlMinLength = 10;

            public const int ImageUrlMaxLength = 2000;
        }
    }
}
