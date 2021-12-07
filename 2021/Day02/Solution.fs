module _2021.Day02.Solution

type Dir = Forward | Down | Up
type Point3 = (int * int * int)
type Command = (Dir * int)

let origin = Point3(0,0,0)

let updatePosition1 pos cmd =
    let (x,y,aim) = pos
    match cmd with
    |(Forward,v) -> Point3(x+v,y,aim)
    |(Down,v) -> Point3(x, y+v, aim)
    |(Up, v) -> Point3(x, y-v, aim)


let updatePosition2 pos cmd =
    let (x,y,aim) = pos
    match cmd with
    |(Forward,v) -> Point3(x+v,y+v*aim,aim)
    |(Down,v) -> Point3(x, y, aim + v)
    |(Up, v) -> Point3(x, y, aim - v)

let part1 (commands:seq<Command>) =
    let (x, y, _) =
        commands
        |> Seq.fold updatePosition1 origin
    x * y

let part2 (commands:seq<Command>) =
    let (x, y, _) =
        commands
        |> Seq.fold updatePosition2 origin
    x * y    