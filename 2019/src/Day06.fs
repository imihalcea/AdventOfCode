module Day06
open System.Collections.Generic
open FSharpx.Collections
open System

[<AutoOpen>]
module Graph=
   type Vertex = string
   type Graph = Map<Vertex, Vertex list>
   
   let empty = Map.empty<Vertex, Vertex list>

   let contains vertex graph = Map.containsKey vertex graph
   
   let neighborsOf (vertex:Vertex) (graph:Graph) =
      match contains vertex graph with
      |true -> graph.[vertex]
      |false -> []

   let vertices graph = graph |> Map.keys

   let add (graph:Graph) (vertex:Vertex)=
      match contains vertex graph with
      |true->graph
      |false-> graph |> Map.add vertex []

   let addMany (graph:Graph) (verticesToAdd:Vertex list)=
      verticesToAdd |> List.fold add graph

   let private addNeighborOf (vertex:Vertex) (neighbor:Vertex) (graph:Graph):Graph =
      Map.add vertex (graph.[vertex]@[neighbor]) graph

   let addEdge vertices (graph:Graph)=
      let (vertexFrom, vertexTo) = vertices
      [vertexFrom;vertexTo] 
      |> addMany graph
      |> addNeighborOf vertexFrom vertexTo

   let findPath (fromVertex:Vertex) (toVertex:Vertex) (graph:Graph)=
         let mutable alreadyVisited = List.empty<Vertex>
         let mutable toVisit = Queue.empty<Vertex list>
         toVisit <- toVisit.Conj [fromVertex]
         seq{
            while not toVisit.IsEmpty do
               let (currentPath,newToVisit) = Queue.uncons toVisit
               let current = List.head currentPath
               alreadyVisited <- current :: alreadyVisited
               toVisit <- graph 
                        |> neighborsOf current
                        |> List.except alreadyVisited
                        |> List.map (fun v -> v::currentPath)
                        |> List.fold (fun q p -> q.Conj p) newToVisit
               if current=toVertex then
                  yield currentPath
         }

module Directed=
      let addEdge (graph:Graph) vertices  = 
         let (vertexFrom, vertexTo) = vertices
         graph |>Graph.addEdge (vertexFrom, vertexTo)

module Undirected=
   let addEdge (graph:Graph) vertices  = 
         let (vertexFrom, vertexTo) = vertices
         graph |> Graph.addEdge (vertexFrom, vertexTo)
         |> Graph.addEdge (vertexTo,vertexFrom)

module Orbit =
   let shortestPath (fromPlanet:Vertex) (toPlanet:Vertex) (orbitMap:Graph) =
       (fromPlanet,toPlanet,orbitMap)|||>Graph.findPath
       |> Seq.sortBy (fun path -> path.Length)
       |> Seq.head

   let countOrbitsTo lastObject orbitMap =
      orbitMap |> vertices |> Seq.map (fun vertex -> shortestPath vertex lastObject orbitMap) 
      |> Seq.sumBy (fun path -> path.Length |> (+) -1)
 
   let part1 (input:string array) =
    input
    |> Array.map (fun strOrbit -> strOrbit.Split(")"))
    |> Array.fold (fun g o -> Directed.addEdge g (o.[1],o.[0])) Graph.empty
    |> countOrbitsTo "COM"

   let part2 (input:string array) =
    input
    |> Array.map (fun strOrbit -> strOrbit.Split(")"))
    |> Array.fold (fun g o -> Undirected.addEdge g (o.[0],o.[1])) Graph.empty
    |> shortestPath "YOU" "SAN"
    |> Seq.except ["YOU";"SAN"]
    |> Seq.length
    |> (+) -1