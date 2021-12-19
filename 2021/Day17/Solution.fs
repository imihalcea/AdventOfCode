module _2021.Day17.Solution

open System

type Status = NotHit | Hit | Exceed

type Target(x1:int, y1:int, x2:int, y2:int)=
    member s.X1 = x1
    member s.X2 = x2
    member s.Y1 = y1
    member s.Y2 = y2
    
    member s.Check(x:int,y:int) =
        if x >= x1 && x<=x2 && y>=y2 && y<=y1 then
            Hit
        elif y<y2 || x>x2 then
            Exceed
        else
            NotHit

// vy € [yMin; abs(yMin)]
// vy will be 0 at some point
// the answer to part one is Sum (vy, vy-1, vy-2) apply gauss trick for the sum
let part1 (target:Target):int =
    let y = max (abs target.Y1) (abs target.Y2)
    y * (y-1) / 2

let rec isHit x y (box:Target) (velocity:int*int) =
    let vx, vy = velocity
    match box.Check(x,y) with
    |Hit-> true
    |Exceed-> false
    |NotHit-> (isHit (x+vx) (y+vy) box ((vx - sign vx),(vy-1)))

// vy € [yMin; abs(yMin)]
let part2 (target:Target):int=
    Seq.allPairs {1..target.X2+1} {target.Y2..(abs target.Y2)}
    |> Seq.map (isHit 0 0 target)
    |> Seq.filter id 
    |> Seq.length