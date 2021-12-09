module _2021.Day02.Tests

open System
open System.IO
open Xunit
open _2021.Day02.Solution

let parseLine (line:string) =
    let parts = line.Split(' ')
    let dir = match parts.[0] with
              |"forward" -> Dir.Forward
              |"up" -> Dir.Up
              |"down" -> Dir.Down
              |_ -> failwith "not supported"
    let value = Convert.ToInt32(parts.[1])
    Command(dir, value)

[<Fact>]
let ``Part 1`` () =
    let result =
        File.ReadLines("Day02/input.txt")
        |> Seq.map parseLine
        |> Solution.part1
    Assert.Equal(1660158, result)


[<Fact>]
let ``Part 2`` () =
    let result =
        File.ReadLines("Day02/input.txt")
        |> Seq.map parseLine
        |> Solution.part2
    Assert.Equal(1604592846,result)