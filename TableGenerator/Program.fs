module TableGenerator.App

open System
open TableGenerator.Shared
open TableGenerator.Reference
open TableGenerator.Table

let inputBranches =
        [ { GitBranchName = "master"
            DisplayName = "Master<br>(5.0.x&nbsp;Runtime)"
            AkaMsChannel = Some("net5/dev") }
          { GitBranchName = "release/5.0.1xx-preview5"
            DisplayName = "5.0.100 Preview 5<br>(5.0 Runtime)"
            AkaMsChannel = Some("net5/preview5") }
          { GitBranchName = "release/5.0.1xx-preview4"
            DisplayName = "5.0.100 Preview 4<br>(5.0 Runtime)"
            AkaMsChannel = Some("net5/preview4") }
          { GitBranchName = "release/3.1.4xx"
            DisplayName = "Release/3.1.4XX<br>(3.1.x Runtime)"
            AkaMsChannel = None }
          { GitBranchName = "release/3.1.3xx"
            DisplayName = "Release/3.1.3XX<br>(3.1.x Runtime)"
            AkaMsChannel = None }
          { GitBranchName = "release/3.1.2xx"
            DisplayName = "Release/3.1.2XX<br>(3.1.x Runtime)"
            AkaMsChannel = None }
          { GitBranchName = "release/3.1.1xx"
            DisplayName = "Release/3.1.1XX<br>(3.1.x Runtime)"
            AkaMsChannel = None }
          { GitBranchName = "release/3.0.1xx"
            DisplayName = "Release/3.0.1xx<br>(3.0.x Runtime)"
            AkaMsChannel = None } ]
let inputBranches2x =
    [ { GitBranchName = "release/2.1.8xx"
        DisplayName = "Release/2.1.8XX<br>(2.1.x Runtime)"
        AkaMsChannel = None }
      { GitBranchName = "release/2.1.6xx"
        DisplayName = "Release/2.1.6XX<br>(2.1.6 Runtime)"
        AkaMsChannel = None }
      { GitBranchName = "release/2.1.5xx"
        DisplayName = "Release/2.1.5XX<br>(2.1.5 Runtime)"
        AkaMsChannel = None } ]

let referentNotes = """Reference notes:
> **1**: Our Debian packages are put together slightly differently than the other OS specific installers. Instead of combining everything, we have separate component packages that depend on each other. If you're installing the SDK from the .deb file (via dpkg or similar), then you'll need to install the corresponding dependencies first:
> * [Host, Host FX Resolver, and Shared Framework](https://github.com/dotnet/runtime#daily-builds)
> * [ASP.NET Core Shared Framework](https://github.com/aspnet/AspNetCore/blob/master/docs/DailyBuilds.md)

.NET Core SDK 2.x downloads can be found here: [.NET Core SDK 2.x Installers and Binaries](Downloads2.x.md)"""

let sdksha2 =
    "[sdk-shas-2.2.1XX]: https://github.com/dotnet/versions/tree/master/build-info/dotnet/product/cli/release/2.2#built-repositories"

let wholeTable branches =
    String.Join
        (Environment.NewLine + Environment.NewLine,
         [ table branches
           referentNotes
           referenceList branches
           sdksha2 ])

[<EntryPoint>]
let main argv =
    Console.WriteLine(wholeTable inputBranches)
    0
