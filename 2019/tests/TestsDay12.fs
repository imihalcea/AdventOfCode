module TestsDay12
open System
open Xunit
open Day12
open System.Numerics

[<Fact>]
let ``simulator step one``()=
    let moons = [|Moon(Vector3(-1.0f,0.0f,2.0f));
                 Moon(Vector3(2.0f,-10.0f,-7.0f));
                 Moon(Vector3(4.0f,-8.0f,8.0f));
                 Moon(Vector3(3.0f,5.0f,-1.0f))|]
    let expectedStepOne = [Vector3(2.0f,-1.0f,1.0f);
                           Vector3(3.0f,-7.0f,-4.0f);
                           Vector3(1.0f,-7.0f,5.0f);
                           Vector3(2.0f,2.0f,0.0f)]
    do Simulator.run moons 
    let afterStepOne = moons |> Seq.map (fun m -> m.Position) |> Seq.toList
    Assert.Equal<Vector3>(expectedStepOne,afterStepOne)

[<Fact>]
let ``simulator 10 steps``()=
    let moons = [|Moon(Vector3(-1.0f,0.0f,2.0f));
                 Moon(Vector3(2.0f,-10.0f,-7.0f));
                 Moon(Vector3(4.0f,-8.0f,8.0f));
                 Moon(Vector3(3.0f,5.0f,-1.0f))|]
    let expected = [|Vector3(2.0f,1.0f,-3.0f);
                    Vector3(1.0f,-8.0f,0.0f);
                    Vector3(3.0f,-6.0f,1.0f);
                    Vector3(2.0f,0.0f,4.0f)|]
    do Simulator.runN 10 moons  
    let result = moons |> Seq.map (fun m -> m.Position) |> Seq.toList
    Assert.Equal<Vector3>(expected,result)

[<Fact>]
let ``simulator energy after 10 runs``()=
    let moons = [|Moon(Vector3(-1.0f,0.0f,2.0f));
                 Moon(Vector3(2.0f,-10.0f,-7.0f));
                 Moon(Vector3(4.0f,-8.0f,8.0f));
                 Moon(Vector3(3.0f,5.0f,-1.0f))|]
    do Simulator.runN 10 moons
    let result = moons |> Seq.map (fun m -> m.TotalEnergy) |> Seq.toList
    Assert.Equal<float32>([36.0f;45.0f;80.0f;18.0f],result)

[<Fact>]
let ``run to realignment``()=
        let moons = [|Moon(Vector3(-1.0f,0.0f,2.0f));
                     Moon(Vector3(2.0f,-10.0f,-7.0f));
                     Moon(Vector3(4.0f,-8.0f,8.0f));
                     Moon(Vector3(3.0f,5.0f,-1.0f))|]
        let numberOfRuns = Simulator.runsToRealignment moons
        Assert.Equal<int64>(2772L,numberOfRuns)

[<Fact>]
let ``part1``()=
    let moons = [|Moon(Vector3(15.0f,-2.0f,-6.0f));
                 Moon(Vector3(-5.0f,-4.0f,-11.0f));
                 Moon(Vector3(0.0f,-6.0f,0.0f));
                 Moon(Vector3(5.0f,9.0f,6.0f))|]
    do Simulator.runN 1000 moons
    let result = moons |> Seq.map (fun m -> m.TotalEnergy) |> Seq.sum
    Assert.Equal<float32>(6735.0f,result)

[<Fact>]
let ``part2``()=
    let moons = [|Moon(Vector3(15.0f,-2.0f,-6.0f));
                 Moon(Vector3(-5.0f,-4.0f,-11.0f));
                 Moon(Vector3(0.0f,-6.0f,0.0f));
                 Moon(Vector3(5.0f,9.0f,6.0f))|]
    let numberOfRuns = Simulator.runsToRealignment moons
    Assert.Equal<int64>(326489627728984L,numberOfRuns)
