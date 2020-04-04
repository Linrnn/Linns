namespace Linns
{
    using Math = System.Math;

    public static class Data
    {
        public const float PropertyWidth = 8f;
        public const float PropertySize = 16f;
        public const float MinimumStandard = 0.00390625f;
        public const string NullString = null;

        public static void NullMethod()
        {
        }

        public static bool IsGreaterMin(float num)
        {
            return num > MinimumStandard;
        }
        public static bool IsGreaterMin(double num)
        {
            return num > MinimumStandard;
        }
        public static bool IsLessMin(float num)
        {
            return num < MinimumStandard;
        }
        public static bool IsLessMin(double num)
        {
            return num < MinimumStandard;
        }

        public static bool IsAbsGreaterMin(float num)
        {
            return Math.Abs(num) > MinimumStandard;
        }
        public static bool IsAbsGreaterMin(double num)
        {
            return Math.Abs(num) > MinimumStandard;
        }
        public static bool IsAbsLessMin(float num)
        {
            return Math.Abs(num) < MinimumStandard;
        }
        public static bool IsAbsLessMin(double num)
        {
            return Math.Abs(num) < MinimumStandard;
        }
    }
}