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
    Assert.Equal(198, result)


[<Fact>]
let ``Example Part 2`` () =
    let result =
        File.ReadLines("Day03/example.txt")
        |> Seq.map parseLine
        |> array2D
        |> Solution.part2
    Assert.Equal(230, result)


[<Fact>]
let ``Part 1`` () =
    let result =
        File.ReadLines("Day03/input.txt")
        |> Seq.map parseLine
        |> array2D
        |> Solution.part1
    Assert.Equal(3985686, result)
    
    
[<Fact>]
let ``Part 2`` () =
    let result =
        File.ReadLines("Day03/input.txt")
        |> Seq.map parseLine
        |> array2D
        |> Solution.part2
    Assert.Equal(2555739, result)