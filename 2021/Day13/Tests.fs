module _2021.Day13.Tests

open System
open System.IO
open Swensen.Unquote
open Xunit
open _2021.Day13.Solution


let parseInput path =
    File.ReadLines path
    |> Seq.map (fun s -> s.Split(',', StringSplitOptions.TrimEntries))
    |> Seq.map (fun s -> {x=Int32.Parse(s.[0]); y = Int32.Parse(s.[1])})
    |> Set.ofSeq

[<Fact>]
let rec ``part 1 ``()=
    let points = parseInput "Day13/input.txt"
    test <@ Solution.part1 points = 638 @>
    

[<Fact>]
let rec ``part 2 ``()=
    let points = parseInput "Day13/input.txt"
    test <@ Solution.part2 points = 0 @>



[<Fact>]
let ``fold example``()=
    let points = Set.ofList [
        {x=3; y=0}; {x=6; y=0}; {x=9;y=0}
        {x=4; y=1}
        {x=0; y=3}
        {x=3; y=4}; {x=8; y=4}; {x=10; y=4}
        {x=1;y=10}; {x=6;y=10}; {x=8;y=10}; {x=9;y=10}
        {x=4; y=11}; 
        {x=6; y=12};{x=10; y=12}
        {x=0; y=13}
        {x=0;y=14}; {x=2; y=14}
    ]  
    
    let resultsV = Set.map (translateY 7) points
                  |> Set.filter (fun p -> p.IsSome)
                  |> Set.map (fun p -> p.Value)
    
    let expectedV = Set.ofList [
        {x=0;y=0};{x=2; y=0}; {x=3; y=0}; {x=6; y=0}; {x=9; y=0}
        {x=0; y=1}; {x=4; y=1}
        {x=6;y=2}; {x=10;y=2}
        {x=0;y=3}; {x=4; y=3}
        {x=1;y=4}; {x=3; y=4}; {x=6; y=4}; {x=8; y=4}; {x=9; y=4}; {x=10; y=4}
    ]
    
    test<@ resultsV - expectedV = Set.empty  @>
    
    let resultsH = Set.map (translateX 5) expectedV
                  |> Set.filter (fun p -> p.IsSome)
                  |> Set.map (fun p -> p.Value)
    
    let expectedH = Set.ofList [
        {x=0;y=0}; {x=1; y=0}; {x=2; y=0}; {x=3; y=0}; {x=4; y=0};
        {x=0; y=1}; {x=4; y=1}
        {x=0;y=2}; {x=4;y=2}
        {x=0;y=3}; {x=4; y=3}
        {x=0;y=4}; {x=1; y=4}; {x=2; y=4}; {x=3; y=4}; {x=4; y=4};
    ]
    
    test<@ resultsH - expectedH = Set.empty  @>