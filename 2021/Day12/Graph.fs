namespace _2021.Day12

open System.Collections.Generic
open FSharpx.Collections

[<AutoOpen>]
module Graph=
   
   type Vertex =
      |Start
      |End
      |Big of string
      |Small of string
      
      static member Create (name:string)=
         match name with
         |"start" -> Start
         |"end" -> End
         |_ -> match (System.Char.IsLower name[0]) with
               |true -> Small(name)
               |false -> Big(name)
      
   type Graph = Map<Vertex, Vertex list>
   
   let empty = Map.empty<Vertex, Vertex list>

   let contains vertex graph = Map.containsKey vertex graph
  
   let neighborsOf (vertex:Vertex) (graph:Graph) =
      match contains vertex graph with
      |true -> graph.[vertex]
      |false -> []

   let add (graph:Graph) (vertex:Vertex)=
      match contains vertex graph with
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

     
   let findPaths (graph:Graph) (smallCaveRule:bool -> bool ->bool)=
         let mutable toVisit = Stack<(Vertex list * bool)>()
         toVisit.Push ([Start],false)
         seq{
            while toVisit.Count>0 do
               let (path, twice) = toVisit.Pop()
               let current = List.head path
               
               for n in (neighborsOf current graph) do
                  match (n) with
                  |End  -> yield (End::path) |> List.rev
                  |Small _ ->
                     let inPath = List.contains n path
                     if (smallCaveRule inPath twice) then
                       toVisit.Push ((n::path), inPath || twice)
                  |Big _ -> toVisit.Push((n::path),twice)
                  |_ -> ()
         }  
   
[<AutoOpen>]
module Undirected=
   let addEdge (graph:Graph) vertices  = 
         let (vertexFrom, vertexTo) = vertices
         graph
         |> Graph.addEdge (vertexFrom, vertexTo)
         |> Graph.addEdge (vertexTo,vertexFrom)