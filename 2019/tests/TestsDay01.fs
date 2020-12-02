module TestsDay01

open System
open Xunit
open Day01


[<Theory>]
[<InlineData(12,2)>]
[<InlineData(14,2)>]
[<InlineData(1969,966)>]
[<InlineData(100756,50346)>]
let ``compute module fuel`` mass expectedFuel =
    let result = FuelForSpaceModule {Mass=mass}
    Assert.Equal (expectedFuel,result)

[<Fact>]
let ``compute fuel for many modules``() = 
    let modulesMasses = [12;14;1969;100756] |> List.map (fun m -> {Mass=m})
    let result = FuelForAllSpaceModules modulesMasses
    Assert.Equal((2+2+966+50346),result)