module _2021.Day12.Solution

open Microsoft.FSharp.Collections
open _2021.Day12


let smallCaveatMostOnce n inPath twice =
    match n with
    |Big _ -> (true, twice)
    |Small _ -> (not(inPath),twice)
    |Start _ -> (not(inPath),twice)
    |_ -> failwith "Not supported"

let singleSmallCaveTwice n inPath twice =
    match n with
    |Small _ -> (not(inPath) || inPath && not(twice),inPath || twice)
    |Big _ -> (true, twice)
    |Start _ -> (not(inPath),twice)
    | _-> failwith "Not Supported"


let part1 (g:Graph) =
    countPaths g smallCaveatMostOnce   
    //|> Seq.length

let part2 (g:Graph) =
    countPaths g singleSmallCaveTwice   
    //|> Seq.length    