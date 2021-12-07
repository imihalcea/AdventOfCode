module _2021.Day01.TestsDay1

open System
open System.IO
open Xunit

[<Fact>]
let ``Part 1`` () =
    let result =
        File.ReadLines("Day01/input.txt")
        |> Seq.map Convert.ToInt16
        |> Solution.part1
    Assert.Equal(result, 1791)
    
[<Fact>]
let ``Part 2`` () =
    let result =
        File.ReadLines("Day01/input.txt")
        |> Seq.map Convert.ToInt16
        |> Solution.part2
    Assert.Equal(result, 1822)