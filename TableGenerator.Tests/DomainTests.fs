module TableGenerator.Tests.DomainTests

open TableGenerator.Shared
open TableGenerator.Reference
open TableGenerator.Table
open TableGenerator.App

open FsUnit
open Xunit
open System

[<Fact>]
let ``it can Shorten master branch Name``() =
    let branch =
        { GitBranchName = "master"
          DisplayName = "Master<br>(5.0.x&nbsp;Runtime)"
          AkaMsChannel = Some("net5/dev") }
    branchNameShorten branch |> should equal "master"

[<Fact>]
let ``it can Shorten releae branch Name``() =
    let branch =
        { GitBranchName = "release/3.1.1xx"
          DisplayName = "Release/3.1.1XX<br>(3.1.x Runtime)"
          AkaMsChannel = None}
    branchNameShorten branch |> should equal "3.1.1XX"

[<Fact>]
let ``it can get major and minor version of a branch``() =
    let branch =
        { GitBranchName = "release/3.1.1xx"
          DisplayName = "Release/3.1.1XX<br>(3.1.x Runtime)"
          AkaMsChannel = None}
    getMajorMinor branch
    |> should equal
           (MajorMinor
               ({ Major = 3
                  Minor = 1 }))
           
[<Fact>]
let ``it can get major and minor version of a preview branch``() =
    let branch =
      { GitBranchName = "release/5.0.1xx-preview2"
        DisplayName = "5.0.100 Preview 2<br>(5.0 Runtime)"
        AkaMsChannel = Some("net5/preview2") }
    getMajorMinor branch
    |> should equal
           (MajorMinor
               ({ Major = 5
                  Minor = 0 }))

[<Fact>]
let ``it can get master version of a master branch``() =
    let branch =
        { GitBranchName = "master"
          DisplayName = "Master<br>(5.0.x&nbsp;Runtime)"
          AkaMsChannel = None}
    getMajorMinor branch |> should equal Master

[<Fact>]
let ``it can get bad branch version``() =
    let branch =
        { GitBranchName = "badbranch"
          DisplayName = ""
          AkaMsChannel = None}
    getMajorMinor branch |> should equal NoVersion

[<Fact>]
let ``it can format winx64Reference``() =
    let branch =
        { GitBranchName = "release/3.1.1xx"
          DisplayName = "Release/3.1.1XX<br>(3.1.x Runtime)"
          AkaMsChannel = None}
    (winX64ReferenceTemplate branch).Value
    |> should equal
           """[win-x64-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/win_x64_Release_version_badge.svg
[win-x64-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[win-x64-installer-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.zip.sha"""

[<Fact>]
let ``it can format ReferenceWithAkaMslink``() =
    let branch =
        { GitBranchName = "release/5.0.1xx-preview2"
          DisplayName = "5.0.100 Preview 2<br>(5.0 Runtime)"
          AkaMsChannel = Some("net5/preview2") }

    (formatTemplate "win-x64" referenceTemplate branch).Value
    |> should equal
           """[win-x64-badge-5.0.1XX-preview2]: https://aka.ms/dotnet/net5/preview2/Sdk/win_x64_Release_version_badge.svg
[win-x64-version-5.0.1XX-preview2]: https://aka.ms/dotnet/net5/preview2/Sdk/productCommit-win-x64.txt
[win-x64-installer-5.0.1XX-preview2]: https://aka.ms/dotnet/net5/preview2/Sdk/dotnet-sdk-win-x64.exe
[win-x64-installer-checksum-5.0.1XX-preview2]: https://aka.ms/dotnet/net5/preview2/Sdk/dotnet-sdk-win-x64.exe.sha
[win-x64-zip-5.0.1XX-preview2]: https://aka.ms/dotnet/net5/preview2/Sdk/dotnet-sdk-win-x64.zip
[win-x64-zip-checksum-5.0.1XX-preview2]: https://aka.ms/dotnet/net5/preview2/Sdk/dotnet-sdk-win-x64.zip.sha"""


let branches =
    [ { GitBranchName = "master"
        DisplayName = "Master<br>(5.0.x&nbsp;Runtime)"
        AkaMsChannel = None}
      { GitBranchName = "release/3.1.1xx"
        DisplayName = "Release/3.1.1XX<br>(3.1.x Runtime)"
        AkaMsChannel = None}
      { GitBranchName = "release/3.0.1xx"
        DisplayName = "Release/3.0.1xx<br>(3.0.x Runtime)"
        AkaMsChannel = None}
      { GitBranchName = "release/2.2.2xx"
        DisplayName = "Release/2.2.2XX<br>(2.2.x Runtime)"
        AkaMsChannel = None}
      { GitBranchName = "release/2.2.1xx"
        DisplayName = "Release/2.2.1XX<br>(2.2.x Runtime)"
        AkaMsChannel = None}
      { GitBranchName = "release/2.1.6xx"
        DisplayName = "Release/2.1.6XX<br>(2.1.x Runtime)"
        AkaMsChannel = None}
      { GitBranchName = "release/2.1.5xx"
        DisplayName = "Release/2.1.5XX<br>(2.1.x Runtime)"
        AkaMsChannel = None} ]

[<Fact>]
let ``it can generate WindowsX64Row``() =
    windowsX64Row branches
    |> should equal
           "| **Windows x64** | [![][win-x64-badge-master]][win-x64-version-master]<br>[Installer][win-x64-installer-master] - [Checksum][win-x64-installer-checksum-master]<br>[zip][win-x64-zip-master] - [Checksum][win-x64-zip-checksum-master] | [![][win-x64-badge-3.1.1XX]][win-x64-version-3.1.1XX]<br>[Installer][win-x64-installer-3.1.1XX] - [Checksum][win-x64-installer-checksum-3.1.1XX]<br>[zip][win-x64-zip-3.1.1XX] - [Checksum][win-x64-zip-checksum-3.1.1XX] | [![][win-x64-badge-3.0.1XX]][win-x64-version-3.0.1XX]<br>[Installer][win-x64-installer-3.0.1XX] - [Checksum][win-x64-installer-checksum-3.0.1XX]<br>[zip][win-x64-zip-3.0.1XX] - [Checksum][win-x64-zip-checksum-3.0.1XX] | [![][win-x64-badge-2.2.2XX]][win-x64-version-2.2.2XX]<br>[Installer][win-x64-installer-2.2.2XX] - [Checksum][win-x64-installer-checksum-2.2.2XX]<br>[zip][win-x64-zip-2.2.2XX] - [Checksum][win-x64-zip-checksum-2.2.2XX] | [![][win-x64-badge-2.2.1XX]][win-x64-version-2.2.1XX]<br>[Installer][win-x64-installer-2.2.1XX] - [Checksum][win-x64-installer-checksum-2.2.1XX]<br>[zip][win-x64-zip-2.2.1XX] - [Checksum][win-x64-zip-checksum-2.2.1XX] | [![][win-x64-badge-2.1.6XX]][win-x64-version-2.1.6XX]<br>[Installer][win-x64-installer-2.1.6XX] - [Checksum][win-x64-installer-checksum-2.1.6XX]<br>[zip][win-x64-zip-2.1.6XX] - [Checksum][win-x64-zip-checksum-2.1.6XX] | [![][win-x64-badge-2.1.5XX]][win-x64-version-2.1.5XX]<br>[Installer][win-x64-installer-2.1.5XX] - [Checksum][win-x64-installer-checksum-2.1.5XX]<br>[zip][win-x64-zip-2.1.5XX] - [Checksum][win-x64-zip-checksum-2.1.5XX] |"

[<Fact>]
let ``it can generate OsxRow``() =
    osxRow branches
    |> should equal
           "| **macOS** | [![][osx-badge-master]][osx-version-master]<br>[Installer][osx-installer-master] - [Checksum][osx-installer-checksum-master]<br>[tar.gz][osx-targz-master] - [Checksum][osx-targz-checksum-master] | [![][osx-badge-3.1.1XX]][osx-version-3.1.1XX]<br>[Installer][osx-installer-3.1.1XX] - [Checksum][osx-installer-checksum-3.1.1XX]<br>[tar.gz][osx-targz-3.1.1XX] - [Checksum][osx-targz-checksum-3.1.1XX] | [![][osx-badge-3.0.1XX]][osx-version-3.0.1XX]<br>[Installer][osx-installer-3.0.1XX] - [Checksum][osx-installer-checksum-3.0.1XX]<br>[tar.gz][osx-targz-3.0.1XX] - [Checksum][osx-targz-checksum-3.0.1XX] | [![][osx-badge-2.2.2XX]][osx-version-2.2.2XX]<br>[Installer][osx-installer-2.2.2XX] - [Checksum][osx-installer-checksum-2.2.2XX]<br>[tar.gz][osx-targz-2.2.2XX] - [Checksum][osx-targz-checksum-2.2.2XX] | [![][osx-badge-2.2.1XX]][osx-version-2.2.1XX]<br>[Installer][osx-installer-2.2.1XX] - [Checksum][osx-installer-checksum-2.2.1XX]<br>[tar.gz][osx-targz-2.2.1XX] - [Checksum][osx-targz-checksum-2.2.1XX] | [![][osx-badge-2.1.6XX]][osx-version-2.1.6XX]<br>[Installer][osx-installer-2.1.6XX] - [Checksum][osx-installer-checksum-2.1.6XX]<br>[tar.gz][osx-targz-2.1.6XX] - [Checksum][osx-targz-checksum-2.1.6XX] | [![][osx-badge-2.1.5XX]][osx-version-2.1.5XX]<br>[Installer][osx-installer-2.1.5XX] - [Checksum][osx-installer-checksum-2.1.5XX]<br>[tar.gz][osx-targz-2.1.5XX] - [Checksum][osx-targz-checksum-2.1.5XX] |"

[<Fact>]
let ``it can generate linuxArmRow``() =
    linuxArmRow branches
    |> should equal
           "| **Linux arm** | [![][linux-arm-badge-master]][linux-arm-version-master]<br>[tar.gz][linux-arm-targz-master] - [Checksum][linux-arm-targz-checksum-master] | [![][linux-arm-badge-3.1.1XX]][linux-arm-version-3.1.1XX]<br>[tar.gz][linux-arm-targz-3.1.1XX] - [Checksum][linux-arm-targz-checksum-3.1.1XX] | [![][linux-arm-badge-3.0.1XX]][linux-arm-version-3.0.1XX]<br>[tar.gz][linux-arm-targz-3.0.1XX] - [Checksum][linux-arm-targz-checksum-3.0.1XX] | [![][linux-arm-badge-2.2.2XX]][linux-arm-version-2.2.2XX]<br>[tar.gz][linux-arm-targz-2.2.2XX] - [Checksum][linux-arm-targz-checksum-2.2.2XX] | [![][linux-arm-badge-2.2.1XX]][linux-arm-version-2.2.1XX]<br>[tar.gz][linux-arm-targz-2.2.1XX] - [Checksum][linux-arm-targz-checksum-2.2.1XX] | [![][linux-arm-badge-2.1.6XX]][linux-arm-version-2.1.6XX]<br>[tar.gz][linux-arm-targz-2.1.6XX] - [Checksum][linux-arm-targz-checksum-2.1.6XX] | [![][linux-arm-badge-2.1.5XX]][linux-arm-version-2.1.5XX]<br>[tar.gz][linux-arm-targz-2.1.5XX] - [Checksum][linux-arm-targz-checksum-2.1.5XX] |"

[<Fact>]
let ``it can generate windowsArm``() =
    windowsArmRow branches
    |> should equal
          "| **Windows arm** | [![][win-arm-badge-master]][win-arm-version-master]<br>[zip][win-arm-zip-master] - [Checksum][win-arm-zip-checksum-master] | [![][win-arm-badge-3.1.1XX]][win-arm-version-3.1.1XX]<br>[zip][win-arm-zip-3.1.1XX] - [Checksum][win-arm-zip-checksum-3.1.1XX] | [![][win-arm-badge-3.0.1XX]][win-arm-version-3.0.1XX]<br>[zip][win-arm-zip-3.0.1XX] - [Checksum][win-arm-zip-checksum-3.0.1XX] | [![][win-arm-badge-2.2.2XX]][win-arm-version-2.2.2XX]<br>[zip][win-arm-zip-2.2.2XX] - [Checksum][win-arm-zip-checksum-2.2.2XX] | [![][win-arm-badge-2.2.1XX]][win-arm-version-2.2.1XX]<br>[zip][win-arm-zip-2.2.1XX] - [Checksum][win-arm-zip-checksum-2.2.1XX] | **N/A** | **N/A** |"

[<Fact>]
let ``it can generate windowsArm with preview branch``() =
    let inputBranches =
        [ { GitBranchName = "master"
            DisplayName = "Master<br>(5.0.x&nbsp;Runtime)"
            AkaMsChannel = Some("net5/dev") }
          { GitBranchName = "release/5.0.1xx-preview4"
            DisplayName = "5.0.100 Preview 4<br>(5.0 Runtime)"
            AkaMsChannel = Some("net5/preview4") }]
    windowsArmRow inputBranches
    |> should equal
           "| **Windows arm** | [![][win-arm-badge-master]][win-arm-version-master]<br>[zip][win-arm-zip-master] - [Checksum][win-arm-zip-checksum-master] | [![][win-arm-badge-5.0.1XX-preview4]][win-arm-version-5.0.1XX-preview4]<br>[zip][win-arm-zip-5.0.1XX-preview4] - [Checksum][win-arm-zip-checksum-5.0.1XX-preview4] |"

[<Fact>]
let ``it can generate windowsArm64``() =
    windowsArm64Row branches
    |> should equal
           "| **Windows arm64** | [![][win-arm64-badge-master]][win-arm64-version-master]<br>[zip][win-arm64-zip-master] | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** |"


[<Fact>]
let ``it can generate titleRow``() =
    titleRow branches
    |> should equal
           "| Platform | Master<br>(5.0.x&nbsp;Runtime) | Release/3.1.1XX<br>(3.1.x Runtime) | Release/3.0.1xx<br>(3.0.x Runtime) | Release/2.2.2XX<br>(2.2.x Runtime) | Release/2.2.1XX<br>(2.2.x Runtime) | Release/2.1.6XX<br>(2.1.x Runtime) | Release/2.1.5XX<br>(2.1.x Runtime) |"

[<Fact>]
let ``it can generate separator``() =
    separator branches
    |> should equal
           "| :--------- | :----------: | :----------: | :----------: | :----------: | :----------: | :----------: | :----------: |"
// pinning tests https://wiki.c2.com/?PinningTests

[<Fact>]
let ``pinning tests for readme in master May 13th 2020``() =
    let inputBranches =
        [ { GitBranchName = "master"
            DisplayName = "Master<br>(5.0.x&nbsp;Runtime)"
            AkaMsChannel = Some("net5/dev") }
          { GitBranchName = "release/5.0.1xx-preview4"
            DisplayName = "5.0.100 Preview 4<br>(5.0 Runtime)"
            AkaMsChannel = Some("net5/preview4") }
          { GitBranchName = "release/5.0.1xx-preview3"
            DisplayName = "5.0.100 Preview 3<br>(5.0 Runtime)"
            AkaMsChannel = Some("net5/preview3") }
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

    wholeTable inputBranches
    |> should equal
           """output"""