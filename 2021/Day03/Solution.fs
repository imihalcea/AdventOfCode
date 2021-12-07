module _2021.Day03.Solution

open System.Collections

type sortedBits = {least:bool; most:bool}

let rows (matrix:bool[,]) =
    matrix.[*,0].Length

let cols (matrix:bool[,]) =
    matrix.[0,*].Length

let toSortedBits bits =
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

let sortBits (matrix:bool[,]) =
    seq {0..(cols(matrix)-1)}
        |> Seq.map (fun col -> matrix.[*, col])
        |> Seq.map toSortedBits

    
let toInt (bits:bool[]):int =
    let arr = [|0|]
    let bitArr = BitArray(Array.rev bits)
    bitArr.CopyTo(arr,0)
    arr.[0]
    
let toRate (sortedBits:seq<sortedBits>) (selector: sortedBits -> bool) = 
    sortedBits
               |> Seq.map selector
               |> Seq.toArray
               |> toInt


let rec findRating (selector: sortedBits -> bool) (col:int) (matrix:bool[,]) : bool[]=
    let sortedBits = sortBits matrix
    
    let predicate (col:int) (src:bool[]) =
        let bit = sortedBits |> Seq.skip col |> Seq.head |> selector
        (src |> Seq.skip col |> Seq.head) = bit
    
          
    match rows(matrix) with
    |0 -> [|false|]
    |1 -> matrix.[0,*]
    |_ ->
           seq{
                for r in 0..(rows matrix)-1 do
                    let bits = matrix.[r,*]
                    if (predicate col bits) then
                        yield bits
            }
            |> array2D
            |> findRating selector (col+1) 

let part1 (matrix:bool[,]) =
   let sortedBits = sortBits matrix
   let gamma = toRate sortedBits (fun c -> c.most)
   let epsilon = toRate sortedBits (fun c -> c.least)
   gamma * epsilon


let part2 (matrix:bool[,]) =
   let o2Rating = toInt (findRating (fun c -> c.most) 0 matrix)
   let co2Rating = toInt (findRating (fun c -> c.least) 0 matrix)
   o2Rating * co2Rating 