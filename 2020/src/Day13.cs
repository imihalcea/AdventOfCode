using System.Collections.Immutable;
using System.Data;
using System.IO;
using System.Linq;

namespace _2020
{
    public class Day13
    {
        public static int Part1((int time, (int busId, int delay)[] busLines) input)
        {
            var (estimatedTime, busLines) = input;
            var (busLine, depTime) = 
                (from i in Enumerable.Range(0, 1000)
                    from busId in busLines.Select(bl => bl.busId)
                    let candidate = estimatedTime + i
                    where candidate % busId == 0
                    select (busId, candidate))
                .FirstOrDefault()!;
            return (depTime - estimatedTime) * busLine;
        }
        
        public static long Part2((int time, (int busId, int delay)[] busLines) input)
        {
            var (_, busLines) = input;
           return ChineseRemainderTheorem(busLines.Select(it => (it.busId, it.busId - it.delay)).ToArray());
        }

        //https://fr.wikipedia.org/wiki/Th%C3%A9or%C3%A8me_des_restes_chinois
        //https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23
        public static long ChineseRemainderTheorem((int mod, int rem)[] items)
        {
            var prod = items.Aggregate(1L, (acc, it) => acc * it.mod);
            var sum = items.Select(it =>
            {
                var p = prod / it.mod;
                return it.rem * ModularMultiplicativeInverse(p, it.mod) * p;
            }).Sum();
            return sum % prod;
            
            long ModularMultiplicativeInverse(long rem, long mod)
            {
                long b = rem % mod;
                for (var x = 1; x < mod; x++)
                    if ((b * x) % mod == 1)
                        return x;

                return 1;
            }
        }
        
        public static (int time, (int busId, int delay)[] busLines) Dataset(string filePath)
        {
            var inputLines = File.ReadAllLines(filePath);
            var busLines = inputLines[1].Split(",").Select((v,idx)=>(v,idx))
                .Where(it => it.v != "x")
                .Select(it => (int.Parse(it.v),it.idx)).ToArray();
            var time = int.Parse(inputLines[0]);
            return (time, busLines);
        }
    }
}