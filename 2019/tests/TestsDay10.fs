module TestsDay10
open System
open Xunit
open Day10
open System.Numerics

[<Fact>]
let ``should load asteroid map``()=
    let inputLines = [|".#..#";".....";"#####";"....#";"...##"|]
    let expected = [| Vector2(1.0f, 0.0f); Vector2(4.0f, 0.0f); Vector2(0.0f, 2.0f); Vector2(1.0f, 2.0f); Vector2(2.0f, 2.0f); Vector2(3.0f, 2.0f); Vector2(4.0f, 2.0f); Vector2(4.0f, 3.0f); Vector2(3.0f, 4.0f); Vector2(4.0f, 4.0f) |]
    let result = AsteroidGrid.create inputLines
    Assert.Equal<Vector2>(expected,result)

[<Theory>]
[<InlineData(1.0f, 0.0f,7)>]
[<InlineData(4.0f, 0.0f,7)>]
[<InlineData(0.0f, 2.0f,6)>]
[<InlineData(1.0f, 2.0f,7)>]
[<InlineData(2.0f, 2.0f,7)>]
[<InlineData(3.0f, 2.0f,7)>]
[<InlineData(4.0f, 2.0f,5)>]
[<InlineData(4.0f, 3.0f,7)>]
[<InlineData(3.0f, 4.0f,8)>]
[<InlineData(4.0f, 4.0f,7)>]
let ``compute number of asteroids detected from position`` (pX:single) (pY:single) (expected:int)=
    let inputLines = [|".#..#";".....";"#####";"....#";"...##"|]
    let position = Vector2(pX,pY)
    let grid = inputLines |> AsteroidGrid.create
    let result = AsteroidGrid.detect (position,grid) |> Seq.length
    Assert.Equal(result, expected)

[<Fact>]
let ``vaporize``()=
    let inputLines = [|".#....#####...#..";"##...##.#####..##";"##...#...#.#####.";"..#.....#...###..";"..#.#.....#....##"|]
    let position = Vector2(8.0f,3.0f)
    let expected = [(8,1);(9,0);(9,1);(10,0);(9,2);(11,1);(12,1);(11,2);(15,1);(12,2);(13,2);(14,2);(15,2);(12,3);(16,4);(15,4);(10,4);(4,4);(2,4);(2,3);(0,2);(1,2);(0,1);(1,1);(5,2);(1,0);(5,1)]
    let expectedResults = List.map (fun (x,y) -> Vector2(float32(x),float32(y))) expected
    let grid = inputLines |> AsteroidGrid.create
    let result = AsteroidGrid.vaporize 27 (position,grid)
    Assert.Equal<Vector2>(expectedResults,result)