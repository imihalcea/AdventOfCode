// Learn more about F# at http://fsharp.org
module Aoc2019

open System
open System.IO
open System.Diagnostics
open FSharp.Data
open Day01
open Day02
open Day04
open Day06

type SpaceModules = CsvProvider<"./data/d01sample.csv", HasHeaders=true>

let private decode (src:string) =
    src.Split(",")
    |> Array.map Int32.Parse

let private encode (sep:string)(codes:int[]) =
    codes 
    |> Array.map (fun c-> c.ToString())
    |> Array.reduce (fun agg n -> sprintf "%s%s%s" agg sep n)


let Day01Program (path:string) =
    let inputData = SpaceModules.Load path 
    inputData.Rows 
        |> Seq.map (fun row -> {Mass=row.Mass})
        |> FuelForAllSpaceModules

let Day02Program (path:string) = 
    File.ReadAllText(path)
    |>decode
    |>RunProgram
    |>encode ","

let Day06Part1Program (path:string)=
    File.ReadAllLines(path) |> Orbit.part1
 

let Day06Part2Program (path:string):int=
    File.ReadAllLines(path) |> Orbit.part2

let Day08Part1Program (path:string):int=
    File.ReadAllText(path) |> Day08.part1 25 6

let Day08Part2Program (path:string)=
    let image = File.ReadAllText(path) |> Day08.part2 25 6
    encode "" image.Pixels

let Day10Part1Program (path:string)=
    File.ReadAllLines(path) |> Day10.part1

let Day10Part2Program (path:string)=
    File.ReadAllLines(path) |> Day10.part2

[<EntryPoint>]
let main argv =
    let sw = Stopwatch()
    sw.Start()
    match argv.[0] with
    |"d1" -> argv.[1] |> Day01Program |> sprintf "%A"
    |"d2" -> argv.[1] |> Day02Program |> sprintf "%A"
    |"d4.1" -> (264793,803935) ||> HowManyDifferentPasswordsPart1 |> sprintf "%A"
    |"d4.2" -> (264793,803935) ||> HowManyDifferentPasswordsPart2 |> sprintf "%A"
    |"d6.1" ->  argv.[1] |> Day06Part1Program |> sprintf "%A"
    |"d6.2" ->  argv.[1] |> Day06Part2Program |> sprintf "%A"
    |"d8.1" ->  argv.[1] |> Day08Part1Program |> sprintf "%A"
    |"d8.2" ->  argv.[1] |> Day08Part2Program |> sprintf "%A"
    |"d10.1" -> argv.[1] |> Day10Part1Program |> sprintf "%A"
    |"d10.2" -> argv.[1] |> Day10Part2Program |> sprintf "%A"
    |_ -> "0"
    |> printfn "%s"
    sw.Stop()
    printfn "in %i ms" sw.ElapsedMilliseconds
    0 // return an integer exit code