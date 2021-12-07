module _2021.Day04.Solution

open System.Collections.Generic

type Cell = (int * bool)

let ROWS = 5
let COLS = 5


let CheckWin (board: Cell [,]) =
    let winingRow =
        { 0 .. ROWS - 1 }
        |> Seq.map (fun rowIdx -> board.[rowIdx, *])
        |> Seq.tryFind (fun cells -> cells |> Array.forall snd)

    let winingCol =
        { 0 .. COLS - 1 }
        |> Seq.map (fun colIdx -> board.[*, colIdx])
        |> Seq.tryFind (fun cells -> cells |> Array.forall snd)

    match (winingCol, winingRow) with
    | (Some (_), None) -> Some((board))
    | (None, Some (_)) -> Some((board))
    | (_, _) -> None

let play (draw: int) (board: Cell [,]) =
    let mark =
        seq {
            for r in 0 .. ROWS - 1 do
                for c in 0 .. COLS - 1 do
                    yield (r, c)
        }
        |> Seq.tryFind (fun (r, c) -> fst (board.[r, c]) = draw)
    
    match mark with
    |Some(r,c) -> board.[r, c] <- (fst (board.[r, c]), true)
    |None -> ignore()
    
    CheckWin board
    

let computeResult (winner: Cell [,]) (draw: int) =
    let mutable cntUnMarked = 0

    for r in 0 .. ROWS - 1 do
        for c in 0 .. COLS - 1 do
            let (n, marked) = winner.[r, c]

            if not marked then
                cntUnMarked <- cntUnMarked + n

    draw * cntUnMarked

let part1 (draw: int []) (boards: Cell [,] []) =
    seq {
        for n in draw do
            for b in boards do
                let r = play n b
                yield (r,n)
    }
    |> Seq.tryFind (fun (x,_) -> x.IsSome)
    |> Option.map(fun (r,n)-> r |> Option.map (fun x -> (x,n)))
    |> Option.flatten
    |> Option.map (fun (w, d) -> computeResult w d)
    |> Option.defaultWith (fun _ -> 0)


let part2 (draw: int []) (boards: Cell [,][]) =
    let last = 
        seq{
            for n in draw do
                for b in boards do
                   match (CheckWin b) with
                   |Some(_) -> yield None
                   |None -> let r = play n b
                            match r with
                            |None -> yield None
                            |Some(x) -> yield Some((x,n))
        }
        |> Seq.filter (fun it -> it.IsSome)
        |> Seq.last
                   
    
    let (winner, num) = last.Value
    computeResult winner num