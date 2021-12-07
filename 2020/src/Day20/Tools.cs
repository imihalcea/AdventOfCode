using System.Linq;

namespace _2020
{
    public static class Tools
    {
        public static int[][] Transpose(int[][] m)
        {
            var cols = m[0].Length;
            var t = new int[cols][];
            for (var i = 0; i < cols; i++)
            {
                t[i] = m.Select(r => r[i]).ToArray();
            }
            return t;
        }
            
        public static int[][] Flip(int[][] m)
        {
            int[][] f = new int[m.Length][];
            int cols = m[0].Length;
            for (var i = 0; i < m.Length; i++)
            {
                f[i] = m[i].Reverse().ToArray();
            }
            return f;
        }

        public static int[][] None(int[][] m) => m;
            
        public static int[][] Rotate(int[][] m) => 
            Transpose(Flip(Transpose(m)));

        public static int[][] FlipAndRotate(int[][] m) =>
            Rotate(Flip(m));
            
        public static long ComputeHash(int[] pixels)
        {
            unchecked
            {
                return pixels.Aggregate(19L, (current, p) => current * 31 + p);
            }
        }
    }
}