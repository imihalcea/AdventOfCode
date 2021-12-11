module _2021.Day11.Tests

open System
open System.IO
open Swensen.Unquote
open Xunit

let parseInput filePath =
    File.ReadLines filePath
    |> Seq.map (fun l -> (l |> Seq.map (fun c -> Convert.ToInt32(c.ToString()))))
    |> array2D

[<Fact>]    
let ``example part1``()=
   let result = parseInput "Day11/example.txt"
                |> Solution.part1 
   test <@ result = 1656  @>
   

[<Fact>]    
let ``part1``()=
   let result = parseInput "Day11/input.txt"
                |> Solution.part1 
   test <@ result = 1603  @>
   
[<Fact>]    
let ``part2``()=
   let result = parseInput "Day11/input.txt"
                |> Solution.part2 
   test <@ result = 222  @>