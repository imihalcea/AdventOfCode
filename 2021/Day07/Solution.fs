module _2021.Day07.Solution

let fuelForPosition cost pos crabs =
    crabs
    |> Array.fold (fun acc crab -> cost (abs (crab-pos)) + acc ) 0

let minimumFuel cost crabs =
    crabs |> Array.sortInPlace    
    {crabs.[0]..crabs.[^1]}
    |> Seq.map (fun pos -> (fuelForPosition cost pos crabs))
    |> Seq.min    

let part1 crabs =
    crabs |> minimumFuel id

let part2 crabs =
    crabs |> minimumFuel (fun d -> d * (d + 1)/2)