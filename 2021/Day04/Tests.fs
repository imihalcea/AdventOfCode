module _2021.Day04.Tests

open System
open System.IO
open Xunit


let parseNumbers (line:string) =
    line.Split(',')
    |> Array.map (fun s -> Int32.Parse(s.Trim()))

let parseBoardLine (line : string) =
    line.Split(' ')
    |> Array.filter (fun s -> not(String.IsNullOrWhiteSpace(s)))  
    |> Array.map (fun s -> (Int32.Parse(s.Trim()),false))


let parseBoard (lines : string[]) =
    lines
    |> Array.skip 1
    |> Array.map parseBoardLine
    |> array2D

let parseInput path =
    let allLines = File.ReadAllLines(path)
    let draw = parseNumbers allLines.[0]
    let boards =
        allLines
        |> Array.skip 1
        |> Array.chunkBySize 6
        |> Array.map parseBoard
    (draw, boards)

let parseInput2 path =
    let allLines = File.ReadAllLines(path)
    let draw = parseNumbers allLines.[0]
    let boards =
        allLines
        |> Array.skip 1
        |> Array.chunkBySize 6
        |> Array.map (fun x -> parseBoard x |> Some) 
    (draw, boards)

[<Fact>]
let ``Example Part 1`` () =
    let result =
        parseInput("Day04/example.txt")
        ||> Solution.part1
    Assert.Equal(4512, result)


[<Fact>]
let ``Example Part 2`` () =
    let result =
        parseInput("Day04/example.txt")
        ||> Solution.part2
    Assert.Equal(1924, result)

[<Fact>]
let ``Part 1`` () =
    let result =
        parseInput("Day04/input.txt")
        ||> Solution.part1
    Assert.Equal(38594, result)