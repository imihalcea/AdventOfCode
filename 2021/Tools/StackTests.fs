module _2021.Tools.StackTests

open Xunit
open Swensen.Unquote

[<Fact>]    
let ``stack push``()=
   let result = [1;2] |> List.fold (fun (s:Stack<int>) -> s.Push) (Stack.Empty) 
   test <@ result.Enumerate()|> Seq.toList = [2;1]@>


[<Fact>]    
let ``stack peak``()=
   let st = [1;2] |> List.fold (fun (s:Stack<int>) -> s.Push) (Stack.Empty)
   test <@ st.Peak() = 2  @>
   test <@ st.Enumerate()|> Seq.toList = [2;1]@>


[<Fact>] 
let ``stack pop``()=
   let original = [1;2] |> List.fold (fun (s:Stack<int>) -> s.Push) (Stack.Empty)
   let (head, remaining) = original.Pop()
   test <@ head = 2  @>
   test <@ remaining.Enumerate()|> Seq.toList =   [1]   @>
   test <@ original.Enumerate() |> Seq.toList = [2;1]   @>