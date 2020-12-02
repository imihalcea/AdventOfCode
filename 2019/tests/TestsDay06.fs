module TestsDay06

open System
open Xunit
open Day06

let private encode (path:Vertex list)=
    path
    |> List.map (fun c -> c.ToString())
    |> List.reduce (fun agg n -> sprintf "%s->%s" agg n)

[<Fact>]
let ``build undirected graph``() =
    let g = [("A","B");("B","C");("B","D");("B","E");("C","D");("E","D");("A","E")]
            |> List.fold Undirected.addEdge Graph.empty
    Assert.Equal<string>(["A";"B";"C";"D";"E"],(vertices g))
    Assert.Equal(5,g.Count)
    Assert.Equal<Vertex>(["B";"E"],(neighborsOf "A" g))
    Assert.Equal<Vertex>(["A";"C";"D";"E"],(neighborsOf "B" g))
    Assert.Equal<Vertex>(["B";"D"],(neighborsOf "C" g))
    Assert.Equal<Vertex>(["B";"C";"E"],(neighborsOf "D" g))
    Assert.Equal<Vertex>(["B";"D";"A"],(neighborsOf "E" g))

[<Fact>]
let ``build directed graph``() =
    let g = [("A","B");("B","C");("B","D");("B","E");("C","D");("E","D");("A","E")]
            |> List.fold Directed.addEdge Graph.empty
    Assert.Equal<string>(["A";"B";"C";"D";"E"],(vertices g))
    Assert.Equal(5,g.Count)
    Assert.Equal<Vertex>(["B";"E"],(neighborsOf "A" g))
    Assert.Equal<Vertex>(["C";"D";"E"],(neighborsOf "B" g))
    Assert.Equal<Vertex>(["D"],(neighborsOf "C" g))
    Assert.Equal<Vertex>([],(neighborsOf "D" g))
    Assert.Equal<Vertex>(["D"],(neighborsOf "E" g)) 

[<Theory>]
[<InlineData("o)A",1)>]
[<InlineData("o)A|A)B",3)>]
[<InlineData("o)A|A)B|A)C|A)D|A)E",9)>]
[<InlineData("o)B|B)C|C)D|D)E|E)F|B)G|G)H|D)I|E)J|J)K|K)L",42)>]
let ``part1`` (input:string) (expectedOutput:int)=
    let result = input.Split("|") 
                    |> Array.map (fun strOrbit -> strOrbit.Split(")"))
                    |> Array.fold (fun g o -> Directed.addEdge g (o.[1],o.[0])) Graph.empty
                    |> Orbit.countOrbitsTo "o"
    Assert.Equal(expectedOutput, result)

[<Theory>]
[<InlineData("o)A|A)B","B","o","B->A->o")>]
[<InlineData("o)A|A)B|B)C","B","o","B->A->o")>]
[<InlineData("o)A|A)B|A)C","C","o","C->A->o")>]
[<InlineData("o)A|A)B|B)C|A)C","C","o","C->A->o")>]
[<InlineData("A)B|A)D|B)C|D)B|D)E|E)B|E)C","A","E","A->B->E")>]
[<InlineData("o)B|B)C|C)D|D)E|E)F|B)G|G)H|D)I|E)J|J)K|K)L","K","I","K->J->E->D->I")>]
let ``find shortest path`` (input:string) sFrom sTo expectedOutput=
    let result = input.Split("|") 
                 |> Array.map (fun strOrbit -> strOrbit.Split(")"))
                 |> Array.fold (fun g o -> Undirected.addEdge g (o.[0],o.[1])) Graph.empty
                 |> Orbit.shortestPath sFrom sTo
                 |> List.rev
                 |> encode
    Assert.Equal(expectedOutput,result)


