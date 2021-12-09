module _2021.Day08.Solution

open System.Collections.Generic

type Value = { input: string []; output: string [] }

let part1 values =
    let digits = HashSet([| 7; 4; 3; 2 |])

    values
    |> Seq.map (fun v -> v.output)
    |> Seq.concat
    |> Seq.map (fun o -> o.Length)
    |> Seq.filter (fun l -> digits.Contains l)
    |> Seq.length

let toInt value =
    let one = value.input |> Seq.find (fun w -> w.Length = 2)
    let four = value.input |> Seq.find (fun w -> w.Length = 4)
    let seven = value.input |> Seq.find (fun w -> w.Length = 3)

    let numberOfCommonChars (s1: string) (s2: string) =
        let ss1 = Set.ofArray (s1.ToCharArray())
        let ss2 = Set.ofArray (s2.ToCharArray())
        (ss1, ss2) ||> Set.intersect |> Set.count

    let decode word =
        match (word, String.length word) with
        | (_, 2) -> 1
        | (_, 3) -> 7
        | (_, 4) -> 4
        | (_, 7) -> 8
        | (w, 5) when (numberOfCommonChars w one) = 2 -> 3
        | (w, 5) when (numberOfCommonChars w four) = 3 -> 5
        | (_, 5) -> 2
        | (w, 6) when (numberOfCommonChars w four) = 4 -> 9
        | (w, 6) when (numberOfCommonChars w seven) = 3 -> 0
        | (_, 6) -> 6
        | (_, _) -> failwith "tu bluffes Martoni"

    value.output |> Seq.mapi (fun i w -> (pown 10 (3 - i)) * (decode w)) |> Seq.sum

let part2 values = values |> Seq.map toInt |> Seq.sum 