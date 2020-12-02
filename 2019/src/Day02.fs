module Day02

open System

type Address = int
type Pointer = int
type ExecutableInstruction = {Op:Pointer;NounAddress:Address;VerbAddress:Address;OutputPointer:Pointer;} 
type Program = {Instruction:ExecutableInstruction; Memory:int[];IsHalted:bool}

let private read (memory:int[]) (address:Address) =
    let pointer = memory.[address]
    memory.[pointer]

let private readNoun program =
    read program.Memory program.Instruction.NounAddress

let private readVerb program =
    read program.Memory program.Instruction.VerbAddress

let private readOutput program =
    program.Memory.[program.Instruction.OutputPointer]

let private readCurrentIntructionCode program = 
    program.Memory.[program.Instruction.Op]

let private createProgram opPointer memory isHalted=
    {
        Instruction = {Op=opPointer;NounAddress = opPointer+1;VerbAddress = opPointer+2;OutputPointer = opPointer+3;}
        Memory=memory;
        IsHalted=isHalted 
    }

let private run program = 
    let updatedMemory = Array.copy program.Memory
    let executable = 
                match readCurrentIntructionCode program with
                |1 -> Some((+),readNoun program, readVerb program, readOutput program)
                |2 -> Some((*),readNoun program, readVerb program, readOutput program)
                |_ -> None
    
    match executable with
    |None -> (program.Instruction.Op,program.Memory,true) |||>createProgram
    |Some (fn, noun, verb, output) -> updatedMemory.[output]<- fn verb noun
                                      (program.Instruction.Op+4,updatedMemory,false) |||> createProgram 


let private mainLoop memory =
      let mutable next = (0,memory,false) |||> createProgram |> run
      while not next.IsHalted do
        next <- run next
      next.Memory

let RunProgram input=
    input |> mainLoop