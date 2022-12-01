module _2022.Day01.Solution

open System

let part1 (data:string) =
    data.Split("\n\n", StringSplitOptions.TrimEntries)
    |> Array.map (fun cals -> cals.Split("\n", StringSplitOptions.TrimEntries) |> Array.map Convert.ToInt32) 
    |> Array.map Array.sum
    |> Array.max
    
let part2 (data:string) =
    data.Split("\n\n", StringSplitOptions.TrimEntries)
    |> Array.map (fun cals -> cals.Split("\n", StringSplitOptions.TrimEntries) |> Array.map Convert.ToInt32) 
    |> Array.map Array.sum
    |> Array.sortDescending
    |> Array.take 3
    |> Array.sum