module _2021.Day09.Tests

open System
open System.IO
open _2021.Day09.Solution
open Swensen.Unquote
open Xunit


let parseInput filePath =
    File.ReadLines filePath
    |> Seq.map (fun l -> (l |> Seq.map (fun c -> Convert.ToInt32(c.ToString()))))
    |> array2D

[<Fact>]
let ``adj tests``()=
    let r = Solution.adj (0,0) 2 2
    test <@ r |> Seq.toList = [(1,0); (0,1)] @>

[<Fact>]
let ``adj tests 2``()=    
    let r1 = Solution.adj (1,1) 2 2 |> Seq.toList
    test <@ r1 = [(0,1); (2,1); (1,0); (1,2)] @>

[<Fact>]
let ``adj tests 3``()=    
    let r1 = Solution.adj (2,2) 2 2 |> Seq.toList
    test <@ r1 = [(1,2); (2,1)] @>

[<Fact>]
let ``example part1``()=
   let result = parseInput "Day09/example.txt"
                |> Solution.part1 
   test <@ result = 15  @>

[<Fact>]
let ``part1``()=
   let result = parseInput "Day09/input.txt"
                |> Solution.part1 
   test <@ result = 0  @> 