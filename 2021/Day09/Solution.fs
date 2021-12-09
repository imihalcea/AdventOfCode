module _2021.Day09.Solution

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
    |> Seq.map (fun adj ->
        System.Diagnostics.Trace.WriteLine $"%i{fst adj}, %i{snd adj}"
        m.[fst adj, snd adj])
    |> Seq.forall (fun adjVal -> v < adjVal)
    

let part1 (matrix:int[,]) =
    let rows = matrix.[*,0].Length - 1
    let cols = matrix.[0,*].Length - 1
    
    cells rows cols
    |> Seq.filter (fun cell -> (adj cell rows cols) |> isMin cell matrix)
    |> Seq.map (fun c -> 1+matrix.[fst c, snd c])
    |> Seq.sum
