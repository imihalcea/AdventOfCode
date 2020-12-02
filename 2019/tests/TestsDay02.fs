module TestsDay02

open System
open Xunit
open Day02

let private decode (src:string) =
    src.Split(",")
    |> Array.map Int32.Parse

let private encode (codes:int[])=
    codes 
    |> Array.map (fun c-> c.ToString())
    |> Array.reduce (fun agg n -> sprintf "%s,%s" agg n)

[<Theory>]
[<InlineData("1,0,0,0,99","2,0,0,0,99")>]
[<InlineData("2,3,0,3,99","2,3,0,6,99")>]
[<InlineData("2,4,4,5,99,0","2,4,4,5,99,9801")>]
[<InlineData("1,1,1,4,99,5,6,0,99","30,1,1,4,2,5,6,0,99")>]
let ``should execute programs`` source expectedOutput=
    let result = source |> decode |> RunProgram |> encode
    Assert.Equal(expectedOutput, result)
    