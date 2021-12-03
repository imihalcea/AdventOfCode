module _2021.Day03.Solution

open System.Collections

type common = {least:bool; most:bool}

let toCommon bits =
    let mutable oneCounter = 0
    let mutable zeroCounter = 0
    
    for b in bits do
        if b then
            oneCounter <- oneCounter + 1
        else
            zeroCounter <- zeroCounter + 1
    
    match oneCounter >= zeroCounter with
    |true -> {least=false;most=true}
    |false -> {least=true;most=false}
    
let toInt (bits:BitArray):int =
    let arr = [|0|]
    bits.CopyTo(arr,0)
    arr.[0]
    

let part1 (matrix:bool[,]) =
   let commons =
        seq {0..11}
        |> Seq.map (fun col -> matrix.[*, col])
        |> Seq.map toCommon
        
   let gamma = commons
               |> Seq.map (fun c -> c.most)
               |> Seq.rev
               |> Seq.toArray
               |> BitArray
               |> toInt
   
   let epsilon = commons
               |> Seq.map (fun c -> c.least)
               |> Seq.rev
               |> Seq.toArray
               |> BitArray
               |> toInt    
   gamma * epsilon 
    