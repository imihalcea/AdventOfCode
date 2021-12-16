module _2021.Day15.Solution

open System.Collections.Generic
open FSharpx.Collections

type Point = (int*int)
type Risk = int
type Edge = (Point * Point * Risk)
type CavernShape = int[,]

and Graph = {
    adjMap:Map<Point, (Point * int) list>
}

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Graph =
    let empty  = { adjMap = Map.empty }
    
    let addEdge (g: Graph) (edge:Edge) : Graph =
        let from,``to``, weight = edge
        let newAdj = Map.change from (fun adj ->
                            match adj with
                            | None -> Some([ (``to``, weight) ])
                            | Some (l) -> Some((``to``, weight) :: l)) g.adjMap
                        
        {g with adjMap = newAdj}

    let neighborsOf (v:Point) (g:Graph): (Point * int) list =
        g.adjMap.[v]

    let djikstra (from: Point) (``to``: Point) (graph: Graph) =
        let mutable weights = [(from, 0)] |> Map.ofList
        let mutable current = from
        let mutable queue = PriorityQueue<Point, int>()
        queue.Enqueue (from,0)
        
        while current <> ``to`` do
            current <- queue.Dequeue()
            
            for (n,w) in neighborsOf current graph do
                    let acc = weights[current] + w
                    if acc < weights.GetValueOrDefault(n, 1_000_000) then
                        queue.Enqueue (n, acc)
                        weights <- Map.change n (fun _ -> Some(acc)) weights
        weights  

let mapSize (m:int[,])= (m.[*,0].Length - 1, m.[0,*].Length - 1)

let readMap (m:CavernShape) (p:Point):int = 
    m[fst p, snd p]

let nextPoints (fRead:Point -> int) (h:int) (w:int) (from:Point):(Point * int)[]=
    let inBounds p =
        (fst p) >= 0 && (fst p) <= h && (snd p) >= 0 && (snd p) <= w
    let r,c = from
    [|Point (r+1,c);Point (r, c+1); Point(r-1,c); Point(r, c-1)|]
    |> Array.filter inBounds
    |> Array.map (fun p -> (p, fRead p))
    
let points rows cols =
          seq{
            for r in 0..rows do
               for c in 0..cols do
                  yield Point(r,c)
            }

let computeRisks (topLeft:Point) (bottomRight:Point) (map:CavernShape)=
   let rows,cols = mapSize map
   let fread = readMap map
   let nextPoints = nextPoints fread rows cols
   let graph = 
       points rows cols
       |> Seq.map (fun from -> nextPoints from |> Seq.map (fun ``to`` -> (from, fst ``to``, snd ``to``)))
       |> Seq.concat
       |> Seq.cons (bottomRight, bottomRight, 0) //add the bottomRight risk to the graph 
       |> Seq.fold Graph.addEdge Graph.empty
   Graph.djikstra topLeft bottomRight graph
          
          
let part1 (map:int[,]) =
   let rows,cols = mapSize map
   (computeRisks (Point (0,0)) (Point(rows, cols)) map)[(Point(rows, cols))]

let augment (m:int[,])=
    let rows, cols = (m.[*,0].Length, m.[0,*].Length)
    seq{
        for r in 0..(rows*5)-1 do
            yield seq{
                for c in 0..(cols*5)-1 do
                    let originRow, originColumn = r%rows, c%cols 
                    let originalValue = m[originRow,originColumn]
                    let distanceToOrigin = (r/rows) + (c/cols)
                    let newValue = (originalValue + distanceToOrigin - 1)%9+1
                    yield newValue
            }
    } |> array2D
    
    
let part2 (map:int[,]) =
   let augmentedMap = augment map
   let rows,cols = mapSize augmentedMap
   (computeRisks (Point (0,0)) (Point(rows, cols)) augmentedMap)[(Point(rows, cols))]