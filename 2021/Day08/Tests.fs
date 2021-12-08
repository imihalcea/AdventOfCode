module _2021.Day08.Tests

open System
open System.IO
open _2021.Day08.Solution
open Swensen.Unquote
open Xunit

let parseValuePart (str:string) =
   str.Split(" ", StringSplitOptions.TrimEntries)

let parseInput filePath =
    File.ReadLines(filePath)
    |> Seq.map (fun s -> s.Split("|", StringSplitOptions.TrimEntries))
    |> Seq.map (fun s -> {input=(parseValuePart s.[0]); output=(parseValuePart s.[1])})
    |> Seq.toArray
    
[<Fact>]
let ``example part1``()=
   let result = parseInput "Day08/example.txt"
                |> Solution.part1 
   test <@ result = 26  @>


[<Fact>]
let ``example part2 small``()=
   let result = parseInput "Day08/example_small.txt"
                |> Solution.part2 
   test <@ result = 8394  @>



[<Fact>]
let ``example part2``()=
   let result = parseInput "Day08/example.txt"
                |> Solution.part2 
   test <@ result = 61229  @>



[<Fact>]
let ``part 1``()=
   let result = parseInput "Day08/input.txt"
                |> Solution.part1 
   test <@ result = 26  @>

[<Fact>]
let ``part 2``()=
   let result = parseInput "Day08/input.txt"
                |> Solution.part2 
   test <@ result = 961734  @>