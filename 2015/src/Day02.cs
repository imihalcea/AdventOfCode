using System;
using System.Linq;

namespace _2015
{
    public class Day2
    {
        public static int ComputePaperArea(in int l, in int w, in int h)
        {
            var area = 2 * l * w + 2 * w * h + 2 * h * l;
            var extra = new[] {l, w, h}.OrderBy(i => i).Take(2).ToArray();
            return area + extra[0] * extra[1];
        }
        
        
        public static int ComputeRibbon(in int l, in int w, in int h)
        {
            var smallestSides = new[] {l, w, h}.OrderBy(i => i).Take(2).ToArray();
            return (2*smallestSides[0] + 2 * smallestSides[1]) + (l*w*h);
        }
        
        public static int ComputeManyAreas((int l, int w, int h)[] sizes) => 
            sizes.Select(item => ComputePaperArea(item.l, item.w, item.h)).Sum();
        
        public static int ComputeManyRibbons((int l, int w, int h)[] sizes) => 
            sizes.Select(item => ComputeRibbon(item.l, item.w, item.h)).Sum();
    }
}