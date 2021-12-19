module _2021.Day17.Tests

open Xunit
open Swensen.Unquote
open Solution

[<Fact>]
let ``part1``()=
    let x = Solution.part1 (Target(153, -75, 199, -114))
    test <@ x=6441 @>

[<Fact>]
let ``part2``()=
    let x = Solution.part2 (Target(153, -75, 199, -114))
    test <@ x=3186 @>

[<Fact>]
let ``example part1``()=
    let x = Solution.part1 (Target(20, -5, 30, -10)) 
    test <@ x=45 @>

[<Fact>]
let ``example part2``()=
    let x = Solution.part2 (Target(20, -5, 30, -10))
    test <@ x=112 @>