module _2021.Day11.Solution

open System.Collections.Generic
type CavernMap(grid:int[,])=
      member s.mapSize = (grid.[*,0].Length - 1, grid.[0,*].Length - 1)
      member s.IncrementAt p =
            let newValue =  grid.[fst p, snd p]+1
            grid.[fst p, snd p] <- newValue
            (newValue,p)
      member s.ResetAt p = grid[fst p, snd p] <- 0

      member s.Points() =
          let rows, cols = s.mapSize
          seq{
            for r in 0..rows do
               for c in 0..cols do
                  yield (r,c)
            }
    
      member s.Adjacent point =
          let rows, cols = s.mapSize
          let r,c = point
          [(r-1,c); (r+1,c); (r, c-1); (r, c+1); (r+1, c-1); (r-1, c-1); (r+1, c+1); (r-1, c+1) ]
          |> Seq.filter (fun p -> (fst p) >= 0 && (fst p) <= rows && (snd p) >= 0 && (snd p) <= cols)

let evolve (grid:int[,]) =
    let cavern = CavernMap(grid)    
    
    let increment (queue:Queue<int * int>) (p:int*int) =
        match cavern.IncrementAt(p) with
        |10,_  -> queue.Enqueue p
        |_ -> ()
    
    seq{
        while true do
            let flashed = HashSet<int*int>()
            let queue = Queue<int*int>()
           
            for p in cavern.Points() do
                increment queue p
                       
            while queue.Count > 0 do
                let flash = queue.Dequeue()
                flashed.Add(flash) |> ignore
                
                for p in cavern.Adjacent(flash) do
                    increment queue p
                
            for p in flashed do
                cavern.ResetAt(p)
            
            yield flashed.Count    
    }

let part1 (grid:int[,]) =
    evolve grid
    |> Seq.take 100
    |> Seq.sum

let part2 (grid:int[,]) =
    (evolve grid
    |> Seq.takeWhile (fun flash -> flash <> 100)
    |> Seq.length) + 1