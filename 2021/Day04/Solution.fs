module _2021.Day04.Solution

open System.Collections.Generic

type Cell = (int * bool)

let ROWS = 5
let COLS = 5


let markThenCheckWin (draw: int) (board: Cell [,]) (marked: (int * int)) =
    let (r, c) = marked
    board.[r, c] <- (fst (board.[r, c]), true)

    let winingRow =
        { 0 .. ROWS - 1 }
        |> Seq.map (fun rowIdx -> board.[rowIdx, *])
        |> Seq.tryFind (fun cells -> cells |> Array.forall snd)

    let winingCol =
        { 0 .. COLS - 1 }
        |> Seq.map (fun colIdx -> board.[*, colIdx])
        |> Seq.tryFind (fun cells -> cells |> Array.forall snd)

    match (winingCol, winingRow) with
    | (Some (_), None) -> Some((board, draw))
    | (None, Some (_)) -> Some((board, draw))
    | (_, _) -> None

let play (draw: int) (board: Cell [,]) =
    seq {
        for r in 0 .. ROWS - 1 do
            for c in 0 .. COLS - 1 do
                yield (r, c)
    }
    |> Seq.tryFind (fun (r, c) -> fst (board.[r, c]) = draw)
    |> Option.bind (markThenCheckWin draw board)

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
            for i in 0 .. boards.Length - 1 do
                let b = boards.[i]
                let r = play n b
                boards.[i] <- b
                yield r
    }
    |> Seq.tryFind (fun x -> x.IsSome)
    |> Option.flatten
    |> Option.map (fun (w, d) -> computeResult w d)
    |> Option.defaultWith (fun _ -> 0)


let part2 (draw: int []) (boards: Cell [,] []) =
    let queue = Queue<obj>()

    let options =
        seq {
            for n in draw do
                for i in 0 .. boards.Length - 1 do
                    let b = boards.[i]
                    let r = play n b

                    if r.IsSome then
                        queue.Enqueue((b.Clone(), n))

                    boards.[i] <- b
                    yield r
        }
        |> Seq.toArray

    options
    |> Seq.filter (fun x -> x.IsSome)
    |> Seq.tryLast
    |> Option.flatten
    |> Option.map (fun (w, d) -> computeResult w d)
    |> Option.defaultWith (fun _ -> 0)