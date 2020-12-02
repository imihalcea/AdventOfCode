using System;
using System.Diagnostics;
using NFluent.Extensions;

namespace _2020.test
{
    public static class TestExtensions
    {
        public static void Answer<X, Y>(Func<X, Y> f, X x)
        {
            var sw = Stopwatch.StartNew();
            var y = f(x);
            sw.Stop();
            Console.WriteLine($"Response: {y} in {sw.ElapsedMilliseconds} ms");
        }
        
        public static void Print<T>(this T @this)
        {
            Console.WriteLine(@this.ToStringProperlyFormatted());
        } 
    }
}