using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _2015.tools;

namespace _2015
{
    public class Day9
    {

        public static int ShortestDistance(Graph g)
        {
            var valueTuples = g.V.Select(g.Bfs).ToArray();
            var tuplesBfs = valueTuples
                .Where(result => HasUniqueVisits(result.predecessors, g)).ToArray();
            
            return tuplesBfs
                .Select(result => result.weights!.Sum())
                .Min();
        }

        private static bool HasUniqueVisits(int?[] predecessors, Graph g)
        {
            return new HashSet<int?>(predecessors).Count == g.V.Length;
        }
         
    }
}