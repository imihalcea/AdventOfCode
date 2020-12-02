module TestsDay04Part1

open System
open Xunit
open Day04


[<Theory>]
[<InlineData(111111,true)>]
[<InlineData(223450,false)>]
[<InlineData(123789,false)>]
[<InlineData(123455,true)>]
let ``part1`` password expectedOutput=
    let result = part1Condition |> CheckPassword <| password
    Assert.Equal(expectedOutput, result)


[<Theory>]
[<InlineData(112233,true)>]
[<InlineData(123444,false)>]
[<InlineData(111111,false)>]
[<InlineData(124433,false)>]
[<InlineData(123455,true)>]
[<InlineData(111122,true)>]
[<InlineData(111225,true)>]
[<InlineData(122225,false)>]
let ``part2`` password expectedOutput=
    let result = part2Condition |> CheckPassword <| password
    Assert.Equal(expectedOutput, result)