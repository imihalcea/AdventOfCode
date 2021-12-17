module _2021.Day16.Tests

open System.IO
open Xunit
open Swensen.Unquote
open _2021.Day16.Solution


[<Fact>]
let ``part 1``()=
    let result = File.ReadAllText "Day16/input.txt" |> Solution.part1
    test <@ result = 886 @>

[<Fact>]
let ``part 2``()=
    let result = File.ReadAllText "Day16/input.txt" |> Solution.part2
    test <@ result = 0 @>

[<Fact>]
let ``decode literals``()=
       let result = Solution.decode "D2FE28"
       test <@ result = {version=6; typeId=4; decodedValue=2021; subPackets=[]}  @>
       

[<Fact>]       
let ``decode operators 0``()=
       let result = Solution.decode "38006F45291200"
       test <@ result = {
                         version=1
                         typeId=6
                         decodedValue=0
                         subPackets=[
                                   {version=6; typeId=4; decodedValue=10; subPackets=[]}
                                   {version=2; typeId=4; decodedValue=20; subPackets=[]}
                                ]
                         }  @>

[<Fact>]       
let ``decode operators 1``()=
       let result = Solution.decode "EE00D40C823060"
       test <@ result = {
                         version=7
                         typeId=3
                         decodedValue=0
                         subPackets=[
                                   {version=2; typeId=4; decodedValue=1; subPackets=[]}
                                   {version=4; typeId=4; decodedValue=2; subPackets=[]}
                                   {version=1; typeId=4; decodedValue=3; subPackets=[]}
                                ]
                         }  @>