module _2022.Day07.Solution

open System
open System.Text.RegularExpressions
type IFs =
    abstract Size:int
    abstract Name:string

type FileInfo(name:string, size:int) =
    interface IFs with
        member s.Size = size
        member s.Name = name
        
type DirInfo(parent:DirInfo option, name:string) =
    let mutable items:IFs list = [] 

    interface IFs with
        member s.Name = name
        member s.Size = items |> List.map (fun it -> it.Size) |> List.sum

    member s.Items = items
    member s.Parent = parent

    member s.AddItem(item:IFs):DirInfo=
        items <- items @ [item]
        s
    member s.Cd(dirName:string):DirInfo =
        match dirName with
        |".." -> match parent with
                |None -> failwith "you are at the root"
                |Some(dir) -> dir
        |_ -> s.Items |> List.find (fun it -> it.Name=dirName) :?> DirInfo
    
    member s.ListDirs() =
        s.Items
        |> List.filter (fun it -> it :? DirInfo)
        |> List.map (fun it -> it :?> DirInfo)
    
    member s.Explore(acc:System.Collections.Generic.List<DirInfo>, sizePredicate:int->bool) =
        for d in s.ListDirs() do
            if (sizePredicate (d:>IFs).Size) then
                acc.Add(d)
            d.Explore(acc, sizePredicate)
    
       
            

type InputLine =
    |CD of string
    |LS
    |DIR of string
    |FILE of int * string

type Input = InputLine list

let parseLine (line:string):InputLine =
    let cdParser = Regex("^\$\s*cd\s+(\S+)$")
    let dirParser = Regex("^dir\s+(\S+)$")
    let lsParser = Regex("^\$\s*ls")
    let fileParser = Regex("^(\d+)\s+(\S+)$")
    if cdParser.IsMatch(line) then
        let m = cdParser.Match(line)
        CD(m.Groups[1].Value)
    else if dirParser.IsMatch(line) then
        let m = dirParser.Match(line)
        DIR(m.Groups[1].Value)
    else if lsParser.IsMatch(line) then
        LS
    else if fileParser.IsMatch(line) then
        let m = fileParser.Match(line)
        FILE(Convert.ToInt32(m.Groups[1].Value), m.Groups[2].Value)
    else
        failwith "no paser matched"


let processLine (dir:DirInfo) (l:InputLine) :DirInfo =
    match l with
    |DIR name -> dir.AddItem (DirInfo(Some(dir), name))  
    |CD dirName ->dir.Cd dirName
    |LS -> dir
    |FILE(size, name) -> dir.AddItem (FileInfo(name,size))

let findDirectories (root:DirInfo) (predicate:int -> bool) =
    let acc = System.Collections.Generic.List<DirInfo>()
    root.Explore(acc, predicate)
    List.ofSeq acc

let part1 (data:string seq):int =
    let root = (DirInfo(None,"/"))
    data
    |> Seq.skip 1
    |> Seq.map parseLine
    |> Seq.fold processLine root |> ignore
    
    findDirectories root (fun x -> x<=100000)
    |> List.map (fun it -> (it:>IFs).Size)
    |> List.sum
    
let part2 (data:string seq):int =
    let root = (DirInfo(None,"/"))
    data
    |> Seq.skip 1
    |> Seq.map parseLine
    |> Seq.fold processLine root |> ignore
    
    
    
    let tofree = 30000000 - (70000000 - (root :> IFs).Size)
    findDirectories root (fun x ->  x >= tofree)
    |> List.map (fun it -> (it:>IFs).Size)
    |> List.sort
    |> List.head