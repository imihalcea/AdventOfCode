module _2022.Day08.Solution

open System
open Microsoft.FSharp.Core


let parseInput (lines : string array):int[,]=
    lines |> Array.map (fun line -> line |> Seq.map (fun c -> Convert.ToInt32(c.ToString())))
    |> array2D

let collinearTrees  (t:int*int) (m:int[,])=
    let r,c = t
    [|m[r,..c-1] |> Array.rev ; m[r,c+1..]; m[..r-1,c] |> Array.rev; m[r+1..,c]|]

let isVisible (m:int[,]) (t:int*int)=
    let v = m[fst(t),snd(t)]
    collinearTrees t m
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

let computeScore (m:int[,]) (t:int*int)=
    let v = m[fst(t),snd(t)]
    collinearTrees t m
    |> Array.map (takeUntil (fun x -> x >= v ))
    |> Array.map Array.length
    |> Array.fold (*) 1

let points rows cols =
    seq{
        for r in 0..rows do
            for c in 0..cols do
                yield (r,c)
    }

let countVisible (m:int[,]) =
    points (m.[*,0].Length - 1) (m.[0,*].Length - 1)
    |> Seq.map (isVisible m)
    |> Seq.filter id
    |> Seq.length

let bestScore (m:int[,]) =
    points (m.[*,0].Length - 1) (m.[0,*].Length - 1)
    |> Seq.map (computeScore m)
    |> Seq.sortDescending
    |> Seq.head
    