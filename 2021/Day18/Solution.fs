module _2021.Day18.Solution

open System
open System.Collections.Generic

type Dir =
    | Left
    | Right

type Num =
    | Reg of int
    | Pair of Num * Num

let rec private cata fReg fPair num =
    let recurse = cata fReg fPair
    match num with
    | Reg v -> fReg v
    | Pair (a, b) -> fPair (recurse a) (recurse b)

let rec private add dir num v =
    match num, dir with
    | Reg i, _ -> Reg(i + v)
    | Pair (a, b), Left -> Pair(add Left a v, b)
    | Pair (a, b), Right -> Pair(a, add Right b v)

let fromString (input: seq<char>) : Num =
    let folder (stack: Stack<Num>) (ch: Char) =
        match ch with
        | ']' ->
            let a, b = stack.Pop(), stack.Pop()
            stack.Push(Pair(b, a))
        | c when Char.IsDigit ch -> stack.Push(Reg(Int32.Parse(c.ToString())))
        | _ -> ignore ()
        stack
    (Seq.fold folder (Stack<Num>()) input).Pop()

let rec dynamite num depth =
    let addLeft = add Left
    let addRight = add Right

    match num with
    | Reg _ -> num, None
    | Pair (Reg l, Reg r) when depth >= 4 -> Reg 0, Some(l, r)
    | Pair (left, right) ->
        let newLeft, newRight, restLeft =
            match dynamite left (depth + 1) with
            | newLeft, Some (expLeft, expRight) -> newLeft, addLeft right expRight, Some expLeft
            | _ -> left, right, None

        let newLeft, newRight, restRight =
            match dynamite newRight (depth + 1) with
            | newRight, Some (expLeft, expRight) -> addRight newLeft expLeft, newRight, Some expRight
            | _ -> newLeft, newRight, None

        match restLeft, restRight with
        | None, None -> Pair(newLeft, newRight), None
        | _ -> Pair(newLeft, newRight), Some(defaultArg restLeft 0, defaultArg restRight 0)

let explode num = dynamite num 0 |> fst

let rec split node =
    match node with
    | Reg n when n >= 10 -> Pair(Reg(n / 2), Reg(n - n / 2)), true
    | Reg _ -> node, false
    | Pair (l, r) ->
        match split l with
        | splitLeft, true -> Pair(splitLeft, r), true
        | _ ->
            let (splitRight, didSplit) = split r
            Pair(l, splitRight), didSplit

let rec reduce node =
    match explode node |> split with
    | afterSplit, true -> reduce afterSplit
    | notSplit, false -> notSplit

let addNodes a b = Pair(a, b) |> reduce

let rec magnitude num = cata id (fun l r -> 3 * l + 2 * r) num

let part1 input =
    input |> Seq.reduce addNodes |> magnitude

let part2 input =
    Seq.allPairs (Seq.indexed input) (Seq.indexed input)
    |> Seq.filter (fun ((i, _), (j, _)) -> i <> j)
    |> Seq.map (fun ((_, l), (_, r)) -> (l, r))
    |> Seq.map (fun (l, r) -> max (addNodes l r |> magnitude) (addNodes r l |> magnitude))
    |> Seq.max
