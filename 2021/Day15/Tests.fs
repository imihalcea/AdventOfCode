module _2021.Day15.Tests

open System
open System.IO
open Xunit
open Swensen.Unquote

let parseInput filePath =
    File.ReadLines filePath
    |> Seq.map (fun l -> (l |> Seq.map (fun c -> Convert.ToInt32(c.ToString()))))
    |> array2D

[<Fact>]
let ``example part1``()=
       let result = parseInput "Day15/example.txt"
                    |> Solution.part1
       test <@ result = 40  @>

[<Fact>]
let ``part1``()=
    let result = parseInput "Day15/input.txt"
                    |> Solution.part1
    test <@ result = 508  @>

[<Fact>]
let ``augmentation test``()=
    let m = parseInput "Day15/example.txt"
    let r = Solution.augment m
    test <@ r[0, *] =  [|1;1;6;3;7;5;1;7;4;2;2;2;7;4;8;6;2;8;5;3;3;3;8;5;9;7;3;9;6;4;4;4;9;6;1;8;4;1;7;5;5;5;1;7;2;9;5;2;8;6 |] @>
    test <@ r[10, *] = [|2;2;7;4;8;6;2;8;5;3;3;3;8;5;9;7;3;9;6;4;4;4;9;6;1;8;4;1;7;5;5;5;1;7;2;9;5;2;8;6;6;6;2;8;3;1;6;3;9;7 |] @>
    
[<Fact>]   
let ``example part2``()=
       let result = parseInput "Day15/example.txt"
                    |> Solution.part2
       test <@ result = 315  @>

[<Fact>]
let ``part 2``() =
       let result = parseInput "Day15/input.txt"
                    |> Solution.part2
       test <@ result = 2872  @>