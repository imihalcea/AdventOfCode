using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace _2020.tools
{
    public class Graph<T>
    {
        private readonly Kind _kind;

        public enum Kind
        {
            Directed,
            Undirected
        }
        public T[] Vertices { get; }
        private (int from, int to, int weight)[] E;
        private Dictionary<(int @from, int to), int> Weights { get; }
        private int[][] Adj { get; }
        public Graph((T from, T to, int w)[] edges, Kind kind = Kind.Directed)
        {
            _kind = kind;
            Vertices = edges.SelectMany(e=>new []{e.from,e.to}).Distinct().ToArray();
            E = edges.SelectMany(e => CreateEdge(e,kind)).Distinct().ToArray();
            Weights = E.ToDictionary(e => (e.from, e.to), e => e.weight);
            Adj = E.GroupBy(e => e.from, e => e.to).Aggregate(new int[Vertices.Length][], (acc, g) =>
            {
                acc[g.Key] = g.ToArray();
                return acc;
            });
        }

        


        public (int?[] predecessors, int[] weights) Dfsi(int srcIdx, Func<int, bool>? stopCondition=null, Action<int?,  int, int>? visitAction=null)
        {
            stopCondition ??= _ => false;
            visitAction ??= (_, __,___) => { };
            var marked = new bool[Vertices.Length]; 
            var predecessors = new int?[Vertices.Length];
            var weights = Vertices.Select((node, idx) => srcIdx==idx?0:int.MaxValue).ToArray();
            int? predecessor = null;   
            void VisitNode(int idx)
            {
                
                marked[idx] = true;
                predecessors[idx] = predecessor;
                var weight = Weight(predecessor, idx).GetValueOrDefault(int.MaxValue);
                weights[idx] = weight;
                visitAction(predecessor, idx,weight);
                predecessor = idx;

            }
            var toVisit = new Stack<int>();
            toVisit.Push(srcIdx);
            do
            {
                var visitIdx = toVisit.Pop();

                if (!marked[visitIdx]) 
                    VisitNode(visitIdx);
                
                if(stopCondition(visitIdx)) return (predecessors, weights);
                
                foreach (var neighbor in NeighborsOf(visitIdx))
                    if (!marked[neighbor])
                        toVisit.Push(neighbor);
            } while (toVisit.Count > 0);

            return (predecessors, weights);
        } 
        public (int?[] predecessors, int[] weights) Dfs(T source, Func<int, bool> stopCondition, Action<int?,  int, int>? visitAction=null)
        {
            return Dfsi(IndexOf(source), stopCondition, visitAction);
        }       

        public int IndexOf(T vertex) => Array.IndexOf(Vertices, vertex);

        public int[] NeighborsOf(int vertex) =>
            Adj[vertex] ?? new int[0];

        public int? Weight(int? from, int to)
        {
            if (@from == null) return 0;
            var key = (@from.Value, to);
            if (!Weights.ContainsKey(key)) return null;
            return Weights[key];
        }

        private IEnumerable<(int from, int to, int weight)> CreateEdge((T from, T to, int weight) edgeDef,
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