module Day04

open System

type State =
    |Inital
    |Valid of int*int[]
    |Invaild

let private digits input =
    seq{
          let mutable r = input
          while r>0 do
              yield r%10
              r<-r/10
    }|> Seq.toArray |> Array.rev

let private incrementGroupCount digit (groups:int[])=
    groups.[digit]<-groups.[digit]+1
    groups

let private transitionFromInital input =
    let groups = Array.init 10 (fun i -> 0)
    Valid(input,((input,groups)||>incrementGroupCount))

let transitionFromValid input previousDigit groups =
    match previousDigit with
    |p when input<p -> Invaild
    |p -> Valid(input, ((input,groups)||>incrementGroupCount))

let private transition previousState input=
    match previousState with
    |Inital -> transitionFromInital input
    |Valid(previousDigit,groups) -> (input,previousDigit,groups) |||> transitionFromValid
    |Invaild -> Invaild

let private isMatching condition state=
    match state with
    |Valid(_,groups) -> groups |> Array.exists condition
    |_->false

let CheckPassword (condition:int->bool) =
    fun password ->
        password
        |> digits
        |> Array.fold transition Inital
        |> isMatching condition

let part1Condition=(fun n->n>=2)
let part2Condition=(fun n->n=2) 

let HowManyDifferentPasswordsPart1 inf sup = 
    seq{inf..sup} 
    |> Seq.filter (CheckPassword part1Condition)
    |> Seq.length

let HowManyDifferentPasswordsPart2 inf sup = 
    seq{inf..sup} 
    |> Seq.filter (CheckPassword part2Condition)
    |> Seq.length