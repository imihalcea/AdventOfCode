module _2021.Day07.Tests

open System
open System.IO
open Swensen.Unquote
open Xunit

let parseInput filePath =
    File.ReadLines(filePath)
    |> Seq.map (fun s -> s.Split(",", StringSplitOptions.RemoveEmptyEntries))
    |> Seq.concat
    |> Seq.map Int32.Parse
    |> Seq.toArray

[<Fact>]
let ``part 1``() =
   let result = parseInput "Day07/input.txt"
                |> Solution.part1
   test <@ result = 340052  @>

[<Fact>]
let ``part 2``() =
   let result = parseInput "Day07/input.txt"
                |> Solution.part2
   test <@ result = 92948968  @>