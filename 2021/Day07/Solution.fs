module _2021.Day07.Solution

let fuelForPosition fc pos crabs =
    crabs
    |> Array.map (fun crab -> abs (crab-pos) |> fc)
    |> Array.sum

let minimumFuel costFun crabs =
    let posMin = Array.min crabs
    let posMax = Array.max crabs
    {posMin..posMax}
    |> Seq.map (fun pos -> (fuelForPosition costFun pos crabs))
    |> Seq.min    

let part1 crabs =
    crabs |> minimumFuel id

let part2 crabs =
    let costFun distance =
        distance * (distance+1) / 2
    crabs |> minimumFuel costFun