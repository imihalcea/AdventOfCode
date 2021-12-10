module _2021.Day10.Tests

open System.IO
open Swensen.Unquote
open Xunit
open _2021.Day10.Solution

[<Fact>]
let ``example corrupted``()=
   test <@ scan "{([(<{}[<>[]}>{[]{[(<()>" = Error(Close CurlyBrace)  @>
   test <@ scan "[[<[([]))<([[{}[[()]]]"   = Error(Close Parenthesis) @>
   test <@ scan "[{[{({}]{}}([{[{{{}}([]"  = Error(Close Bracket)     @>
   test <@ scan "[<(<(<(<{}))><([]([]()"   = Error(Close Parenthesis) @>
   test <@ scan "<{([([[(<>()){}]>(<<{{"   = Error(Close Chevron)     @>
   
[<Fact>]   
let ``part1``()=
   let r = File.ReadLines "Day10/input.txt" |> part1
   test <@ r = 370407  @>
   
[<Fact>]   
let ``part2``()=
   let r = File.ReadLines "Day10/input.txt" |> part2
   test <@ r = 3249889609L  @>