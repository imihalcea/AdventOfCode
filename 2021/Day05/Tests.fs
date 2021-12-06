module _2021.Day05.Tests

open System
open System.IO
open Xunit
open Solution
open Swensen.Unquote

let parseInput filePath =
    File.ReadLines(filePath)
    |> Seq.map (fun s -> s.Replace(" -> ",","))
    |> Seq.map (fun s -> s.Split(","))
    |> Seq.map (fun s -> {x1=Int32.Parse(s.[0]);y1=Int32.Parse(s.[1]); x2 = Int32.Parse(s.[2]); y2=Int32.Parse(s.[3])})

[<Fact>]
let ``diag 1``() =
    let points =Solution.toPoints {x1=1;y1=1;x2=3;y2=3}
    test <@ points |> Seq.toList = [{x=1;y=1};{x=2;y=2};{x=3;y=3}]  @>

[<Fact>]
let ``diag 2``() =
    let points =Solution.toPoints {x1=3;y1=3;x2=1;y2=1}
    test <@ points |> Seq.toList = [{x=3;y=3};{x=2;y=2};{x=1;y=1}]  @>

[<Fact>]
let ``diag 3``() =
    let points =Solution.toPoints {x1=9;y1=7;x2=7;y2=9}
    test <@ points |> Seq.toList = [{x=9;y=7};{x=8;y=8};{x=7;y=9}]  @>

[<Fact>]
let ``Part 1`` () =
    let result =
        parseInput "Day05/input.txt"
        |> Solution.part1
    Assert.Equal(6548, result)
    
[<Fact>]
let ``Part 2`` () =
    let result =
        parseInput "Day05/input.txt"
        |> Solution.part2
    Assert.Equal(19663, result)