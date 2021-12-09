module _2021.Day09.Solution

open System.Collections.Generic

let cells rows cols =
    seq{
        for r in 0..rows do
            for c in 0..cols do
                yield (r,c)
    }

let adj cell rows cols =
    let (r,c) = cell
    seq{
        if (r-1) >= 0 then
            yield (r-1,c)
        if (r+1) <= rows then
            yield (r+1,c)
        if (c-1) >= 0 then
            yield (r, c-1)
        if (c+1) <= cols then
            yield (r, c+1)
    }

let isMin (cell:(int * int)) (m:int[,]) (adjs:seq<(int * int)>)=
    let v = m.[fst cell, snd cell]
    adjs
    |> Seq.map (fun adj -> m.[fst adj, snd adj])
    |> Seq.forall (fun adjVal -> v < adjVal)


let basinsSize (matrix:int[,]):seq<int> =
    let rows = matrix.[*,0].Length - 1
    let cols = matrix.[0,*].Length - 1
    
    let size (cell:int * int) (matrix:int[,]) =
        let visited = HashSet()
        let queue = Queue<(int * int)>()
        visited.Add cell |> ignore
        queue.Enqueue cell
        let mutable cnt = 0
        while queue.Count > 0 do
            cnt <- cnt + 1
            let qit = queue.Dequeue() 
            let adjCells = (qit,rows,cols) |||> adj |> Seq.filter (fun cell -> not(matrix.[fst cell, snd cell] = 9)) |> Seq.toArray
            
            for a in adjCells do
                if not(visited.Contains(a)) then
                    queue.Enqueue a
                    visited.Add a |> ignore
        cnt
    
    cells rows cols
    |> Seq.filter (fun cell -> (adj cell rows cols) |> isMin cell matrix)
    |> Seq.filter (fun cell -> not(matrix.[fst cell, snd cell] = 9))
    |> Seq.map (fun cell -> size cell matrix)
    |> Seq.sortDescending

let part1 (matrix:int[,]) =
    let rows = matrix.[*,0].Length - 1
    let cols = matrix.[0,*].Length - 1
    
    cells rows cols
    |> Seq.filter (fun cell -> (adj cell rows cols) |> isMin cell matrix)
    |> Seq.map (fun c -> 1+matrix.[fst c, snd c])
    |> Seq.sum

let part2 (matrix:int[,]) =
    matrix
    |> basinsSize
    |> Seq.take 3
    |> Seq.fold (fun acc it -> acc * it) 1