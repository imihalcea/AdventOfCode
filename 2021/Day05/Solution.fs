module _2021.Day05.Solution
type Line = { x1: int; y1: int; x2: int; y2: int }
type Point = { x: int; y: int }
let isStraightLine line = line.x1 = line.x2 || line.y1 = line.y2

let diagonalPoints line =
    seq {
        let delta_y = abs (line.y2 - line.y1)
        let sy = sign (line.y2 - line.y1)
        let sx = sign (line.x2 - line.x1)

        for dy in 0 .. delta_y do
            yield { x = line.x1 + sx * dy; y = line.y1 + sy * dy }
    }

let straightPoints line =
    seq {
            for x in (min line.x1 line.x2) .. (max line.x1 line.x2) do
                for y in (min line.y1 line.y2) .. (max line.y1 line.y2) do
                    yield { x = x; y = y }
        }

let toPoints line =
    match isStraightLine line with
    | true -> straightPoints line
    | false -> diagonalPoints line

let countPoints (m: Map<Point, int>) (p: Point) =
    m |> Map.change  p (fun x ->
                            match x with
                            | Some (cnt) -> Some(cnt + 1)
                            | None -> Some(1))

let solverPipeline lines predicate =
    lines
    |> Seq.filter predicate 
    |> Seq.map toPoints
    |> Seq.concat
    |> Seq.fold countPoints Map.empty
    |> Map.values
    |> Seq.where (fun cnt -> cnt >= 2)
    |> Seq.length

let part1 (lines: seq<Line>) =
    solverPipeline lines isStraightLine

let part2 (lines: seq<Line>) =
    solverPipeline lines (fun _ -> true)    