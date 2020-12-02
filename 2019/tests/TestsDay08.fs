module TestsDay08

open System
open Xunit
open Day08

[<Fact>]
let ``create an image``() = 
    let img = SpaceImage.create 3 2 |> SpaceImage.load "123456789012"
    Assert.Equal(2, img.Content.Length)
    Assert.Equal<int>([1;2;3;4;5;6], img.Content.[0])
    Assert.Equal<int>([7;8;9;0;1;2], img.Content.[1])

[<Fact>]
let ``identifiy layer with fewer zero``()=
    let img = SpaceImage.create 3 2 |> SpaceImage.load "123456789012"
    let layerWithFewerZero =  img.Content |> Layer.layerWithFewer 0
    Assert.Equal<int>([1;2;3;4;5;6], layerWithFewerZero)

[<Fact>]
let ``count digit in layer``()=
    let img = SpaceImage.create 3 2 |> SpaceImage.load "122236789012"
    Assert.Equal(3, (Layer.countDigit 2 img.Content.[0]))

[<Fact>]
let ``decode image 1``()=
    let img = SpaceImage.create 2 2 |> SpaceImage.load "1212110002111211"
    let decodedImage = (SpaceImage.decode img)
    Assert.Equal<int>([1;1;1;0],decodedImage.Pixels)

[<Fact>]
let ``decode image 2``()=
    let img = SpaceImage.create 2 2 |> SpaceImage.load "0222112222120000"
    let decodedImage = (SpaceImage.decode img)
    Assert.Equal<int>([0;1;1;0],decodedImage.Pixels)