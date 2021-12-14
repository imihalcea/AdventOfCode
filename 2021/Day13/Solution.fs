module _2021.Day13.Solution

open System
open System.IO
open FSharpx.Collections
open SkiaSharp
open Swensen.Unquote
open Xunit
open _2021.Day05.Solution


type Point = {
    x:int
    y:int
}

type Axis = X of int | Y of int 

let translateY (instruction:int) (p:Point) : Option<Point> =
    match (instruction, p.x, p.y) with
    |sy, px, py when py>sy -> Some {x=px;y=abs(py-2*sy) }
    |sy, px, py when py<sy -> Some p
    |_,_,_ -> None

let translateX (instruction:int) (p:Point) : Option<Point> =
    match (instruction, p.x, p.y) with
    |sx, px, py when px>sx -> Some {x=abs(px-2*sx);y=py }
    |sx, px, py when px<sx -> Some p
    |_,_,_ -> None

let fold (points:Set<Point>) (instruction:Axis):Set<Point> =
     let translator =
         match instruction with
         |X(v) -> translateX v
         |Y(v) -> translateY v
         
     Set.map translator points
    |> Set.filter (fun p -> p.IsSome)
    |> Set.map (fun p -> p.Value)


let part1 (points:Set<Point>):int =
    fold points (X 655)
    |> Set.count

let createImage (fileName:string) (points:Set<Point>) =
    let bmp = new SKBitmap(100, 100)
    for p in points do
            let color = SKColors.Black
            do bmp.SetPixel(p.x,p.y,color)
            
    let data = SKImage.FromBitmap(bmp).Encode(SKEncodedImageFormat.Png, 100)
    use stream = File.Open(fileName,FileMode.Create)
    data.SaveTo(stream)
    
let part2 (points:Set<Point>):int =
    let finish = 
        [X 655; Y 447; X 327; Y 223; X 163; Y 111; X 81; Y 55; X 40; Y 27; Y 13; Y 6]
        |> List.fold fold points
    createImage "part2.png" finish
    0