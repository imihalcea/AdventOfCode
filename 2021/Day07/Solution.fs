module _2021.Day07.Solution

let fuelForPosition fc pos crabs =
    crabs
    |> Array.fold (fun acc crab -> fc (abs (crab-pos)) + acc ) 0

let minimumFuel costFun crabs =
    crabs |> Array.sortInPlace    
    {crabs.[0]..crabs.[^1]}
    |> Seq.map (fun pos -> (fuelForPosition costFun pos crabs))
    |> Seq.min    

let part1 crabs =
    crabs |> minimumFuel id

let part2 crabs =
    let costFun distance =
        distance * (distance+1) / 2
    crabs |> minimumFuel costFun