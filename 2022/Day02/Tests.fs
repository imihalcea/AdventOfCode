module _2022.Day02.TestsDay1

open System
open System.IO
open Xunit
open Swensen.Unquote
open Solution

[<Fact>]
let ``example1``()=
    let r = [|(Hand.Rock, Hand.Paper); (Hand.Paper, Hand.Rock); (Hand.Scissors, Hand.Scissors)  |]
            |> Array.map score
            |> Array.sum
    test <@ r = 15 @>
    

[<Fact>]
let ``Part 1`` () =
    let data =File.ReadAllText("Day02/input.txt")
    let result = part1 data
    test  <@ result = 12740  @>

[<Fact>]
let ``Part 2`` () =
    let data =File.ReadAllText("Day02/input.txt")
    let result = part2 data
    test  <@ result = 11980  @>
    