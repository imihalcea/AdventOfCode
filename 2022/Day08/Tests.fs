module _2022.Day08.Tests

open System.IO
open Solution
open Xunit
open Swensen.Unquote

[<Fact>]
let ``collinear trees``()=
    let result =
        File.ReadAllLines("Day08/ex.txt")
        |> parseInput
        |> sameRowOrColumn (2,2)
    test <@ result = [| [|5;6|]; [|3;2|]; [|5;3|]; [|5;3|]  |] @>

[<Fact>]
let ``visibility``() =
    let m =
        File.ReadAllLines("Day08/ex.txt")
        |> parseInput
    test <@ isVisible m (1,1)  = true @>
    test <@ isVisible m (1,3)  = false @>

[<Fact>]
let ``count visible``() =
    let result =
        File.ReadAllLines("Day08/ex.txt")
        |> parseInput
        |> countVisible 
    test <@ result = 21 @>

[<Fact>]
let ``part1``() =
    let result =
        File.ReadAllLines("Day08/input.txt")
        |> parseInput
        |> countVisible 
    test <@ result = 1713 @>

[<Fact>]
let ``take Until``()=
    test <@ (takeUntil (fun x -> x>=3) [|6;5|]) = [|6|] @>


[<Fact>]
let ``compute scenic score``()=
    let m =
        File.ReadAllLines("Day08/ex.txt")
        |> parseInput
    test <@ computeScore m (1,2)  = 4 @>
    test <@ computeScore m (3,2)  = 8 @>
    test <@ computeScore m (2,2)  = 1 @>


[<Fact>]
let ``part2``() =
    let result =
        File.ReadAllLines("Day08/input.txt")
        |> parseInput
        |> bestScore 
    test <@ result = 268464 @>