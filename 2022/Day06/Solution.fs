module _2022.Day06.Solution

let findStartPacket (packetSize:int) (input:string) =
    input
    |> Seq.windowed packetSize
    |> Seq.takeWhile (fun it -> set(it).Count <> packetSize)
    |> Seq.length
    |> (+) packetSize
     