module _2021.Day11.Solution

open System.Collections.Generic
let mapSize (m:int[,])= (m.[*,0].Length - 1, m.[0,*].Length - 1)

let increment (m:int[,]) p =
    let newValue =  m[fst p, snd p]+1
    m[fst p, snd p] <- newValue
    (newValue,p)

let reset (m:int[,]) p =
    m[fst p, snd p] <- 0

let points (m:int[,]) =
    let rows, cols = (m.[*,0].Length - 1, m.[0,*].Length - 1)
    seq{
        for r in 0..rows do
            for c in 0..cols do
                  yield (r,c)
    }
let adjacent (map:int[,]) point =
    let rows, cols = mapSize map
    let r,c = point
    [(r-1,c); (r+1,c); (r, c-1); (r, c+1); (r+1, c-1); (r-1, c-1); (r+1, c+1); (r-1, c+1) ]
    |> Seq.filter (fun p -> (fst p) >= 0 && (fst p) <= rows && (snd p) >= 0 && (snd p) <= cols)

let evolve (map:int[,]) =
    let reset = reset map
    let adjacent = adjacent map

    let increment (queue:Queue<int * int>) (p:int*int) =
        match increment map p with
        |10,_  -> queue.Enqueue p
        |_ -> ()
    
    seq{
        while true do
            let flashed = HashSet<int*int>()
            let queue = Queue<int*int>()
           
            for p in points map do
                increment queue p
                       
            while queue.Count > 0 do
                let flash = queue.Dequeue()
                flashed.Add(flash) |> ignore
                
                for p in (adjacent flash) do
                    increment queue p
                
            for p in flashed do
                reset p
            
            yield flashed.Count    
    }

let part1 (map:int[,]) =
    evolve map
    |> Seq.take 100
    |> Seq.sum

let part2 (map:int[,]) =
    (evolve map
    |> Seq.takeWhile (fun flash -> flash <> 100)
    |> Seq.length) + 1