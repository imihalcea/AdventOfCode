module _2021.Day09.Tests

open System
open System.IO
open Swensen.Unquote
open Xunit


let parseInput filePath =
    File.ReadLines filePath
    |> Seq.map (fun l -> (l |> Seq.map (fun c -> Convert.ToInt32(c.ToString()))))
    |> array2D

[<Fact>]
let ``example part1``()=
   let result = parseInput "Day09/example.txt"
                |> Solution.part1 
   test <@ result = 15  @>

[<Fact>]
let ``example part2``()=
   let result = parseInput "Day09/example.txt"
                |> Solution.part2 
   test <@ result = 1134  @>

[<Fact>]
let ``part1``()=
   let result = parseInput "Day09/input.txt"
                |> Solution.part1 
   test <@ result = 512  @>

[<Fact>]
let ``part2``()=
   let result = parseInput "Day09/input.txt"
                |> Solution.part2 
   test <@ result = 1600104  @>  