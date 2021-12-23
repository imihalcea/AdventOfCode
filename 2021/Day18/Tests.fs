module _2021.Day18.Tests

open System.IO
open Microsoft.FSharp.Reflection
open Xunit
open Swensen.Unquote
open _2021.Day18.Solution


type TestData() =
    static member ParseCases =
        [ ("[1,2]", Pair(Reg(1), Reg(2)))
          ("[[1,2],3]", Pair(Pair(Reg(1), Reg(2)), Reg(3)))
          ("[1,[2,3]]", Pair(Reg(1), Pair(Reg(2), Reg(3))))
          ("[[1,2],[3,4]]", Pair(Pair(Reg(1), Reg(2)), Pair(Reg(3), Reg(4))))
          ("[[[1,2],[3,4]],5]", Pair(Pair(Pair(Reg(1), Reg(2)), Pair(Reg(3), Reg(4))), Reg(5)))
          ("[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]",
           Pair(
               Pair(Pair(Pair(Reg(6), Reg(6)), Pair(Reg(7), Reg(6))), Pair(Pair(Reg(7), Reg(7)), Pair(Reg(7), Reg(0)))),
               Pair(Pair(Pair(Reg(7), Reg(7)), Pair(Reg(7), Reg(7))), Pair(Pair(Reg(7), Reg(8)), Pair(Reg(9), Reg(9))))
           )) ]
        |> Seq.map FSharpValue.GetTupleFields

    static member ExplodeCases =
        [ "[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]"
          "[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[7,[[8,4],9]]],[1,1]]"
          "[[[[0,7],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[15,[0,13]]],[1,1]]" ]
        |> Seq.map FSharpValue.GetTupleFields

[<Theory; MemberData("ParseCases", MemberType = typeof<TestData>)>]
let ``parse tests`` (input: string, expected: Num) = test <@ fromString input = expected @>


let parseInput (filePath: string) =
    filePath
    |> File.ReadAllLines
    |> Seq.map Solution.fromString


[<Fact>]
let ``part1`` () =
    let input = parseInput "Day18/input.txt"
    test <@ (part1 input) = 4017 @>

[<Fact>]
let ``part1 example``() =
    let input = parseInput "Day18/example.txt"
    test <@ Solution.part1 input = 3488 @>

[<Fact>]
let ``part2`` () =
    let input = parseInput "Day18/input.txt"
    let r = part2 input
    test <@ r = 4583 @>