module _2022.Day07.Tests

open System.IO
open Xunit
open Swensen.Unquote
open Solution

[<Fact>]
let ``parse input``() =
    test <@ parseLine "$ ls" = LS @>
    test <@ parseLine "$ cd .." = CD("..") @>
    test <@ parseLine "$ cd mjpbhjm" = CD("mjpbhjm") @>
    test <@ parseLine "dir hqnj" = DIR("hqnj") @>
    test <@ parseLine "18783 lmw.rwr" = FILE(18783,"lmw.rwr") @>
    test <@ parseLine "18783 lmw" = FILE(18783,"lmw") @>

[<Fact>]
let ``exemple 1``() =
    let r = File.ReadAllLines("Day07/ex1.txt") |> part1
    test <@ r = 95437 @>

[<Fact>]
let ``input 1``() =
    let r = File.ReadAllLines("Day07/input.txt") |> part1
    test <@ r = 1444896 @>

[<Fact>]
let ``input 2``() =
    let r = File.ReadAllLines("Day07/input.txt") |> part2
    test <@ r = 1444896 @>   