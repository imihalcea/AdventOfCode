[<Microsoft.FSharp.Core.AutoOpen>]
module _2022.Tools.Stack


type Stack<'T when 'T:equality>  =
    | Empty
    | Stack of 'T * Stack<'T>

    static member empty<'T>() = Empty
    member s.Push x =
        Stack(x, s)

   
    
    member s.Pop() =
        match s with
        | Empty -> failwith "No elements"
        | Stack (t, s) -> (t,s)
    
    member s.accumulate (acc: 'T list) (popResult:'T*Stack<'T>) =
        acc @ [(fst popResult)], (snd popResult)
        
    member s.PopMany(n:int) =
        [0..n-1]
        |> List.fold (fun acc _ -> s.accumulate (fst acc) ((snd acc).Pop())) ([],s)
    
    member s.PushMany(items:'T list):Stack<'T> =
        items
        |> List.fold (fun acc -> acc.Push ) s    
    member s.Peak() =
        match s with
        | Empty -> failwith "No elements"
        | Stack (t, _) -> t

    member s.IsClear() =
        match s with
        | Empty -> true
        | _ -> false

    member s.Enumerate() =
        let mutable current = s
        seq{
            while not(current.IsClear()) do
                match current with
                | Stack(t,st) -> current <- st
                                 yield t
                | _ -> failwith "shouldn't be emty" 
        }

     member s.ContainsElement(x:'T):bool =
        s.Enumerate() |> Seq.contains x
