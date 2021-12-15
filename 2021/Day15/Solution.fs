module _2021.Day15.Solution

type Point = (int*int)
type Risk = int
type CavernShape = int[,]


let mapSize (m:int[,])= (m.[*,0].Length - 1, m.[0,*].Length - 1)

let readMap (p:Point) (m:CavernShape):int = m[fst p, snd p]


let nextPoints (cavern:CavernShape) (from:Point):(Point * int)[]=
    let h,w = mapSize cavern
    let inBounds p =
        (fst p) >= 0 && (fst p) <= h && (snd p) >= 0 && (snd p) <= w
    let r,c = from
    [|Point (r+1,c);Point (r, c+1)|]
    |> Array.filter inBounds
    |> Array.map (fun p -> (p, (readMap p cavern)))
    

let part1 (debug:string->unit) (map:int[,]) =
   0