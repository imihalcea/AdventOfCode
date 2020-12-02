module Day10
open System.Numerics
open System
open FSharpx.Collections

type Segment = {FromPoint:Vector2; ToPoint:Vector2;Theta:float; Length:single}
type AsteroidGrid = seq<Vector2> 

module Segment = 
   let theta (u:Vector2) (v:Vector2) = Math.Atan2(float(u.Y-v.Y),float(u.X-v.X))

   let create (u:Vector2) (v:Vector2) = 
        {FromPoint=u; ToPoint=v; Theta=(theta u v); Length=Vector2.Distance(u,v)}

   let colinears segments = 
        segments |> Seq.groupBy (fun seg -> seg.Theta)

   let sortByTheta (segments:Segment list)=
        List.concat [
             (segments |> List.where (fun s-> s.Theta>=Math.PI/2. && s.Theta<=Math.PI) |> List.sortBy (fun s -> s.Theta));
             (segments |> List.where (fun s-> s.Theta>=((-1.)*(Math.PI)) && s.Theta<Math.PI/2.) |> List.sortBy (fun s -> s.Theta))   
        ]

   let angles (segments:Segment list)=
        segments |> List.map (fun s -> s.Theta) |> List.distinct

   let shortestWithAngle (angle:float) (segments:List<Segment>)=
        segments |> List.where (fun s -> s.Theta=angle) |> List.minBy (fun s -> s.Length)

module AsteroidGrid=
    let create (input:string[]):AsteroidGrid=
        seq{
            for y in [0..input.Length-1] do
                let chars = input.[y].ToCharArray()
                for x in [0..chars.Length-1] do
                    if chars.[x]='#' then
                        yield Vector2(float32(x),float32(y))
        }
    
    let toSegments positionOnGrid =
        let(position:Vector2,grid:AsteroidGrid) = positionOnGrid
        grid |> Seq.except [position] 
             |> Seq.map (fun other -> Segment.create position other)
             |> Seq.toList
    
    let detect positionOnGrid=
        positionOnGrid |> toSegments |> Segment.colinears
    
    let private bestPosition (grid:AsteroidGrid)=
        let position = grid 
                        |> Seq.sortByDescending (fun p -> detect(p,grid)|> Seq.length)
                        |> Seq.toArray
                        |> Array.head
        (position,grid)

    let detectionsFromBestPosition = 
        create >> bestPosition >> detect

    let vaporize n positionOnGrid = 
        let mutable vaporized = List.empty<Segment>
        let mutable segments = positionOnGrid |> toSegments |> Segment.sortByTheta 
        let angles  = segments |> Segment.angles
        while vaporized.Length<n do
            angles |> List.iter (fun angle -> let s = Segment.shortestWithAngle angle segments
                                              vaporized<-vaporized@[s])
            segments <- List.except vaporized segments
        vaporized |> List.map (fun s -> s.ToPoint) |> List.take n

    let vaporizeFromBestPosition n=
        create >> bestPosition >> vaporize n

let part1 (inputLines:string[])=
    inputLines |> AsteroidGrid.detectionsFromBestPosition |> Seq.length
    
let part2 (inputLines:string[])=
    inputLines |> AsteroidGrid.vaporizeFromBestPosition 200 |> List.last