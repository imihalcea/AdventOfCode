module Day12
open System.Numerics
open System
open MathNet.Numerics;

type Moon(position:Vector3, ?velocity:Vector3) =
    let mutable _position = position
    let mutable _velocity = defaultArg velocity Vector3.Zero

    member this.Velocity
        with get()=_velocity
    
    member this.Position
        with get()=_position
    
    member this.PotentialEnergy
        with get()=Math.Abs(this.Position.X) + Math.Abs(this.Position.Y) + Math.Abs(this.Position.Z)

    member this.KineticEnergy
        with get()=Math.Abs(this.Velocity.X) + Math.Abs(this.Velocity.Y) + Math.Abs(this.Velocity.Z)
    
    member this.TotalEnergy
        with get()=this.PotentialEnergy * this.KineticEnergy 
    
    static member private ComputeAxisVelocity(axis1:float32, axis2:float32, velocity:float32):float32=
        match axis1,axis2 with
        |v1,v2 when v1>v2 -> velocity-1.0f
        |v1,v2 when v1<v2 -> velocity+1.0f
        |_,_ -> velocity

    member this.ApplyGravity(other:Moon):unit =
            _velocity.X <- Moon.ComputeAxisVelocity(this.Position.X,other.Position.X,this.Velocity.X)
            _velocity.Y <- Moon.ComputeAxisVelocity(this.Position.Y,other.Position.Y,this.Velocity.Y)
            _velocity.Z <- Moon.ComputeAxisVelocity(this.Position.Z,other.Position.Z,this.Velocity.Z)
    
    member this.ApplyVelocity():unit =
        _position <- this.Position+this.Velocity
    
    static member ArrangePairs(moons:seq<Moon>) =
        moons |>
        Seq.collect (fun m -> moons |> Seq.except [m]|> Seq.map (fun other -> (m,other)))
    
    member this.Copy():Moon=
        Moon(this.Position,this.Velocity)

module Simulator=
    let applyGravity (m:Moon) (moons:seq<Moon>)=
        moons |> Seq.except [m] |> Seq.iter (fun other -> m.ApplyGravity(other))
    
    let run (moons:Moon[]) =
        moons |> Array.iter (fun m -> applyGravity m moons)
        moons |> Array.iter (fun m -> m.ApplyVelocity())

    let runN (n:int) (moons:Moon[]) =
        [1..n] |> List.iter (fun _ -> run moons)
    
    let private snapshotMoons (moons:Moon[]) = 
        moons |> Array.map (fun m -> m.Copy())

    let private runUntil (moons:Moon[]) (fStopCondition:Moon[]->bool)=
        let snapshot = snapshotMoons moons 
        let mutable cnt = 2L;
        do run snapshot
        while not (fStopCondition snapshot) do
            do run snapshot
            cnt<-cnt+1L
        cnt
    
    let private arePositionsEq (projection:Moon->float32) (initial:Moon[]) (actual:Moon[])=
        initial 
        |> Array.mapi (fun i _ -> projection(initial.[i]) = projection(actual.[i]))
        |> Array.reduce (&&)
   
    
    let runsToRealignment (moons:Moon[])=
        let snapshot = snapshotMoons moons
        let axisConditions = [|
                (fun (initialMoons:Moon[]) (actualMoons:Moon[]) -> arePositionsEq (fun (m:Moon) -> m.Position.X) initialMoons actualMoons);
                (fun (initialMoons:Moon[]) (actualMoons:Moon[]) -> arePositionsEq (fun (m:Moon) -> m.Position.Y) initialMoons actualMoons);
                (fun (initialMoons:Moon[]) (actualMoons:Moon[]) -> arePositionsEq (fun (m:Moon) -> m.Position.Z) initialMoons actualMoons);
            |]
        axisConditions 
          |> Array.map (fun condition -> (runUntil moons (condition snapshot))) 
          |> Euclid.LeastCommonMultiple