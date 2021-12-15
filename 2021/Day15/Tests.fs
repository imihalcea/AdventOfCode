module _2021.Day15.Tests

open System
open System.IO
open Xunit
open Swensen.Unquote
open Xunit.Abstractions
let parseInput filePath =
    File.ReadLines filePath
    |> Seq.map (fun l -> (l |> Seq.map (fun c -> Convert.ToInt32(c.ToString()))))
    |> array2D

type MyTests(output:ITestOutputHelper) =
    [<Fact>]
    member _.``example part1``()=
       let result = parseInput "Day15/example.txt"
                    |> Solution.part1 output.WriteLine
       test <@ result = 15  @>