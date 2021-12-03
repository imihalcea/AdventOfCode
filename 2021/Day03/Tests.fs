module _2021.Day03.Tests

open System
open System.IO
open Xunit

let parseLine (line:string) =
    line
    |> Seq.map (fun c -> match c with
                         |'1' -> true
                         |_ -> false)


[<Fact>]
let ``Example Part 1`` () =
    let result =
        File.ReadLines("Day03/example.txt")
        |> Seq.map parseLine
        |> array2D
        |> Solution.part1
    Assert.Equal(result, 1660158)

[<Fact>]
let ``Part 1`` () =
    let result =
        File.ReadLines("Day03/input.txt")
        |> Seq.map parseLine
        |> array2D
        |> Solution.part1
    Assert.Equal(result, 3985686)