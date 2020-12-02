using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Linq.Enumerable;

namespace _2015.tools
{
    public class Graph
    {
        private readonly Kind _kind;

        public enum Kind
        {
            Directed,
            Undirected
        }
        public string[] V { get; }
        private (int from, int to, int weight)[] E;
        private int[][] Adj { get; }
        public Graph((string from, string to, int d)[] edges, Kind kind = Kind.Directed)
        {
            _kind = kind;
            V = edges.SelectMany(e=>new []{e.from,e.to}).Distinct().ToArray();
            E = edges.SelectMany(e => CreateEdge(e,kind)).Distinct().ToArray();
            Adj = E.GroupBy(e => e.from, e => e.to).Aggregate(new int[V.Length][], (acc, g) =>
            {
                acc[g.Key] = g.ToArray();
                return acc;
            });
        }

        
        public (int[] vertices, int?[] weights) HamiltonianCycle_BruteForce(Func<IEnumerable<(int[] vertices,int?[] weights)>, (int[] vertices,int?[] weights)> selector)
        {
            //optimizations ideas
            // * limit the number of permutations to real possible paths in the graph 
            // * approach based on a MinimumSpanningTree (MST)  
            
            var candidates = Range(0, V.Length).ToArray()
                .Permutations()
                .Select(c => (c, c.Pairwise((from, to) => Weight(from, to)).ToArray()));
            return selector(candidates);
        }
       
        
        public (int?[] predecessors, int[] weights) Bellman_Ford(string source)
        {
            var weights = V.Select((node, idx) => IndexOf(source)==idx?0:int.MaxValue).ToArray();
            var predecessors = new int?[V.Length];

            foreach (var pass in Range(1,V.Length -1))
            foreach (var (u, v, w) in E)
                if (weights[u] != int.MaxValue && weights[u] + w < weights[v])
                {
                    weights[v] = weights[u] + w;
                    predecessors[v] = u;
                }

            return (predecessors, weights);
        }

        public (int?[] predecessors, int[] weights) Dfs(string source)
        {
            var marked = new bool[V.Length]; 
            var predecessors = new int?[V.Length];
            var weights = V.Select((node, idx) => IndexOf(source)==idx?0:int.MaxValue).ToArray();
            int? predecessor = null;   
            void VisitNode(int idx)
            {
                marked[idx] = true;
                predecessors[idx] = predecessor;
                weights[idx] = Weight(predecessor, idx).GetValueOrDefault(int.MaxValue);
                predecessor = idx;

            }
            var toVisit = new Stack<int>();
            toVisit.Push(IndexOf(source));
            do
            {
                var visitIdx = toVisit.Pop();

                if (!marked[visitIdx]) 
                    VisitNode(visitIdx);
                
                foreach (var neighbor in NeighborsOf(visitIdx))
                    if (!marked[neighbor])
                        toVisit.Push(neighbor);
            } while (toVisit.Count > 0);

            return (predecessors, weights);
        }       

        public (int?[] predecessors, int[] weights) Bfs(string source)
        {
            var predecessors = new int?[V.Length];
            var distance = V.Select((node, idx) => IndexOf(source)==idx?0:int.MaxValue).ToArray();
            var marked = new bool[V.Length];
            var queue = new Queue<int>();
            var startIdx = IndexOf(source);
            int? predecessor = null;
            queue.Enqueue(startIdx);
                    
            while (queue.Count>0)
            {
                var current = queue.Dequeue();
                if (!marked[current])
                {
                    visit(current);
                    foreach (var n in NeighborsOf(current))
                        if (!marked[n])
                            queue.Enqueue(n);
                }
            }

            return (predecessors, distance);
            void visit(int idx)
            {
                marked[idx] = true;
                predecessors[idx] = predecessor;
                distance[idx] = Weight(predecessor, idx).GetValueOrDefault(int.MaxValue);
                predecessor = idx;
            }
        }

        
 
        private int IndexOf(string vertex) => Array.IndexOf(V, vertex);

        private int[] NeighborsOf(int vertex) =>
            Adj[vertex] ?? new int[0];

        private int? Weight(int? from, int to)
        {
            if (@from == null) return 0;
            var egdes = E.Where(e => e.@from == @from && e.to == to).ToArray();
            if (!egdes.Any()) return null;
            return egdes.Single().weight;
        }

        private IEnumerable<(int from, int to, int weight)> CreateEdge((string from, string to, int weight) edgeDef,
            Kind kind)
        {
            var edges = new List<(int from, int to, int weight)>()
            {
                (IndexOf(edgeDef.from), IndexOf(edgeDef.to), edgeDef.weight)
            };
            if (kind == Kind.Undirected)
            {
                edges.Add((IndexOf(edgeDef.to), IndexOf(edgeDef.from), edgeDef.weight));
            }

            return edges;
        }
    }
}