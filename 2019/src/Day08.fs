module Day08
open System
open System.IO
open SkiaSharp;

type Layer = int array
type SpaceImage = {Content : Layer array; Width:int; Height:int}
type DecodedImage = {Pixels:int array;Width:int;Height:int}

module Layer = 
    let countDigit (digit:int) (layer:Layer) = layer |> Array.where (fun d->d=digit) |> Array.length
  
    let layerWithFewer (digit:int) (layers:Layer array)=
        layers
        |> Array.sortBy (fun layer -> layer |> Array.where (fun d->d=digit) |> Array.length)
        |> Array.head

    let private decodePixels pixels = 
        pixels |> Array.map (fun a -> a |> Array.except [2] |> Array.head)
    
    let private zip (layers:Layer array)=
        [|0..(layers.[0].Length-1)|]
        |> Array.map(fun i-> layers |> (Array.map (fun layer ->layer.[i])))

    let stack = zip>>decodePixels

module SpaceImage= 
    let create (width:int) (height:int)=
        {Content=Array.empty<Layer>;Width=width;Height=height}

    let load (input:string) (image:SpaceImage):SpaceImage= 
        let layers = input.ToCharArray() 
                    |> Array.map (Char.GetNumericValue>>Convert.ToInt32)
                    |> Array.chunkBySize (image.Width*image.Height)
        {Content=layers;Width=image.Width;Height=image.Height}

    let decode (image:SpaceImage):DecodedImage=
        let decodedPixels = (Layer.stack image.Content)
        {Pixels=decodedPixels;Width=image.Width;Height=image.Height}

module Bitmap= 
    let private pixelColor pixel=
        match pixel with
        |0 -> SKColors.Black
        |_ -> SKColors.White

    let create (fileName:string) (image:DecodedImage) =
        let bmp = new SKBitmap(image.Width, image.Height)
        let mutable x=0 
        let mutable y=0
        let pixels = image.Pixels
        for i in [0..pixels.Length-1] do
            let color = pixelColor pixels.[i]
            do bmp.SetPixel(x,y,color)
            x<-x+1
            if x%image.Width=0 then
                x<-0
                y<-y+1
        let data = SKImage.FromBitmap(bmp).Encode(SKEncodedImageFormat.Png, 100)
        use stream = File.Open(fileName,FileMode.Create)
        data.SaveTo(stream)
        image

let part1 width height input =
    let image = SpaceImage.create width height |> SpaceImage.load input
    let layer = image.Content |> Layer.layerWithFewer 0
    (*) (Layer.countDigit 1 layer) (Layer.countDigit 2 layer)

let part2 width height input=
   SpaceImage.create width height 
   |> SpaceImage.load input 
   |> SpaceImage.decode 
   |> Bitmap.create "d08.png"