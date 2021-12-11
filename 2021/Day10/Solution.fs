module _2021.Day10.Solution

type Char = Open of Item | Close of Item
and Item = Parenthesis| Bracket |CurlyBrace |Chevron

let mapping = Map[
                  ('(', Open Parenthesis); (')', Close Parenthesis)
                  ('[', Open Bracket); (']', Close Bracket)
                  ('{', Open CurlyBrace); ('}', Close CurlyBrace)
                  ('<', Open Chevron); ('>', Close Chevron)
                  ]

let scan (input:string) =
    let parse c = mapping[c]
       
    let rec check (stack:list<Char>) (line:list<Char>)=
        let isCorruption a =
            match (a, (List.head stack)) with
            |Close x, Open y when x <> y -> true
            |_ -> false
       
        match line with
        |[] -> Ok(stack)
        |current::others ->
            match current with
            |_ when (List.isEmpty stack) -> check [current] others
            |Open _                      -> check (current::stack) others
            |Close _ when isCorruption current -> Error(current) 
            |_                                 -> check (List.tail stack) others
        
    input.ToCharArray() |> Seq.map parse |> Seq.toList |> check []       
   
let part1 (lines:seq<string>):int =
    let scores = Map[(Close Parenthesis,3); (Close Bracket,57); (Close CurlyBrace, 1197); (Close Chevron,25137)]

    lines
    |> Seq.map scan
    |> Seq.map (fun r -> match r with
                        |Error c -> scores[c]
                        | _ -> 0)
    |> Seq.sum

let part2 (lines:seq<string>):int64 =
    let opposite a =
        match a with
        |Open(x) -> Close(x)
        |Close(x) -> Open(x)
    
    let completeLine r =
       match r with
       |Ok(remaining) -> List.map opposite remaining
       |_ -> []

    let scoresMap = Map[(Close Parenthesis,1); (Close Bracket,2); (Close CurlyBrace, 3); (Close Chevron,4)]
    
    let scores =
            lines
            |> Seq.map scan
            |> Seq.map completeLine |> Seq.filter (fun l -> not (List.isEmpty l))      
            |> Seq.map (Seq.fold (fun acc c -> acc * 5L + int64(scoresMap.[c])) 0L)
            |> Seq.sort
    
    let n = ((Seq.length scores) - 1)/2
    scores |> Seq.skip n |> Seq.head