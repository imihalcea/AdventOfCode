module _2021.Day06.Tests

open System
open System.IO
open Xunit
open Swensen.Unquote

let parseInput filePath =
    let fish = Array.create 8 0
    File.ReadLines(filePath)
    |> Seq.map (fun s -> s.Split(","))
    |> Seq.concat
    |> Seq.map Int32.Parse
    |> Seq.groupBy id
    |> Seq.fold (fun m x -> Map.add (fst x) ((snd x)|> Seq.length) m) Map.empty
    |> Map.iter (fun k v -> fish.[k]<-v)
    fish

[<Fact>]
let ``example part 1``()=
   let result = parseInput "Day06/example.txt"
                |> Solution.part1 18
   test <@ result = 26  @>               