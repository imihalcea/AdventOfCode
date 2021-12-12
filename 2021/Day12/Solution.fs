module _2021.Day12.Solution

open Microsoft.FSharp.Collections
open _2021.Day12


let atMostOnce inPath _ =
    not(inPath)

let singleSmallCaveTwice inPath twice =
    not(inPath) || inPath && not(twice)


let part1 (g:Graph) =
    findPaths g atMostOnce   
    |> Seq.length

let part2 (g:Graph) =
    findPaths g singleSmallCaveTwice   
    |> Seq.length    