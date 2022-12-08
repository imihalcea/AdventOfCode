module _2022.Day08.Solution

open System
open Microsoft.FSharp.Core


let parseInput (lines : string array):int[,]=
    lines |> Array.map (fun line -> line |> Seq.map (fun c -> Convert.ToInt32(c.ToString())))
    |> array2D

let sameRowOrColumn  (tree:int*int) (grid:int[,])=
    let r,c = tree
    [|grid[r,..c-1] |> Array.rev ; grid[r,c+1..]; grid[..r-1,c] |> Array.rev; grid[r+1..,c]|]

let isVisible (grid:int[,]) (tree:int*int)=
    let v = grid[fst(tree),snd(tree)]
    sameRowOrColumn tree grid
    |> Array.map (Array.forall (fun x -> x < v )) 
    |> set |> Set.contains true

let takeUntil (predicate:int -> bool) (s:int seq):int array =
    let mutable hit = false
    seq{
        for x in s do
            if (not hit) && predicate(x) then
                hit <- true
                yield x
            else if not hit then
                yield x     
    } |> Array.ofSeq

let computeScore (grid:int[,]) (tree:int*int)=
    let v = grid[fst(tree),snd(tree)]
    sameRowOrColumn tree grid
    |> Array.map (takeUntil (fun x -> x >= v ))
    |> Array.map Array.length
    |> Array.fold (*) 1

let points rows cols =
    seq{
        for r in 0..rows do
            for c in 0..cols do
                yield (r,c)
    }

let countVisible (grid:int[,]) =
    points (grid.[*,0].Length - 1) (grid.[0,*].Length - 1)
    |> Seq.map (isVisible grid)
    |> Seq.filter id
    |> Seq.length

let bestScore (grid:int[,]) =
    points (grid.[*,0].Length - 1) (grid.[0,*].Length - 1)
    |> Seq.map (computeScore grid)
    |> Seq.sortDescending
    |> Seq.head
    