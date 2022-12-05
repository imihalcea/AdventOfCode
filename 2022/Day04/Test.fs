module _2022.Day04.Test

open System.IO
open Xunit
open Swensen.Unquote


[<Fact>]
let ``fully contains``()=
    test <@ Solution.fullyContains (6s,6s) (4s,6s) = true @>
    test <@ Solution.fullyContains (2s,8s) (3s,7s) = true @>
    test <@ Solution.fullyContains (2s,4s) (6s,8s) = false @>
    test <@ Solution.fullyContains (2s,4s) (3s,5s) = false @>

[<Fact>]
let ``overlaps``()=
    test <@ Solution.overlaps (6s,6s) (4s,6s) = true @>
    test <@ Solution.overlaps (2s,8s) (3s,7s) = true @>
    test <@ Solution.overlaps (2s,4s) (6s,8s) = false @>
    test <@ Solution.overlaps (2s,4s) (3s,5s) = true @>
    test <@ Solution.overlaps (2s,4s) (5s,7s) = false @>
    test <@ Solution.overlaps (2s,4s) (0s,3s) = true @>
    test <@ Solution.overlaps (0s,3s) (2s,4s) = true @>
    

[<Fact>]
let part1()=
    let r = File.ReadAllText("Day04/input.txt") |> Solution.part1
    test <@ r = 464 @>

[<Fact>]
let part2()=
    let r = File.ReadAllText("Day04/input.txt") |> Solution.part2
    test <@ r = 770 @>
    