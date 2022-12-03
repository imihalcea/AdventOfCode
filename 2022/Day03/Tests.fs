module _2022.Day03.TestsDay3

open System
open System.IO
open Xunit
open Swensen.Unquote
open Solution

[<Fact>]
let ``example1``()=
    let data = """vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw
"""
    let r = Solution.checkRucksack("vJrwpWtwJgWrhcsFMMfFFhFp")
    test <@ r = [16] @>    

[<Fact>]
let ``example2``()=
    let data = """vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw"""
    let r = Solution.part2(data)
    test <@ r = 70 @>   

[<Fact>]
let ``Part 1`` () =
    let data =File.ReadAllText("Day03/input.txt")
    let result = part1 data
    test  <@ result = 7831  @>

[<Fact>]
let ``Part 2`` () =
    let data =File.ReadAllText("Day03/input.txt")
    let result = part2 data
    test  <@ result = 7831  @>
    