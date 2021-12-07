module _2021.Day01.Solution

let part1 (ints:seq<int16>) =
    ints
    |> Seq.pairwise
    |> Seq.map (fun (x,y) -> x - y)
    |> Seq.filter (fun x -> x < 0s)
    |> Seq.length
    
let part2 (ints:seq<int16>) =
    ints
    |> Seq.windowed 3
    |> Seq.map Array.sum
    |> part1
    