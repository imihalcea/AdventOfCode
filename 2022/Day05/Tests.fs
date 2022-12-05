module _2022.Day05.Tests

open System
open System.IO
open Xunit
open Swensen.Unquote

[<Fact>]
let ``parse line``() =
    test <@ Solution.parseLine "move 10 from 5 to 3" = {qty=10; source=5; target=3}  @>

[<Fact>]
let ``example 1``()=
    let movesData = """move 1 from 2 to 1
                   move 3 from 1 to 3
                   move 2 from 2 to 1
                   move 1 from 1 to 2"""
    let moves = movesData.Split("\n", StringSplitOptions.TrimEntries)
                |> Array.map Solution.parseLine
    
    let initialPiles = Map[
        (1, Solution.stackOf ["Z";"N"])
        (2, Solution.stackOf ["M"; "C"; "D"])
        (3, Solution.stackOf ["P"])
    ]
    test <@ (Solution.part1 initialPiles moves) = "CMZ" @>

[<Fact>]
let ``part 1``()=
    let moves = File.ReadAllLines("Day05/input.txt")
                |> Array.map Solution.parseLine
    
    let initialPiles = Map[
        (1, Solution.stackOf ["Z";"J";"N"; "W"; "P"; "S"] )
        (2, Solution.stackOf ["G"; "S"; "T"] )
        (3, Solution.stackOf ["V"; "Q"; "R"; "L"; "H"] )
        (4, Solution.stackOf ["V";"S";"T"; "D"] )
        (5, Solution.stackOf ["Q";"Z";"T";"D";"B";"M";"J"] )
        (6, Solution.stackOf ["M";"W";"T";"J";"D";"C";"Z";"L"] )
        (7, Solution.stackOf ["L";"P";"M";"W";"G";"T";"J"] )
        (8, Solution.stackOf ["N";"G";"M";"T";"B";"F";"Q";"H"] )
        (9, Solution.stackOf ["R";"D";"G";"C";"P";"B";"Q";"W"] )
    ]
    test <@ (Solution.part1 initialPiles moves) = "MQTPGLLDN" @>

[<Fact>]
let ``example 2``()=
    let movesData = """move 1 from 2 to 1
                   move 3 from 1 to 3
                   move 2 from 2 to 1
                   move 1 from 1 to 2"""
    let moves = movesData.Split("\n", StringSplitOptions.TrimEntries)
                |> Array.map Solution.parseLine
    
    let initialPiles = Map[
        (1, Solution.stackOf ["Z";"N"])
        (2, Solution.stackOf ["M"; "C"; "D"])
        (3, Solution.stackOf ["P"])
    ]
    test <@ (Solution.part2 initialPiles moves) = "MCD" @>

[<Fact>]
let ``part 2``()=
    let moves = File.ReadAllLines("Day05/input.txt")
                |> Array.map Solution.parseLine
    
    let initialPiles = Map[
        (1, Solution.stackOf ["Z";"J";"N"; "W"; "P"; "S"] )
        (2, Solution.stackOf ["G"; "S"; "T"] )
        (3, Solution.stackOf ["V"; "Q"; "R"; "L"; "H"] )
        (4, Solution.stackOf ["V";"S";"T"; "D"] )
        (5, Solution.stackOf ["Q";"Z";"T";"D";"B";"M";"J"] )
        (6, Solution.stackOf ["M";"W";"T";"J";"D";"C";"Z";"L"] )
        (7, Solution.stackOf ["L";"P";"M";"W";"G";"T";"J"] )
        (8, Solution.stackOf ["N";"G";"M";"T";"B";"F";"Q";"H"] )
        (9, Solution.stackOf ["R";"D";"G";"C";"P";"B";"Q";"W"] )
    ]
    test <@ (Solution.part2 initialPiles moves) = "LVZPSTTCZ" @>