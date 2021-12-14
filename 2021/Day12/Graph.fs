namespace _2021.Day12

open System.Collections.Generic
open FSharpx.Collections

[<AutoOpen>]
module Graph=
   
   type Vertex =
      Start |End |Big of string |Small of string
      static member Create (name:string)=
         match name with
         |"start" -> Start
         |"end" -> End
         |_ -> match (System.Char.IsLower name[0]) with
               |true -> Small(name)
               |false -> Big(name)
      
   type Graph = Map<Vertex, Vertex list>
   
   let empty = Map.empty<Vertex, Vertex list>
   let neighborsOf (vertex:Vertex) (graph:Graph) =
      graph.[vertex]


   let add (graph:Graph) (vertex:Vertex)=
      match Map.containsKey vertex graph with
      |true->graph
      |false-> graph |> Map.add vertex []

   let addMany (graph:Graph) (verticesToAdd:Vertex list)=
      verticesToAdd |> List.fold add graph

   let private _addEdge (from:Vertex) (``to``:Vertex) (graph:Graph):Graph =
      Map.add from (graph.[from]@[``to``]) graph

   let addEdge vertices (graph:Graph)=
      let (vertexFrom, vertexTo) = vertices
      [vertexFrom;vertexTo] 
      |> addMany graph
      |> _addEdge vertexFrom vertexTo

   let pushOnPath (path:Stack<Vertex>) (n:Vertex) =
      path.Push n
      path
      
   let findPaths (graph:Graph) (rule: Vertex-> bool ->bool->bool*bool)=
         let mutable toVisit = Stack<(Vertex list * HashSet<Vertex> * bool)>()
         toVisit.Push ([Start],HashSet<Vertex>([Start]),false)
         seq{
            while toVisit.Count>0 do
               let (path, vertices, twice) = toVisit.Pop()
               let current = List.head path
               
               for n in graph[current] do
                  match n with
                  |End  -> yield (End::path) |> List.rev
                  |_ ->
                     let inPath = List.contains n path
                     match (rule n inPath twice) with
                     |true,newTwice ->
                        toVisit.Push ((n::path), vertices,newTwice)
                     |false, _ -> ()
         }  
   let countPaths (graph:Graph) (rule: Vertex-> bool ->bool->bool*bool)=
         let mutable toVisit = Stack<(Vertex * Set<Vertex> * bool)>()
         toVisit.Push (Start,Set.ofList [Start],false)
         let mutable count = 0
         while toVisit.Count>0 do
               let (current, vertices, twice) = toVisit.Pop()
               
               for n in graph[current] do
                  match n with
                  |End  -> count <- count+1
                  |_ ->
                     let inPath = Set.contains n vertices
                     match (rule n inPath twice) with
                     |true,hasSmallTwice ->
                        toVisit.Push (n, (Set.add n vertices),hasSmallTwice)
                     |false, _ -> ()
         count
[<AutoOpen>]
module Undirected=
   let addEdge (graph:Graph) vertices  = 
         let (vertexFrom, vertexTo) = vertices
         graph
         |> Graph.addEdge (vertexFrom, vertexTo)
         |> Graph.addEdge (vertexTo,vertexFrom)