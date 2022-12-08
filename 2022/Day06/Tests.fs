module _2022.Day06.Tests

open System.IO
open Xunit
open Swensen.Unquote
open Solution


[<Fact>]
let ``examples 1``() =
    test <@ (findStartPacket 4 "bvwbjplbgvbhsrlpgdmjqwftvncz") = 5 @>
    test <@ (findStartPacket 4 "nppdvjthqldpwncqszvftbrmjlhg") = 6 @>
    test <@ (findStartPacket 4 "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg") = 10 @>
    test <@ (findStartPacket 4 "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw") = 11 @>

[<Fact>]
let ``part 1``() =
    let result = File.ReadAllText("Day06/input.txt") |> findStartPacket 4
    test <@ result = 1582 @>
    
[<Fact>]
let ``part 2``() =
    let result = File.ReadAllText("Day06/input.txt") |> findStartPacket 14
    test <@ result = 3588 @>