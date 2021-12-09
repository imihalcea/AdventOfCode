module _2021.Day09.Solution

open System.Collections.Generic

let mapSize (m:int[,])=
    (m.[*,0].Length - 1, m.[0,*].Length - 1)

let points rows cols =
    seq{
        for r in 0..rows do
            for c in 0..cols do
                yield (r,c)
    }

let adjacent point rows cols =
    let (r,c) = point
    [(r-1,c); (r+1,c); (r, c-1); (r, c+1)]
    |> Seq.filter (fun p -> (fst p) >= 0 && (fst p) <= rows && (snd p) >= 0 && (snd p) <= cols)

let lowPoints (heightMap:int[,]) =
    let (rows,cols) = mapSize heightMap
    points rows cols
    |> Seq.filter (fun point ->
                                (point,rows,cols)
                                |||> adjacent 
                                |> Seq.forall (fun adj -> heightMap.[fst point, snd point] < heightMap.[fst adj, snd adj])
                   )

let basinsSize (heightMap:int[,]):seq<int> =
    let (rows,cols) = mapSize heightMap 
    
    let rec basinSize (lowPoint:int * int) =
        let visited = HashSet()
        let queue = Queue<int * int>()
        visited.Add lowPoint |> ignore
        queue.Enqueue lowPoint
        let mutable cnt = 0
        while queue.Count > 0 do
            cnt <- cnt + 1
            let qit = queue.Dequeue() 
            let adjCells = (qit,rows,cols) |||> adjacent |> Seq.filter (fun cell -> not(heightMap.[fst cell, snd cell] = 9)) |> Seq.toArray
            
            for a in adjCells do
                if not(visited.Contains(a)) then
                    queue.Enqueue a
                    visited.Add a |> ignore
        cnt
    
    heightMap |> lowPoints |> Seq.map basinSize
 
let part1 (heightMap:int[,]) =
    heightMap |> lowPoints 
    |> Seq.map (fun c -> 1+heightMap.[fst c, snd c])
    |> Seq.sum

let part2 (heightMap:int[,]) =
    heightMap
    |> basinsSize
    |> Seq.sortDescending
    |> Seq.take 3
    |> Seq.fold (fun acc it -> acc * it) 1