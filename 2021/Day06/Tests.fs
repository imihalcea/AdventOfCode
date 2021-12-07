module _2021.Day06.Tests

open System
open System.IO
open Xunit
open Swensen.Unquote

let parseInput filePath =
    File.ReadLines(filePath)
    |> Seq.map (fun s -> s.Split(","))
    |> Seq.concat
    |> Seq.map Int32.Parse

[<Theory>]
[<InlineData(18,26)>]
[<InlineData(80,5934)>]
[<InlineData(256,26984457539L)>]
let ``example``(days:int, expected:int64)=
   let result = parseInput "Day06/example.txt"
                |> Solution.part1 days
   test <@ result = expected  @>

[<Fact>]
let ``part 1``() =
   let result = parseInput "Day06/input.txt"
                |> Solution.part1 80
   test <@ result = 390923L  @>

[<Fact>]
let ``part 2``() =
   let result = parseInput "Day06/input.txt"
                |> Solution.part1 256
   test <@ result = 1749945484935L  @>