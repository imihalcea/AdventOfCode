module _2022.Day03.Solution

open System
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

let itemMap =
    let lc = [97..122]
             |> List.map (fun it -> (char(it), it-96))
    let uc = [65..90]
             |> List.map ((fun it -> (char(it), it-64+26)))
    Map(lc @ uc)

let checkRucksack (content:String)=
    let mid = content.Length/2
    let small = content[..mid-1].ToCharArray()
    let big = content[mid..].ToCharArray()
    Set.intersect (Set.ofArray small) (Set.ofArray big)
    |> Set.map (fun it -> itemMap[it])
    |> Set.toList

let assignGroup (rucksacks:string[]) =
    let common = rucksacks
                |> Array.map (fun it -> it.ToCharArray()|> Set.ofArray)
                |> Set.intersectMany
                |> Set.toList |> List.head
    itemMap[common]
    
    

let part1 (data:String) =
    data.Split("\n", StringSplitOptions.TrimEntries)
    |> Array.map checkRucksack
    |> Array.map List.sum
    |> Array.sum

let part2 (data:String) =
    let rucksacks = data.Split("\n", StringSplitOptions.TrimEntries)
    rucksacks
    |> Array.splitInto (rucksacks.Length/3)
    |> Array.map assignGroup
    |> Array.sum   