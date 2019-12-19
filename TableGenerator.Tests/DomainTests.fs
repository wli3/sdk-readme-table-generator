module TableGenerator.Tests.DomainTests

open FsUnit
open Xunit
open System.IO
open NuGet.Versioning
open System

//[<Fact>]
//let ``it can parser the json``() =
//    let deserializeResult = deserialize (File.ReadAllText("pageJsonSample.json"))
//    match (Seq.first deserializeResult.Items) with
//    | Some x ->
//        let { PackageId = packageid; PackageVersion = _ } = x
//        packageid |> should equal "MassTransit.CastleWindsor"
//    | None -> failwith "non item"
