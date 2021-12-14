module _2021.Day12.Tests

open System
open System.IO
open Xunit
open _2021.Day12.Solution
open Swensen.Unquote

let buildGraph (lines:string[]) =
    lines
    |> Seq.map (fun line -> line.Split('-', StringSplitOptions.RemoveEmptyEntries))
    |> Seq.fold (fun g o -> Undirected.addEdge g (Vertex.Create(o.[0]), Vertex.Create(o.[1]))) Graph.empty 

let parseInput path = File.ReadAllLines(path) |> buildGraph


[<Fact>]
let ``example part 1``()=
    let graph = buildGraph [|
        "start-A"
        "start-b"
        "A-c"
        "A-b"
        "b-d"
        "A-end"
        "b-end"
    |]
    
    let paths = Graph.findPaths graph smallCaveatMostOnce|> Seq.toList
    test <@ List.contains [Start; Big "A"; End ] paths  @>
    test <@ List.contains [Start; Big "A"; Small("b");  End ] paths  @>
    test <@ List.contains [Start; Big "A"; Small("b");  Big("A"); End ] paths  @>
    test <@ List.contains [Start; Big "A"; Small("c");  Big("A"); End ] paths  @>
    test <@ List.contains [Start; Big "A"; Small("b"); Big("A"); Small("c"); Big("A"); End ] paths  @>
    test <@ paths.Length = 10 @>    

[<Fact>]
let ``part 1``()=
    let r = parseInput "Day12/input.txt" |> Solution.part1
    test <@ r = 4549 @>



[<Fact>]
let ``example part 2``()=
    let graph = buildGraph [|
        "start-A"
        "start-b"
        "A-c"
        "A-b"
        "b-d"
        "A-end"
        "b-end"
    |]
    
    let paths = Graph.findPaths graph singleSmallCaveTwice |> Seq.toList
    test <@ List.contains [Start; Big("A"); End ] paths  @>
    test <@ List.contains [Start; Big("A"); Small("b");  End ] paths  @>
    test <@ List.contains [Start; Big("A"); Small("b");  Big("A"); End ] paths  @>
    test <@ List.contains [Start; Big("A"); Small("c");  Big("A"); End ] paths  @>
    test <@ List.contains [Start; Big("A"); Small("b"); Big("A"); Small("c"); Big("A"); End ] paths  @>
    test <@ List.contains [Start; Big("A"); Small("c"); Big("A"); Small("b"); Small("d"); Small("b"); Big("A"); End ] paths  @>
    test <@ paths.Length = 36 @>  


[<Fact>]
let ``part 2``()=
    let r = parseInput "Day12/input.txt" |> Solution.part2
    test <@ r = 120535 @>