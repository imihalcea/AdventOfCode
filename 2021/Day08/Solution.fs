module _2021.Day08.Solution

open System
open System.Collections.Generic
type Value = {input:string[]; output:string[]}

let part1 values =
    let digits = HashSet([|7;4;3;2|])
    values
    |> Seq.map (fun v -> v.output)
    |> Seq.concat
    |> Seq.map (fun o -> o.Length)
    |> Seq.filter (fun l -> digits.Contains l)
    |> Seq.length

let toInt value =
    let readInput (input:string[]) =
        let one = input |> Seq.find (fun w -> w.Length = 2)
        let four = input |> Seq.find (fun w -> w.Length = 4)
        let seven = input |> Seq.find (fun w -> w.Length = 3)
        (one,four,seven)
    
    let numberOfCommonChars (s1:string) (s2:string) =
        let ss1 = Set.ofArray (s1.ToCharArray())
        let ss2 = Set.ofArray (s2.ToCharArray())
        (ss1, ss2)
        ||> Set.intersect
        |> Set.count
        
    let digitOfFiveLettersWord word one four =
        match word with
        | w when (numberOfCommonChars w one) = 2 -> 3
        | w when (numberOfCommonChars w four) = 3 -> 5
        | _ -> 2 

    let digitOfSixLettersWord word four seven =
        match word with
        | w when (numberOfCommonChars w four) = 4 -> 9
        | w when (numberOfCommonChars w seven) = 3 -> 0
        | _ -> 6
        
    let mapInt index word one four seven =
        let digit = 
            match String.length word with
            |2 -> 1
            |3 -> 7
            |4 -> 4
            |7 -> 8
            |5 -> digitOfFiveLettersWord word one four
            |6 -> digitOfSixLettersWord word four seven
            |_ -> failwith "tu bluffes Martoni"
        (pown 10 (3-index)) * digit
    
    let (one, four, seven) = readInput value.input
    value.output
    |> Seq.mapi (fun idx word -> mapInt idx word one four seven)
    |> Seq.sum

let part2 values =
    values
    |> Seq.map toInt
    |> Seq.sum 