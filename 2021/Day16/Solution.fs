module _2021.Day16.Solution

open FSharpx.Collections
open BitTools
open Microsoft.FSharp.Core
open BitsReader

type Packet = {version:int; typeId:int; decodedValue:int64; subPackets:Packet list}

let rec decodeBits (bitsReader:BitsReader) :Packet=
    let version = bitsReader.Read 3
    let typeId = bitsReader.Read 3
    let mutable packets = List.empty
    let mutable value = 0L
    
    match typeId with
    |4 -> value <- decodeLiteral bitsReader
    |_ -> match (bitsReader.Read 1) with
          |0 -> packets <- decodeOperatorZero bitsReader
          |1 -> packets <- decodeOperatorOne bitsReader
          |_ -> failwith "not supported"
    {version=version;typeId=typeId; decodedValue=value; subPackets = packets}      
and decodeLiteral (sequencer:BitsReader) : int64=
    let mutable isLastChunk = false
    let mutable value = 0L
    while not(isLastChunk) do
        isLastChunk <- (sequencer.Read(1) = 0)
        value<- value * 16L + (int64 (sequencer.Read 4))
    value    
and decodeOperatorZero (reader:BitsReader):Packet list =
    let subReader = reader.Read 15 |> reader.Fork
    seq{
        while subReader.HasMore() do
            yield (decodeBits subReader)
    }|> Seq.toList
and decodeOperatorOne (sequencer:BitsReader):Packet list=
    let cnt = sequencer.Read 11
    [0..cnt-1]|> List.map (fun _ -> decodeBits sequencer)
    
let decode  = rawBits >> BitsReader >> decodeBits
    
let part1 (input:string)=
    let rec addVersion (p:Packet):int=
        p.version + Seq.sumBy addVersion p.subPackets
    decode input |> addVersion
    
let part2 (input:string)=
    let rec eval (p:Packet):int64=
        let items = p.subPackets |> Seq.map eval |> Seq.toArray
        match p.typeId with
        |0 -> Array.fold (fun acc it -> acc + it) 0L items
        |1 -> Array.fold (fun acc it -> acc * it) 1L items
        |2 -> Array.min items
        |3 -> Array.max items
        |4 -> p.decodedValue
        |5 -> if items[0]>items[1] then 1L else 0L
        |6 -> if items[0]<items[1] then 1L else 0L
        |7 -> if items[0]=items[1] then 1L else 0L
        |_ -> failwith "not supported"
        
    decode input |> eval