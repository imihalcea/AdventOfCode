module _2021.Day06.Solution


let rec evolve iter (fish:int64[])  =
    let shift (currentFish:int64[]) =
        let new_fish = Array.create currentFish.Length 0L
        {1..8} |> Seq.iter (fun i -> new_fish.[i-1] <- currentFish.[i])
        let reproducers = currentFish.[0]
        new_fish.[6] <- new_fish.[6] + reproducers
        new_fish.[8] <- reproducers
        new_fish    
    match iter with
    | 0 -> fish
    | n -> shift fish |> evolve (n-1)


let part1 (days:int) (timers:seq<int>)  =
    timers
    |> Seq.groupBy id
    |> Seq.fold (fun arr it -> arr |> Array.updateAt (fst it) ((snd it) |> Seq.length |> int64)) (Array.create 9 0L)
    |> Seq.toArray
    |> evolve days
    |> Array.fold (fun acc x -> acc + int64(x)) 0L
    
    