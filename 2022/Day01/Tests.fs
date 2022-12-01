module _2022.Day01.TestsDay1

open System
open System.IO
open Xunit
open Swensen.Unquote

[<Fact>]
let ``Part 1`` () =
    let data =File.ReadAllText("Day01/input.txt")
    let result = Solution.part1 data
    test  <@ result = 69795  @>

[<Fact>]
let ``Part 2`` () =
    let data =File.ReadAllText("Day01/input.txt")
    let result = Solution.part2 data
    test  <@ result = 208437  @>
    