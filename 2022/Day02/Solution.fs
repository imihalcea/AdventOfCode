module _2022.Day02.Solution

open System
open Microsoft.FSharp.Collections

type Outcome = |Win = 6 |Draw = 3 |Loss = 0
type Hand = |Rock = 1 |Paper = 2 |Scissors =3
type Round = Hand * Hand
type Strategy = Hand * Outcome

let strategies = Map[
    (Outcome.Win,  Map[(Hand.Paper, Hand.Scissors); (Hand.Rock, Hand.Paper); (Hand.Scissors, Hand.Rock)])
    (Outcome.Loss, Map[(Hand.Paper, Hand.Rock); (Hand.Rock, Hand.Scissors); (Hand.Scissors, Hand.Paper)])
    (Outcome.Draw, Map[(Hand.Rock, Hand.Rock); (Hand.Scissors, Hand.Scissors); (Hand.Paper, Hand.Paper)])
]

let outcomes = Map[
    (Hand.Paper, Map[(Hand.Rock,Outcome.Win); (Hand.Scissors, Outcome.Loss); (Hand.Paper, Outcome.Draw)])
    (Hand.Rock, Map[(Hand.Paper,Outcome.Loss); (Hand.Scissors, Outcome.Win); (Hand.Rock, Outcome.Draw)])
    (Hand.Scissors, Map[(Hand.Paper,Outcome.Win); (Hand.Scissors, Outcome.Draw); (Hand.Rock, Outcome.Loss)])
]

let decodeRound = Map [("A",Hand.Rock); ("B",Hand.Paper); ("C", Hand.Scissors);("X",Hand.Rock); ("Y",Hand.Paper); ("Z", Hand.Scissors); ]
let decodeOutcomes = Map[("X", Outcome.Loss); ("Y", Outcome.Draw); ("Z", Outcome.Win)]

let score round =
    let opponent, me = round
    int(outcomes[me][opponent]) + int(me)

let cheat strat =
    let opponent, outcome = strat
    (opponent, strategies[outcome][opponent])

let part1 (data:string) =
    data.Split("\n", StringSplitOptions.TrimEntries)
    |> Array.map (fun it -> it.Split(" ", StringSplitOptions.TrimEntries))
    |> Array.map (fun it -> (decodeRound[it[0]], decodeRound[it[1]]))
    |> Array.map score
    |> Array.sum

let part2 (data:string) =
    data.Split("\n", StringSplitOptions.TrimEntries)
    |> Array.map (fun it -> it.Split(" ", StringSplitOptions.TrimEntries))
    |> Array.map (fun it -> (decodeRound[it[0]], decodeOutcomes[it[1]]))
    |> Array.map cheat
    |> Array.map score
    |> Array.sum
