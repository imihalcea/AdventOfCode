module _2022.Day08.Solution

open System


let parseData (lines : string seq):int[,]=
    lines |> Seq.map (fun line -> line.ToCharArray() |> Seq.map Convert.ToInt32)
    |> array2D

