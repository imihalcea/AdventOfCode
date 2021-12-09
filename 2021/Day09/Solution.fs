module _2021.Day09.Solution

open System.Collections.Generic

let mapSize (m:int[,])= (m.[*,0].Length - 1, m.[0,*].Length - 1)

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

let lowPoints (map:int[,]) =
    let (rows,cols) = mapSize map
    points rows cols
    |> Seq.filter (fun point ->
                                (point,rows,cols)|||> adjacent 
                                |> Seq.forall (fun adj -> map.[fst point, snd point] < map.[fst adj, snd adj])
                   )

let basinsSizes (map:int[,]):seq<int> =
    let (rows,cols) = mapSize map 
    let visited = HashSet()
    
    let rec basinSize (cnt:int) (lowPoint:int * int) =
        visited.Add lowPoint |> ignore
        (lowPoint,rows,cols)|||> adjacent
        |> Seq.filter (fun p -> not(visited.Contains p) && map.[fst p, snd p] <> 9)
        |> Seq.fold (fun cnt -> basinSize (cnt+1)) cnt
    
    map |> lowPoints |> Seq.map (basinSize 1)
 
let part1 (map:int[,]) =
    map |> lowPoints 
    |> Seq.map (fun c -> 1+map.[fst c, snd c])
    |> Seq.sum

let part2 (map:int[,]) =
    map
    |> basinsSizes
    |> Seq.sortDescending
    |> Seq.take 3
    |> Seq.fold (fun acc it -> acc * it) 1