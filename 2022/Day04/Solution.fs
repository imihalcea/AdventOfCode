module _2022.Day04.Solution

open System


let parseInterval (str:string) =
    let ints = str.Split("-")
               |> Array.map Convert.ToInt16
    (ints[0], ints[1])           
    

let parseInput (data:String) =
    data.Split("\n", StringSplitOptions.TrimEntries)
    |> Array.map (fun it -> it.Split(","))
    |> Array.map (Array.map parseInterval)
    |> Array.map (fun it -> (it[0], it[1]))
    

let fullyContains (inter1:int16*int16) (inter2:int16*int16) =
    let (start1, end1) = inter1
    let (start2, end2) = inter2
    (start2 >= start1 && end2 <= end1)  || (start1 >= start2 && end1 <= end2)

let overlaps (inter1:int16*int16) (inter2:int16*int16) =
    let (start1, end1) = inter1
    let (start2, end2) = inter2
    (start1 <= end2 && end1 >= start2)

let part1 data =
    parseInput data
    |> Array.map (fun it -> fullyContains (fst it) (snd it))
    |> Array.filter (fun it -> it = true)
    |> Array.length

let part2 data =
    parseInput data
    |> Array.map (fun it -> overlaps (fst it) (snd it))
    |> Array.filter (fun it -> it = true)
    |> Array.length