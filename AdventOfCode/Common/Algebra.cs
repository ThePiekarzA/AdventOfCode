namespace AdventOfCode.Common
{
    public class Algebra
    {
        public static int MathMod(int a, int b)
        {
            return (Math.Abs(a * b) + a) % b;
        }
    }
}
