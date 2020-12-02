using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static System.Text.Encoding;

namespace _2015
{
    public class Day4
    {
        public static int Compute(string input, string hashStart)
        {
            return TryFind(input, hash => hash.StartsWith(hashStart), (int)Math.Pow(10, 8));
        }

        private static int TryFind(string input, Predicate<string> stopCondition, int maxAttempts)
        {
            using var md5 = MD5.Create();
            return
                (from i in Enumerable.Range(0, maxAttempts) 
                    let hash = string.Concat(md5.ComputeHash(ASCII.GetBytes($"{input}{i}")).Select(b => $"{b:x2}"))
                    where stopCondition(hash)
                    select i)
                .FirstOrDefault();
        }
    }
}