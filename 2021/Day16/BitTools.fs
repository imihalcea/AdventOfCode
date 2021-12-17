module _2021.Day16.BitTools

open System
open System.Globalization

let toInt (bits:bool[]):int =
    bits |> Array.fold (fun acc b -> acc*2 + (if b then 1 else 0)) 0

let extractHalfByte (n:int):bool[] =
    [|8;4;2;1|]|> Array.map (fun m -> (m &&& n) <> 0)
    
let rawBits (hex:string):bool[] =
    hex.ToCharArray()
    |> Seq.map (fun c -> Int32.Parse(c.ToString(),NumberStyles.HexNumber))
    |> Seq.map extractHalfByte
    |> Seq.concat
    |> Seq.toArray