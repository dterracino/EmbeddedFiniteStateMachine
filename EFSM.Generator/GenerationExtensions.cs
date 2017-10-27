namespace EFSM.Generator
{
    internal static class GenerationExtensions
    {
        public static string FixDefineName(this string value)
        {
            if (value == null)
                return null;

            return value.Replace(" ", "_").ToUpper();
        }

        public static string FixFunctionName(this string value)
        {
            if (value == null)
                return null;

            return value.Replace(" ", "");
        }
    }
}