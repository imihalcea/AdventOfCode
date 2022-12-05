module _2022.Day05.Solution

open System
open System.Text.RegularExpressions
open Microsoft.FSharp.Core
open _2022.Tools


type Pile = int
type Piles = Map<Pile,Stack<string>>

type Move = {qty:int; source:Pile; target:Pile}

let stackOf items =
    items |> List.fold (fun (s:Stack<string>) -> s.Push) (Stack.Empty)

let parseLine line =
    let pattern = Regex(@"^move\s(\d+)\sfrom\s(\d+)\sto\s(\d+)$")
    let m = pattern.Match(line)
    {qty=Convert.ToInt32(m.Groups[1].Value)
     source=Convert.ToInt32(m.Groups[2].Value)
     target = Convert.ToInt32(m.Groups[3].Value)}

let mv (s:Stack<string>) (t:Stack<string>) : Stack<string> list =
    let (v, s') = s.Pop()
    let t' = t.Push(v)
    [s'; t']
    
let mvs (qty:int) (s:Stack<string>) (t:Stack<string>) : Stack<string> list =
    let (v, s') = s.PopMany(qty)
    let t' = t.PushMany(v |> List.rev)
    [s'; t']


let change (nv:Stack<string>) (found:Stack<string> option) =
    match found with
    |Some _ -> Some(nv)
    |None -> None

let play (piles:Piles) (move:Move): Piles =
    [0..move.qty-1]
    |> List.fold (fun p _ -> mv p[0] p[1]) [piles[move.source];piles[move.target]]
    |> List.zip [move.source; move.target]
    |> List.fold (fun ps p -> (ps.Change ((fst p),(change (snd p))))) piles 

let play2 (piles:Piles) (move:Move): Piles =
    mvs move.qty piles[move.source] piles[move.target]
    |> List.zip [move.source; move.target]
    |> List.fold (fun ps p -> (ps.Change ((fst p),(change (snd p))))) piles     

let part1 (piles:Piles) (moves:Move array) =
    moves
    |> Array.fold play piles
    |> Map.toList
    |> List.map (fun it -> (snd it).Peak())
    |> String.Concat

let part2 (piles:Piles) (moves:Move array) =
    moves
    |> Array.fold play2 piles
    |> Map.toList
    |> List.map (fun it -> (snd it).Peak())
    |> String.Concat