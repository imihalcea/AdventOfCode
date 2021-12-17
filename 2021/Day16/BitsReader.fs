module _2021.Day16.BitsReader

open BitTools

type BitsReader(bits:bool[])=
    let mutable head = 0
    let advanceHeadBy (count:int)=
        head<-head+count
        
    member x.HasMore() = head < bits.Length
    member x.Fork(count:int)=
        let sequencer = BitsReader(bits[head..head+count-1])
        advanceHeadBy count
        sequencer
    member x.Read(count:int):int=
        let result = toInt bits[head..head+count-1]
        advanceHeadBy count
        result
    member x.Skip(count:int):int=
        advanceHeadBy count
        0