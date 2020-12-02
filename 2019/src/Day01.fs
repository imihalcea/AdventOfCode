module Day01

type SpaceModule = {Mass:int}

let private generateFuelNeeds spaceModule =
    let calculateFuelNeed = fun mass -> mass / 3 - 2
    let initalFuelMass = calculateFuelNeed spaceModule.Mass
    seq{
        let mutable remain = calculateFuelNeed initalFuelMass
        while remain>0 do
            yield remain
            remain <- calculateFuelNeed remain
        yield initalFuelMass
    }

let FuelForSpaceModule spaceModule =
    spaceModule |> generateFuelNeeds |> Seq.sum

let FuelForAllSpaceModules spaceModules = 
    spaceModules |> Seq.sumBy FuelForSpaceModule

