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
          DisplayName = "Master<br>(5.0.x&nbsp;Runtime)" }
    branchNameShorten branch |> should equal "master"

[<Fact>]
let ``it can Shorten releae branch Name``() =
    let branch =
        { GitBranchName = "release/3.1.1xx"
          DisplayName = "Release/3.1.1XX<br>(3.1.x Runtime)" }
    branchNameShorten branch |> should equal "3.1.1XX"

[<Fact>]
let ``it can get major and minor version of a branch``() =
    let branch =
        { GitBranchName = "release/3.1.1xx"
          DisplayName = "Release/3.1.1XX<br>(3.1.x Runtime)" }
    getMajorMinor branch
    |> should equal
           (MajorMinor
               ({ Major = 3
                  Minor = 1 }))

[<Fact>]
let ``it can get master version of a master branch``() =
    let branch =
        { GitBranchName = "master"
          DisplayName = "Master<br>(5.0.x&nbsp;Runtime)" }
    getMajorMinor branch |> should equal Master

[<Fact>]
let ``it can get bad branch version``() =
    let branch =
        { GitBranchName = "badbranch"
          DisplayName = "" }
    getMajorMinor branch |> should equal NoVersion

[<Fact>]
let ``it can format winx64Reference``() =
    let branch =
        { GitBranchName = "release/3.1.1xx"
          DisplayName = "Release/3.1.1XX<br>(3.1.x Runtime)" }
    (winX64ReferenceTemplate branch).Value
    |> should equal
           """[win-x64-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/win_x64_Release_version_badge.svg
[win-x64-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[win-x64-installer-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.zip.sha"""

let branches =
    [ { GitBranchName = "master"
        DisplayName = "Master<br>(5.0.x&nbsp;Runtime)" }
      { GitBranchName = "release/3.1.1xx"
        DisplayName = "Release/3.1.1XX<br>(3.1.x Runtime)" }
      { GitBranchName = "release/3.0.1xx"
        DisplayName = "Release/3.0.1xx<br>(3.0.x Runtime)" }
      { GitBranchName = "release/2.2.2xx"
        DisplayName = "Release/2.2.2XX<br>(2.2.x Runtime)" }
      { GitBranchName = "release/2.2.1xx"
        DisplayName = "Release/2.2.1XX<br>(2.2.x Runtime)" }
      { GitBranchName = "release/2.1.6xx"
        DisplayName = "Release/2.1.6XX<br>(2.1.x Runtime)" }
      { GitBranchName = "release/2.1.5xx"
        DisplayName = "Release/2.1.5XX<br>(2.1.x Runtime)" } ]

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
let ``it can generate windowsArm64``() =
    windowsArm64Row branches
    |> should equal
           "| **Windows arm64** | [![][win-arm-64-badge-master]][win-arm-64-version-master]<br>[zip][win-arm-64-zip-master] | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** |"


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
let ``pinning tests for readme in 3.1.2xx``() =
    let branches =
        [ { GitBranchName = "master"
            DisplayName = "Master<br>(5.0.x&nbsp;Runtime)" }
          { GitBranchName = "release/3.1.1xx"
            DisplayName = "Release/3.1.1XX<br>(3.1.x Runtime)" }
          { GitBranchName = "release/3.0.1xx"
            DisplayName = "Release/3.0.1xx<br>(3.0.x Runtime)" }
          { GitBranchName = "release/2.2.3xx"
            DisplayName = "Release/2.2.3XX<br>(2.2.x Runtime)" }
          { GitBranchName = "release/2.2.2xx"
            DisplayName = "Release/2.2.2XX<br>(2.2.x Runtime)" }
          { GitBranchName = "release/2.2.1xx"
            DisplayName = "Release/2.2.1XX<br>(2.2.x Runtime)" }
          { GitBranchName = "release/2.1.7xx"
            DisplayName = "Release/2.1.7XX<br>(2.1.7 Runtime)" }
          { GitBranchName = "release/2.1.6xx"
            DisplayName = "Release/2.1.6XX<br>(2.1.6 Runtime)" }
          { GitBranchName = "release/2.1.5xx"
            DisplayName = "Release/2.1.5XX<br>(2.1.5 Runtime)" } ]

    wholeTable branches
    |> should equal
           """| Platform | Master<br>(5.0.x&nbsp;Runtime) | Release/3.1.1XX<br>(3.1.x Runtime) | Release/3.0.1xx<br>(3.0.x Runtime) | Release/2.2.3XX<br>(2.2.x Runtime) | Release/2.2.2XX<br>(2.2.x Runtime) | Release/2.2.1XX<br>(2.2.x Runtime) | Release/2.1.7XX<br>(2.1.7 Runtime) | Release/2.1.6XX<br>(2.1.6 Runtime) | Release/2.1.5XX<br>(2.1.5 Runtime) |
| :--------- | :----------: | :----------: | :----------: | :----------: | :----------: | :----------: | :----------: | :----------: | :----------: |
| **Windows x64** | [![][win-x64-badge-master]][win-x64-version-master]<br>[Installer][win-x64-installer-master] - [Checksum][win-x64-installer-checksum-master]<br>[zip][win-x64-zip-master] - [Checksum][win-x64-zip-checksum-master] | [![][win-x64-badge-3.1.1XX]][win-x64-version-3.1.1XX]<br>[Installer][win-x64-installer-3.1.1XX] - [Checksum][win-x64-installer-checksum-3.1.1XX]<br>[zip][win-x64-zip-3.1.1XX] - [Checksum][win-x64-zip-checksum-3.1.1XX] | [![][win-x64-badge-3.0.1XX]][win-x64-version-3.0.1XX]<br>[Installer][win-x64-installer-3.0.1XX] - [Checksum][win-x64-installer-checksum-3.0.1XX]<br>[zip][win-x64-zip-3.0.1XX] - [Checksum][win-x64-zip-checksum-3.0.1XX] | [![][win-x64-badge-2.2.3XX]][win-x64-version-2.2.3XX]<br>[Installer][win-x64-installer-2.2.3XX] - [Checksum][win-x64-installer-checksum-2.2.3XX]<br>[zip][win-x64-zip-2.2.3XX] - [Checksum][win-x64-zip-checksum-2.2.3XX] | [![][win-x64-badge-2.2.2XX]][win-x64-version-2.2.2XX]<br>[Installer][win-x64-installer-2.2.2XX] - [Checksum][win-x64-installer-checksum-2.2.2XX]<br>[zip][win-x64-zip-2.2.2XX] - [Checksum][win-x64-zip-checksum-2.2.2XX] | [![][win-x64-badge-2.2.1XX]][win-x64-version-2.2.1XX]<br>[Installer][win-x64-installer-2.2.1XX] - [Checksum][win-x64-installer-checksum-2.2.1XX]<br>[zip][win-x64-zip-2.2.1XX] - [Checksum][win-x64-zip-checksum-2.2.1XX] | [![][win-x64-badge-2.1.7XX]][win-x64-version-2.1.7XX]<br>[Installer][win-x64-installer-2.1.7XX] - [Checksum][win-x64-installer-checksum-2.1.7XX]<br>[zip][win-x64-zip-2.1.7XX] - [Checksum][win-x64-zip-checksum-2.1.7XX] | [![][win-x64-badge-2.1.6XX]][win-x64-version-2.1.6XX]<br>[Installer][win-x64-installer-2.1.6XX] - [Checksum][win-x64-installer-checksum-2.1.6XX]<br>[zip][win-x64-zip-2.1.6XX] - [Checksum][win-x64-zip-checksum-2.1.6XX] | [![][win-x64-badge-2.1.5XX]][win-x64-version-2.1.5XX]<br>[Installer][win-x64-installer-2.1.5XX] - [Checksum][win-x64-installer-checksum-2.1.5XX]<br>[zip][win-x64-zip-2.1.5XX] - [Checksum][win-x64-zip-checksum-2.1.5XX] |
| **Windows x86** | [![][win-x86-badge-master]][win-x86-version-master]<br>[Installer][win-x86-installer-master] - [Checksum][win-x86-installer-checksum-master]<br>[zip][win-x86-zip-master] - [Checksum][win-x86-zip-checksum-master] | [![][win-x86-badge-3.1.1XX]][win-x86-version-3.1.1XX]<br>[Installer][win-x86-installer-3.1.1XX] - [Checksum][win-x86-installer-checksum-3.1.1XX]<br>[zip][win-x86-zip-3.1.1XX] - [Checksum][win-x86-zip-checksum-3.1.1XX] | [![][win-x86-badge-3.0.1XX]][win-x86-version-3.0.1XX]<br>[Installer][win-x86-installer-3.0.1XX] - [Checksum][win-x86-installer-checksum-3.0.1XX]<br>[zip][win-x86-zip-3.0.1XX] - [Checksum][win-x86-zip-checksum-3.0.1XX] | [![][win-x86-badge-2.2.3XX]][win-x86-version-2.2.3XX]<br>[Installer][win-x86-installer-2.2.3XX] - [Checksum][win-x86-installer-checksum-2.2.3XX]<br>[zip][win-x86-zip-2.2.3XX] - [Checksum][win-x86-zip-checksum-2.2.3XX] | [![][win-x86-badge-2.2.2XX]][win-x86-version-2.2.2XX]<br>[Installer][win-x86-installer-2.2.2XX] - [Checksum][win-x86-installer-checksum-2.2.2XX]<br>[zip][win-x86-zip-2.2.2XX] - [Checksum][win-x86-zip-checksum-2.2.2XX] | [![][win-x86-badge-2.2.1XX]][win-x86-version-2.2.1XX]<br>[Installer][win-x86-installer-2.2.1XX] - [Checksum][win-x86-installer-checksum-2.2.1XX]<br>[zip][win-x86-zip-2.2.1XX] - [Checksum][win-x86-zip-checksum-2.2.1XX] | [![][win-x86-badge-2.1.7XX]][win-x86-version-2.1.7XX]<br>[Installer][win-x86-installer-2.1.7XX] - [Checksum][win-x86-installer-checksum-2.1.7XX]<br>[zip][win-x86-zip-2.1.7XX] - [Checksum][win-x86-zip-checksum-2.1.7XX] | [![][win-x86-badge-2.1.6XX]][win-x86-version-2.1.6XX]<br>[Installer][win-x86-installer-2.1.6XX] - [Checksum][win-x86-installer-checksum-2.1.6XX]<br>[zip][win-x86-zip-2.1.6XX] - [Checksum][win-x86-zip-checksum-2.1.6XX] | [![][win-x86-badge-2.1.5XX]][win-x86-version-2.1.5XX]<br>[Installer][win-x86-installer-2.1.5XX] - [Checksum][win-x86-installer-checksum-2.1.5XX]<br>[zip][win-x86-zip-2.1.5XX] - [Checksum][win-x86-zip-checksum-2.1.5XX] |
| **macOS** | [![][osx-badge-master]][osx-version-master]<br>[Installer][osx-installer-master] - [Checksum][osx-installer-checksum-master]<br>[tar.gz][osx-targz-master] - [Checksum][osx-targz-checksum-master] | [![][osx-badge-3.1.1XX]][osx-version-3.1.1XX]<br>[Installer][osx-installer-3.1.1XX] - [Checksum][osx-installer-checksum-3.1.1XX]<br>[tar.gz][osx-targz-3.1.1XX] - [Checksum][osx-targz-checksum-3.1.1XX] | [![][osx-badge-3.0.1XX]][osx-version-3.0.1XX]<br>[Installer][osx-installer-3.0.1XX] - [Checksum][osx-installer-checksum-3.0.1XX]<br>[tar.gz][osx-targz-3.0.1XX] - [Checksum][osx-targz-checksum-3.0.1XX] | [![][osx-badge-2.2.3XX]][osx-version-2.2.3XX]<br>[Installer][osx-installer-2.2.3XX] - [Checksum][osx-installer-checksum-2.2.3XX]<br>[tar.gz][osx-targz-2.2.3XX] - [Checksum][osx-targz-checksum-2.2.3XX] | [![][osx-badge-2.2.2XX]][osx-version-2.2.2XX]<br>[Installer][osx-installer-2.2.2XX] - [Checksum][osx-installer-checksum-2.2.2XX]<br>[tar.gz][osx-targz-2.2.2XX] - [Checksum][osx-targz-checksum-2.2.2XX] | [![][osx-badge-2.2.1XX]][osx-version-2.2.1XX]<br>[Installer][osx-installer-2.2.1XX] - [Checksum][osx-installer-checksum-2.2.1XX]<br>[tar.gz][osx-targz-2.2.1XX] - [Checksum][osx-targz-checksum-2.2.1XX] | [![][osx-badge-2.1.7XX]][osx-version-2.1.7XX]<br>[Installer][osx-installer-2.1.7XX] - [Checksum][osx-installer-checksum-2.1.7XX]<br>[tar.gz][osx-targz-2.1.7XX] - [Checksum][osx-targz-checksum-2.1.7XX] | [![][osx-badge-2.1.6XX]][osx-version-2.1.6XX]<br>[Installer][osx-installer-2.1.6XX] - [Checksum][osx-installer-checksum-2.1.6XX]<br>[tar.gz][osx-targz-2.1.6XX] - [Checksum][osx-targz-checksum-2.1.6XX] | [![][osx-badge-2.1.5XX]][osx-version-2.1.5XX]<br>[Installer][osx-installer-2.1.5XX] - [Checksum][osx-installer-checksum-2.1.5XX]<br>[tar.gz][osx-targz-2.1.5XX] - [Checksum][osx-targz-checksum-2.1.5XX] |
| **Linux x64** | [![][linux-badge-master]][linux-version-master]<br>[DEB Installer][linux-DEB-installer-master] - [Checksum][linux-DEB-installer-checksum-master]<br>[RPM Installer][linux-RPM-installer-master] - [Checksum][linux-RPM-installer-checksum-master]<br>_see installer note below_<sup>1</sup><br>[tar.gz][linux-targz-master] - [Checksum][linux-targz-checksum-master] | [![][linux-badge-3.1.1XX]][linux-version-3.1.1XX]<br>[DEB Installer][linux-DEB-installer-3.1.1XX] - [Checksum][linux-DEB-installer-checksum-3.1.1XX]<br>[RPM Installer][linux-RPM-installer-3.1.1XX] - [Checksum][linux-RPM-installer-checksum-3.1.1XX]<br>_see installer note below_<sup>1</sup><br>[tar.gz][linux-targz-3.1.1XX] - [Checksum][linux-targz-checksum-3.1.1XX] | [![][linux-badge-3.0.1XX]][linux-version-3.0.1XX]<br>[DEB Installer][linux-DEB-installer-3.0.1XX] - [Checksum][linux-DEB-installer-checksum-3.0.1XX]<br>[RPM Installer][linux-RPM-installer-3.0.1XX] - [Checksum][linux-RPM-installer-checksum-3.0.1XX]<br>_see installer note below_<sup>1</sup><br>[tar.gz][linux-targz-3.0.1XX] - [Checksum][linux-targz-checksum-3.0.1XX] | [![][linux-badge-2.2.3XX]][linux-version-2.2.3XX]<br>[DEB Installer][linux-DEB-installer-2.2.3XX] - [Checksum][linux-DEB-installer-checksum-2.2.3XX]<br>[RPM Installer][linux-RPM-installer-2.2.3XX] - [Checksum][linux-RPM-installer-checksum-2.2.3XX]<br>_see installer note below_<sup>1</sup><br>[tar.gz][linux-targz-2.2.3XX] - [Checksum][linux-targz-checksum-2.2.3XX] | [![][linux-badge-2.2.2XX]][linux-version-2.2.2XX]<br>[DEB Installer][linux-DEB-installer-2.2.2XX] - [Checksum][linux-DEB-installer-checksum-2.2.2XX]<br>[RPM Installer][linux-RPM-installer-2.2.2XX] - [Checksum][linux-RPM-installer-checksum-2.2.2XX]<br>_see installer note below_<sup>1</sup><br>[tar.gz][linux-targz-2.2.2XX] - [Checksum][linux-targz-checksum-2.2.2XX] | [![][linux-badge-2.2.1XX]][linux-version-2.2.1XX]<br>[DEB Installer][linux-DEB-installer-2.2.1XX] - [Checksum][linux-DEB-installer-checksum-2.2.1XX]<br>[RPM Installer][linux-RPM-installer-2.2.1XX] - [Checksum][linux-RPM-installer-checksum-2.2.1XX]<br>_see installer note below_<sup>1</sup><br>[tar.gz][linux-targz-2.2.1XX] - [Checksum][linux-targz-checksum-2.2.1XX] | [![][linux-badge-2.1.7XX]][linux-version-2.1.7XX]<br>[DEB Installer][linux-DEB-installer-2.1.7XX] - [Checksum][linux-DEB-installer-checksum-2.1.7XX]<br>[RPM Installer][linux-RPM-installer-2.1.7XX] - [Checksum][linux-RPM-installer-checksum-2.1.7XX]<br>_see installer note below_<sup>1</sup><br>[tar.gz][linux-targz-2.1.7XX] - [Checksum][linux-targz-checksum-2.1.7XX] | [![][linux-badge-2.1.6XX]][linux-version-2.1.6XX]<br>[DEB Installer][linux-DEB-installer-2.1.6XX] - [Checksum][linux-DEB-installer-checksum-2.1.6XX]<br>[RPM Installer][linux-RPM-installer-2.1.6XX] - [Checksum][linux-RPM-installer-checksum-2.1.6XX]<br>_see installer note below_<sup>1</sup><br>[tar.gz][linux-targz-2.1.6XX] - [Checksum][linux-targz-checksum-2.1.6XX] | [![][linux-badge-2.1.5XX]][linux-version-2.1.5XX]<br>[DEB Installer][linux-DEB-installer-2.1.5XX] - [Checksum][linux-DEB-installer-checksum-2.1.5XX]<br>[RPM Installer][linux-RPM-installer-2.1.5XX] - [Checksum][linux-RPM-installer-checksum-2.1.5XX]<br>_see installer note below_<sup>1</sup><br>[tar.gz][linux-targz-2.1.5XX] - [Checksum][linux-targz-checksum-2.1.5XX] |
| **Linux arm** | [![][linux-arm-badge-master]][linux-arm-version-master]<br>[tar.gz][linux-arm-targz-master] - [Checksum][linux-arm-targz-checksum-master] | [![][linux-arm-badge-3.1.1XX]][linux-arm-version-3.1.1XX]<br>[tar.gz][linux-arm-targz-3.1.1XX] - [Checksum][linux-arm-targz-checksum-3.1.1XX] | [![][linux-arm-badge-3.0.1XX]][linux-arm-version-3.0.1XX]<br>[tar.gz][linux-arm-targz-3.0.1XX] - [Checksum][linux-arm-targz-checksum-3.0.1XX] | [![][linux-arm-badge-2.2.3XX]][linux-arm-version-2.2.3XX]<br>[tar.gz][linux-arm-targz-2.2.3XX] - [Checksum][linux-arm-targz-checksum-2.2.3XX] | [![][linux-arm-badge-2.2.2XX]][linux-arm-version-2.2.2XX]<br>[tar.gz][linux-arm-targz-2.2.2XX] - [Checksum][linux-arm-targz-checksum-2.2.2XX] | [![][linux-arm-badge-2.2.1XX]][linux-arm-version-2.2.1XX]<br>[tar.gz][linux-arm-targz-2.2.1XX] - [Checksum][linux-arm-targz-checksum-2.2.1XX] | [![][linux-arm-badge-2.1.7XX]][linux-arm-version-2.1.7XX]<br>[tar.gz][linux-arm-targz-2.1.7XX] - [Checksum][linux-arm-targz-checksum-2.1.7XX] | [![][linux-arm-badge-2.1.6XX]][linux-arm-version-2.1.6XX]<br>[tar.gz][linux-arm-targz-2.1.6XX] - [Checksum][linux-arm-targz-checksum-2.1.6XX] | [![][linux-arm-badge-2.1.5XX]][linux-arm-version-2.1.5XX]<br>[tar.gz][linux-arm-targz-2.1.5XX] - [Checksum][linux-arm-targz-checksum-2.1.5XX] |
| **Linux arm64** | [![][linux-arm64-badge-master]][linux-arm64-version-master]<br>[tar.gz][linux-arm64-targz-master] - [Checksum][linux-arm64-targz-checksum-master] | [![][linux-arm64-badge-3.1.1XX]][linux-arm64-version-3.1.1XX]<br>[tar.gz][linux-arm64-targz-3.1.1XX] - [Checksum][linux-arm64-targz-checksum-3.1.1XX] | [![][linux-arm64-badge-3.0.1XX]][linux-arm64-version-3.0.1XX]<br>[tar.gz][linux-arm64-targz-3.0.1XX] - [Checksum][linux-arm64-targz-checksum-3.0.1XX] | [![][linux-arm64-badge-2.2.3XX]][linux-arm64-version-2.2.3XX]<br>[tar.gz][linux-arm64-targz-2.2.3XX] - [Checksum][linux-arm64-targz-checksum-2.2.3XX] | [![][linux-arm64-badge-2.2.2XX]][linux-arm64-version-2.2.2XX]<br>[tar.gz][linux-arm64-targz-2.2.2XX] - [Checksum][linux-arm64-targz-checksum-2.2.2XX] | [![][linux-arm64-badge-2.2.1XX]][linux-arm64-version-2.2.1XX]<br>[tar.gz][linux-arm64-targz-2.2.1XX] - [Checksum][linux-arm64-targz-checksum-2.2.1XX] | [![][linux-arm64-badge-2.1.7XX]][linux-arm64-version-2.1.7XX]<br>[tar.gz][linux-arm64-targz-2.1.7XX] - [Checksum][linux-arm64-targz-checksum-2.1.7XX] | [![][linux-arm64-badge-2.1.6XX]][linux-arm64-version-2.1.6XX]<br>[tar.gz][linux-arm64-targz-2.1.6XX] - [Checksum][linux-arm64-targz-checksum-2.1.6XX] | [![][linux-arm64-badge-2.1.5XX]][linux-arm64-version-2.1.5XX]<br>[tar.gz][linux-arm64-targz-2.1.5XX] - [Checksum][linux-arm64-targz-checksum-2.1.5XX] |
| **RHEL 6** | [![][rhel-6-badge-master]][rhel-6-version-master]<br>[tar.gz][rhel-6-targz-master] - [Checksum][rhel-6-targz-checksum-master] | [![][rhel-6-badge-3.1.1XX]][rhel-6-version-3.1.1XX]<br>[tar.gz][rhel-6-targz-3.1.1XX] - [Checksum][rhel-6-targz-checksum-3.1.1XX] | [![][rhel-6-badge-3.0.1XX]][rhel-6-version-3.0.1XX]<br>[tar.gz][rhel-6-targz-3.0.1XX] - [Checksum][rhel-6-targz-checksum-3.0.1XX] | [![][rhel-6-badge-2.2.3XX]][rhel-6-version-2.2.3XX]<br>[tar.gz][rhel-6-targz-2.2.3XX] - [Checksum][rhel-6-targz-checksum-2.2.3XX] | [![][rhel-6-badge-2.2.2XX]][rhel-6-version-2.2.2XX]<br>[tar.gz][rhel-6-targz-2.2.2XX] - [Checksum][rhel-6-targz-checksum-2.2.2XX] | [![][rhel-6-badge-2.2.1XX]][rhel-6-version-2.2.1XX]<br>[tar.gz][rhel-6-targz-2.2.1XX] - [Checksum][rhel-6-targz-checksum-2.2.1XX] | [![][rhel-6-badge-2.1.7XX]][rhel-6-version-2.1.7XX]<br>[tar.gz][rhel-6-targz-2.1.7XX] - [Checksum][rhel-6-targz-checksum-2.1.7XX] | [![][rhel-6-badge-2.1.6XX]][rhel-6-version-2.1.6XX]<br>[tar.gz][rhel-6-targz-2.1.6XX] - [Checksum][rhel-6-targz-checksum-2.1.6XX] | [![][rhel-6-badge-2.1.5XX]][rhel-6-version-2.1.5XX]<br>[tar.gz][rhel-6-targz-2.1.5XX] - [Checksum][rhel-6-targz-checksum-2.1.5XX] |
| **Linux-musl** | [![][linux-musl-badge-master]][linux-musl-version-master]<br>[tar.gz][linux-musl-targz-master] - [Checksum][linux-musl-targz-checksum-master] | [![][linux-musl-badge-3.1.1XX]][linux-musl-version-3.1.1XX]<br>[tar.gz][linux-musl-targz-3.1.1XX] - [Checksum][linux-musl-targz-checksum-3.1.1XX] | [![][linux-musl-badge-3.0.1XX]][linux-musl-version-3.0.1XX]<br>[tar.gz][linux-musl-targz-3.0.1XX] - [Checksum][linux-musl-targz-checksum-3.0.1XX] | [![][linux-musl-badge-2.2.3XX]][linux-musl-version-2.2.3XX]<br>[tar.gz][linux-musl-targz-2.2.3XX] - [Checksum][linux-musl-targz-checksum-2.2.3XX] | [![][linux-musl-badge-2.2.2XX]][linux-musl-version-2.2.2XX]<br>[tar.gz][linux-musl-targz-2.2.2XX] - [Checksum][linux-musl-targz-checksum-2.2.2XX] | [![][linux-musl-badge-2.2.1XX]][linux-musl-version-2.2.1XX]<br>[tar.gz][linux-musl-targz-2.2.1XX] - [Checksum][linux-musl-targz-checksum-2.2.1XX] | [![][linux-musl-badge-2.1.7XX]][linux-musl-version-2.1.7XX]<br>[tar.gz][linux-musl-targz-2.1.7XX] - [Checksum][linux-musl-targz-checksum-2.1.7XX] | [![][linux-musl-badge-2.1.6XX]][linux-musl-version-2.1.6XX]<br>[tar.gz][linux-musl-targz-2.1.6XX] - [Checksum][linux-musl-targz-checksum-2.1.6XX] | [![][linux-musl-badge-2.1.5XX]][linux-musl-version-2.1.5XX]<br>[tar.gz][linux-musl-targz-2.1.5XX] - [Checksum][linux-musl-targz-checksum-2.1.5XX] |
| **Windows arm** | [![][win-arm-badge-master]][win-arm-version-master]<br>[zip][win-arm-zip-master] - [Checksum][win-arm-zip-checksum-master] | [![][win-arm-badge-3.1.1XX]][win-arm-version-3.1.1XX]<br>[zip][win-arm-zip-3.1.1XX] - [Checksum][win-arm-zip-checksum-3.1.1XX] | [![][win-arm-badge-3.0.1XX]][win-arm-version-3.0.1XX]<br>[zip][win-arm-zip-3.0.1XX] - [Checksum][win-arm-zip-checksum-3.0.1XX] | [![][win-arm-badge-2.2.3XX]][win-arm-version-2.2.3XX]<br>[zip][win-arm-zip-2.2.3XX] - [Checksum][win-arm-zip-checksum-2.2.3XX] | [![][win-arm-badge-2.2.2XX]][win-arm-version-2.2.2XX]<br>[zip][win-arm-zip-2.2.2XX] - [Checksum][win-arm-zip-checksum-2.2.2XX] | [![][win-arm-badge-2.2.1XX]][win-arm-version-2.2.1XX]<br>[zip][win-arm-zip-2.2.1XX] - [Checksum][win-arm-zip-checksum-2.2.1XX] | **N/A** | **N/A** | **N/A** |
| **Windows arm64** | [![][win-arm-64-badge-master]][win-arm-64-version-master]<br>[zip][win-arm-64-zip-master] | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** |
| **FreeBSD x64** | [![][freebsd-x64-badge-master]][freebsd-x64-version-master]<br>[tar.gz][freebsd-x64-zip-master] - [Checksum][freebsd-x64-zip-checksum-master]  | [![][freebsd-x64-badge-3.1.1XX]][freebsd-x64-version-3.1.1XX]<br>[tar.gz][freebsd-x64-zip-3.1.1XX] - [Checksum][freebsd-x64-zip-checksum-3.1.1XX]  | [![][freebsd-x64-badge-3.0.1XX]][freebsd-x64-version-3.0.1XX]<br>[tar.gz][freebsd-x64-zip-3.0.1XX] - [Checksum][freebsd-x64-zip-checksum-3.0.1XX]  | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** |
| **Constituent Repo Shas** | **N/A** | **N/A** | [Git SHAs][sdk-shas-2.2.1XX] | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** | **N/A** |

Reference notes:
> **1**: Our Debian packages are put together slightly differently than the other OS specific installers. Instead of combining everything, we have separate component packages that depend on each other. If you're installing the SDK from the .deb file (via dpkg or similar), then you'll need to install the corresponding dependencies first:
> * [Host, Host FX Resolver, and Shared Framework](https://github.com/dotnet/runtime#daily-builds)
> * [ASP.NET Core Shared Framework](https://github.com/aspnet/AspNetCore/blob/master/docs/DailyBuilds.md)

.NET Core SDK 2.x downloads can be found here: [.NET Core SDK 2.x Installers and Binaries](Downloads2.x.md)

[win-x64-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/win_x64_Release_version_badge.svg
[win-x64-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[win-x64-installer-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-x64.zip.sha

[win-x64-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/win_x64_Release_version_badge.svg
[win-x64-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[win-x64-installer-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x64.zip.sha

[win-x64-badge-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/win_x64_Release_version_badge.svg
[win-x64-version-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/latest.version
[win-x64-installer-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-win-x64.zip.sha

[win-x64-badge-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/win_x64_Release_version_badge.svg
[win-x64-version-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/latest.version
[win-x64-installer-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-win-x64.zip.sha

[win-x64-badge-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/win_x64_Release_version_badge.svg
[win-x64-version-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/latest.version
[win-x64-installer-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-win-x64.zip.sha

[win-x64-badge-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/win_x64_Release_version_badge.svg
[win-x64-version-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/latest.version
[win-x64-installer-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-win-x64.zip.sha

[win-x64-badge-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/win_x64_Release_version_badge.svg
[win-x64-version-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/latest.version
[win-x64-installer-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-win-x64.zip.sha

[win-x64-badge-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/win_x64_Release_version_badge.svg
[win-x64-version-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/latest.version
[win-x64-installer-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-win-x64.zip.sha

[win-x64-badge-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/win_x64_Release_version_badge.svg
[win-x64-version-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/latest.version
[win-x64-installer-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-win-x64.exe
[win-x64-installer-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-win-x64.exe.sha
[win-x64-zip-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-win-x64.zip
[win-x64-zip-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-win-x64.zip.sha

[win-x86-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/win_x86_Release_version_badge.svg
[win-x86-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[win-x86-installer-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-x86.exe
[win-x86-installer-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-x86.exe.sha
[win-x86-zip-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-x86.zip
[win-x86-zip-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-x86.zip.sha

[win-x86-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/win_x86_Release_version_badge.svg
[win-x86-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[win-x86-installer-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x86.exe
[win-x86-installer-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x86.exe.sha
[win-x86-zip-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x86.zip
[win-x86-zip-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-x86.zip.sha

[win-x86-badge-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/win_x86_Release_version_badge.svg
[win-x86-version-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/latest.version
[win-x86-installer-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-win-x86.exe
[win-x86-installer-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-win-x86.exe.sha
[win-x86-zip-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-win-x86.zip
[win-x86-zip-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-win-x86.zip.sha

[win-x86-badge-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/win_x86_Release_version_badge.svg
[win-x86-version-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/latest.version
[win-x86-installer-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-win-x86.exe
[win-x86-installer-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-win-x86.exe.sha
[win-x86-zip-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-win-x86.zip
[win-x86-zip-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-win-x86.zip.sha

[win-x86-badge-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/win_x86_Release_version_badge.svg
[win-x86-version-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/latest.version
[win-x86-installer-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-win-x86.exe
[win-x86-installer-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-win-x86.exe.sha
[win-x86-zip-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-win-x86.zip
[win-x86-zip-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-win-x86.zip.sha

[win-x86-badge-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/win_x86_Release_version_badge.svg
[win-x86-version-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/latest.version
[win-x86-installer-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-win-x86.exe
[win-x86-installer-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-win-x86.exe.sha
[win-x86-zip-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-win-x86.zip
[win-x86-zip-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-win-x86.zip.sha

[win-x86-badge-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/win_x86_Release_version_badge.svg
[win-x86-version-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/latest.version
[win-x86-installer-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-win-x86.exe
[win-x86-installer-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-win-x86.exe.sha
[win-x86-zip-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-win-x86.zip
[win-x86-zip-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-win-x86.zip.sha

[win-x86-badge-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/win_x86_Release_version_badge.svg
[win-x86-version-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/latest.version
[win-x86-installer-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-win-x86.exe
[win-x86-installer-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-win-x86.exe.sha
[win-x86-zip-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-win-x86.zip
[win-x86-zip-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-win-x86.zip.sha

[win-x86-badge-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/win_x86_Release_version_badge.svg
[win-x86-version-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/latest.version
[win-x86-installer-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-win-x86.exe
[win-x86-installer-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-win-x86.exe.sha
[win-x86-zip-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-win-x86.zip
[win-x86-zip-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-win-x86.zip.sha

[osx-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/osx_x64_Release_version_badge.svg
[osx-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[osx-installer-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-osx-x64.pkg
[osx-installer-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-osx-x64.pkg.sha
[osx-targz-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-osx-x64.tar.gz
[osx-targz-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-osx-x64.tar.gz.sha

[osx-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/osx_x64_Release_version_badge.svg
[osx-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[osx-installer-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-osx-x64.pkg
[osx-installer-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-osx-x64.pkg.sha
[osx-targz-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-osx-x64.tar.gz
[osx-targz-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-osx-x64.tar.gz.sha

[osx-badge-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/osx_x64_Release_version_badge.svg
[osx-version-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/latest.version
[osx-installer-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-osx-x64.pkg
[osx-installer-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-osx-x64.pkg.sha
[osx-targz-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-osx-x64.tar.gz
[osx-targz-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-osx-x64.tar.gz.sha

[osx-badge-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/osx_x64_Release_version_badge.svg
[osx-version-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/latest.version
[osx-installer-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-osx-x64.pkg
[osx-installer-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-osx-x64.pkg.sha
[osx-targz-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-osx-x64.tar.gz
[osx-targz-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-osx-x64.tar.gz.sha

[osx-badge-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/osx_x64_Release_version_badge.svg
[osx-version-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/latest.version
[osx-installer-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-osx-x64.pkg
[osx-installer-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-osx-x64.pkg.sha
[osx-targz-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-osx-x64.tar.gz
[osx-targz-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-osx-x64.tar.gz.sha

[osx-badge-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/osx_x64_Release_version_badge.svg
[osx-version-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/latest.version
[osx-installer-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-osx-x64.pkg
[osx-installer-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-osx-x64.pkg.sha
[osx-targz-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-osx-x64.tar.gz
[osx-targz-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-osx-x64.tar.gz.sha

[osx-badge-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/osx_x64_Release_version_badge.svg
[osx-version-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/latest.version
[osx-installer-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-osx-x64.pkg
[osx-installer-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-osx-x64.pkg.sha
[osx-targz-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-osx-x64.tar.gz
[osx-targz-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-osx-x64.tar.gz.sha

[osx-badge-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/osx_x64_Release_version_badge.svg
[osx-version-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/latest.version
[osx-installer-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-osx-x64.pkg
[osx-installer-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-osx-x64.pkg.sha
[osx-targz-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-osx-x64.tar.gz
[osx-targz-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-osx-x64.tar.gz.sha

[osx-badge-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/osx_x64_Release_version_badge.svg
[osx-version-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/latest.version
[osx-installer-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-osx-x64.pkg
[osx-installer-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-osx-x64.pkg.sha
[osx-targz-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-osx-x64.tar.gz
[osx-targz-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-osx-x64.tar.gz.sha

[linux-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/linux_x64_Release_version_badge.svg
[linux-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[linux-DEB-installer-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-x64.deb
[linux-DEB-installer-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-x64.deb.sha
[linux-RPM-installer-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-x64.rpm
[linux-RPM-installer-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-x64.rpm.sha
[linux-targz-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-linux-x64.tar.gz
[linux-targz-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-linux-x64.tar.gz.sha

[linux-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/linux_x64_Release_version_badge.svg
[linux-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[linux-DEB-installer-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-x64.deb
[linux-DEB-installer-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-x64.deb.sha
[linux-RPM-installer-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-x64.rpm
[linux-RPM-installer-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-x64.rpm.sha
[linux-targz-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-linux-x64.tar.gz
[linux-targz-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-linux-x64.tar.gz.sha

[linux-badge-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/linux_x64_Release_version_badge.svg
[linux-version-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/latest.version
[linux-DEB-installer-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-x64.deb
[linux-DEB-installer-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-x64.deb.sha
[linux-RPM-installer-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-x64.rpm
[linux-RPM-installer-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-x64.rpm.sha
[linux-targz-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-linux-x64.tar.gz
[linux-targz-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-linux-x64.tar.gz.sha

[linux-badge-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/linux_x64_Release_version_badge.svg
[linux-version-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/latest.version
[linux-DEB-installer-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-x64.deb
[linux-DEB-installer-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-x64.deb.sha
[linux-RPM-installer-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-x64.rpm
[linux-RPM-installer-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-x64.rpm.sha
[linux-targz-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-linux-x64.tar.gz
[linux-targz-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-linux-x64.tar.gz.sha

[linux-badge-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/linux_x64_Release_version_badge.svg
[linux-version-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/latest.version
[linux-DEB-installer-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-x64.deb
[linux-DEB-installer-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-x64.deb.sha
[linux-RPM-installer-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-x64.rpm
[linux-RPM-installer-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-x64.rpm.sha
[linux-targz-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-linux-x64.tar.gz
[linux-targz-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-linux-x64.tar.gz.sha

[linux-badge-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/linux_x64_Release_version_badge.svg
[linux-version-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/latest.version
[linux-DEB-installer-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-x64.deb
[linux-DEB-installer-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-x64.deb.sha
[linux-RPM-installer-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-x64.rpm
[linux-RPM-installer-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-x64.rpm.sha
[linux-targz-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-linux-x64.tar.gz
[linux-targz-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-linux-x64.tar.gz.sha

[linux-badge-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/linux_x64_Release_version_badge.svg
[linux-version-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/latest.version
[linux-DEB-installer-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-x64.deb
[linux-DEB-installer-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-x64.deb.sha
[linux-RPM-installer-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-x64.rpm
[linux-RPM-installer-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-x64.rpm.sha
[linux-targz-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-linux-x64.tar.gz
[linux-targz-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-linux-x64.tar.gz.sha

[linux-badge-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/linux_x64_Release_version_badge.svg
[linux-version-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/latest.version
[linux-DEB-installer-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-x64.deb
[linux-DEB-installer-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-x64.deb.sha
[linux-RPM-installer-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-x64.rpm
[linux-RPM-installer-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-x64.rpm.sha
[linux-targz-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-linux-x64.tar.gz
[linux-targz-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-linux-x64.tar.gz.sha

[linux-badge-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/linux_x64_Release_version_badge.svg
[linux-version-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/latest.version
[linux-DEB-installer-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-x64.deb
[linux-DEB-installer-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-x64.deb.sha
[linux-RPM-installer-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-x64.rpm
[linux-RPM-installer-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-x64.rpm.sha
[linux-targz-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-linux-x64.tar.gz
[linux-targz-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-linux-x64.tar.gz.sha

[linux-arm-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/linux_arm_Release_version_badge.svg
[linux-arm-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[linux-arm-targz-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-linux-arm.tar.gz
[linux-arm-targz-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-linux-arm.tar.gz.sha

[linux-arm-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/linux_arm_Release_version_badge.svg
[linux-arm-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[linux-arm-targz-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-linux-arm.tar.gz
[linux-arm-targz-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-linux-arm.tar.gz.sha

[linux-arm-badge-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/linux_arm_Release_version_badge.svg
[linux-arm-version-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/latest.version
[linux-arm-targz-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-linux-arm.tar.gz
[linux-arm-targz-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-linux-arm.tar.gz.sha

[linux-arm-badge-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/linux_arm_Release_version_badge.svg
[linux-arm-version-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/latest.version
[linux-arm-targz-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-linux-arm.tar.gz
[linux-arm-targz-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-linux-arm.tar.gz.sha

[linux-arm-badge-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/linux_arm_Release_version_badge.svg
[linux-arm-version-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/latest.version
[linux-arm-targz-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-linux-arm.tar.gz
[linux-arm-targz-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-linux-arm.tar.gz.sha

[linux-arm-badge-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/linux_arm_Release_version_badge.svg
[linux-arm-version-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/latest.version
[linux-arm-targz-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-linux-arm.tar.gz
[linux-arm-targz-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-linux-arm.tar.gz.sha

[linux-arm-badge-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/linux_arm_Release_version_badge.svg
[linux-arm-version-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/latest.version
[linux-arm-targz-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-linux-arm.tar.gz
[linux-arm-targz-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-linux-arm.tar.gz.sha

[linux-arm-badge-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/linux_arm_Release_version_badge.svg
[linux-arm-version-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/latest.version
[linux-arm-targz-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-linux-arm.tar.gz
[linux-arm-targz-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-linux-arm.tar.gz.sha

[linux-arm-badge-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/linux_arm_Release_version_badge.svg
[linux-arm-version-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/latest.version
[linux-arm-targz-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-linux-arm.tar.gz
[linux-arm-targz-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-linux-arm.tar.gz.sha

[linux-arm64-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/linux_arm64_Release_version_badge.svg
[linux-arm64-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[linux-arm64-targz-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-linux-arm64.tar.gz
[linux-arm64-targz-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-linux-arm64.tar.gz.sha

[linux-arm64-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/linux_arm64_Release_version_badge.svg
[linux-arm64-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[linux-arm64-targz-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-linux-arm64.tar.gz
[linux-arm64-targz-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-linux-arm64.tar.gz.sha

[linux-arm64-badge-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/linux_arm64_Release_version_badge.svg
[linux-arm64-version-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/latest.version
[linux-arm64-targz-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-linux-arm64.tar.gz
[linux-arm64-targz-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-linux-arm64.tar.gz.sha

[linux-arm64-badge-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/linux_arm64_Release_version_badge.svg
[linux-arm64-version-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/latest.version
[linux-arm64-targz-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-linux-arm64.tar.gz
[linux-arm64-targz-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-linux-arm64.tar.gz.sha

[linux-arm64-badge-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/linux_arm64_Release_version_badge.svg
[linux-arm64-version-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/latest.version
[linux-arm64-targz-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-linux-arm64.tar.gz
[linux-arm64-targz-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-linux-arm64.tar.gz.sha

[linux-arm64-badge-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/linux_arm64_Release_version_badge.svg
[linux-arm64-version-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/latest.version
[linux-arm64-targz-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-linux-arm64.tar.gz
[linux-arm64-targz-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-linux-arm64.tar.gz.sha

[linux-arm64-badge-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/linux_arm64_Release_version_badge.svg
[linux-arm64-version-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/latest.version
[linux-arm64-targz-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-linux-arm64.tar.gz
[linux-arm64-targz-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-linux-arm64.tar.gz.sha

[linux-arm64-badge-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/linux_arm64_Release_version_badge.svg
[linux-arm64-version-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/latest.version
[linux-arm64-targz-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-linux-arm64.tar.gz
[linux-arm64-targz-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-linux-arm64.tar.gz.sha

[linux-arm64-badge-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/linux_arm64_Release_version_badge.svg
[linux-arm64-version-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/latest.version
[linux-arm64-targz-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-linux-arm64.tar.gz
[linux-arm64-targz-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-linux-arm64.tar.gz.sha

[rhel-6-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/rhel.6_x64_Release_version_badge.svg
[rhel-6-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[rhel-6-targz-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-rhel.6-x64.tar.gz
[rhel-6-targz-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-rhel.6-x64.tar.gz.sha

[rhel-6-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/rhel.6_x64_Release_version_badge.svg
[rhel-6-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[rhel-6-targz-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-rhel.6-x64.tar.gz
[rhel-6-targz-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-rhel.6-x64.tar.gz.sha

[rhel-6-badge-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/rhel.6_x64_Release_version_badge.svg
[rhel-6-version-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/latest.version
[rhel-6-targz-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-rhel.6-x64.tar.gz
[rhel-6-targz-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-rhel.6-x64.tar.gz.sha

[rhel-6-badge-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/rhel.6_x64_Release_version_badge.svg
[rhel-6-version-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/latest.version
[rhel-6-targz-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-rhel.6-x64.tar.gz
[rhel-6-targz-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-rhel.6-x64.tar.gz.sha

[rhel-6-badge-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/rhel.6_x64_Release_version_badge.svg
[rhel-6-version-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/latest.version
[rhel-6-targz-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-rhel.6-x64.tar.gz
[rhel-6-targz-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-rhel.6-x64.tar.gz.sha

[rhel-6-badge-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/rhel.6_x64_Release_version_badge.svg
[rhel-6-version-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/latest.version
[rhel-6-targz-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-rhel.6-x64.tar.gz
[rhel-6-targz-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-rhel.6-x64.tar.gz.sha

[rhel-6-badge-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/rhel.6_x64_Release_version_badge.svg
[rhel-6-version-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/latest.version
[rhel-6-targz-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-rhel.6-x64.tar.gz
[rhel-6-targz-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-rhel.6-x64.tar.gz.sha

[rhel-6-badge-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/rhel.6_x64_Release_version_badge.svg
[rhel-6-version-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/latest.version
[rhel-6-targz-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-rhel.6-x64.tar.gz
[rhel-6-targz-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-rhel.6-x64.tar.gz.sha

[rhel-6-badge-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/rhel.6_x64_Release_version_badge.svg
[rhel-6-version-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/latest.version
[rhel-6-targz-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-rhel.6-x64.tar.gz
[rhel-6-targz-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-rhel.6-x64.tar.gz.sha

[linux-musl-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/linux_musl_x64_Release_version_badge.svg
[linux-musl-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[linux-musl-targz-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-linux-musl-x64.tar.gz
[linux-musl-targz-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-linux-musl-x64.tar.gz.sha

[linux-musl-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/linux_musl_x64_Release_version_badge.svg
[linux-musl-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[linux-musl-targz-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-linux-musl-x64.tar.gz
[linux-musl-targz-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-linux-musl-x64.tar.gz.sha

[linux-musl-badge-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/linux_musl_x64_Release_version_badge.svg
[linux-musl-version-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/latest.version
[linux-musl-targz-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-linux-musl-x64.tar.gz
[linux-musl-targz-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-linux-musl-x64.tar.gz.sha

[linux-musl-badge-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/linux_musl_x64_Release_version_badge.svg
[linux-musl-version-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/latest.version
[linux-musl-targz-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-linux-musl-x64.tar.gz
[linux-musl-targz-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-linux-musl-x64.tar.gz.sha

[linux-musl-badge-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/linux_musl_x64_Release_version_badge.svg
[linux-musl-version-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/latest.version
[linux-musl-targz-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-linux-musl-x64.tar.gz
[linux-musl-targz-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-linux-musl-x64.tar.gz.sha

[linux-musl-badge-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/linux_musl_x64_Release_version_badge.svg
[linux-musl-version-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/latest.version
[linux-musl-targz-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-linux-musl-x64.tar.gz
[linux-musl-targz-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-linux-musl-x64.tar.gz.sha

[linux-musl-badge-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/linux_musl_x64_Release_version_badge.svg
[linux-musl-version-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/latest.version
[linux-musl-targz-2.1.7XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-linux-musl-x64.tar.gz
[linux-musl-targz-checksum-2.1.7XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.7xx/dotnet-sdk-latest-linux-musl-x64.tar.gz.sha

[linux-musl-badge-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/linux_musl_x64_Release_version_badge.svg
[linux-musl-version-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/latest.version
[linux-musl-targz-2.1.6XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-linux-musl-x64.tar.gz
[linux-musl-targz-checksum-2.1.6XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.6xx/dotnet-sdk-latest-linux-musl-x64.tar.gz.sha

[linux-musl-badge-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/linux_musl_x64_Release_version_badge.svg
[linux-musl-version-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/latest.version
[linux-musl-targz-2.1.5XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-linux-musl-x64.tar.gz
[linux-musl-targz-checksum-2.1.5XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.1.5xx/dotnet-sdk-latest-linux-musl-x64.tar.gz.sha

[win-arm-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/win_arm_Release_version_badge.svg
[win-arm-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[win-arm-zip-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-arm.zip
[win-arm-zip-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-arm.zip.sha

[win-arm-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/win_arm_Release_version_badge.svg
[win-arm-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[win-arm-zip-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-arm.zip
[win-arm-zip-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-win-arm.zip.sha

[win-arm-badge-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/win_arm_Release_version_badge.svg
[win-arm-version-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/latest.version
[win-arm-zip-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-win-arm.zip
[win-arm-zip-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-win-arm.zip.sha

[win-arm-badge-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/win_arm_Release_version_badge.svg
[win-arm-version-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/latest.version
[win-arm-zip-2.2.3XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-win-arm.zip
[win-arm-zip-checksum-2.2.3XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.3xx/dotnet-sdk-latest-win-arm.zip.sha

[win-arm-badge-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/win_arm_Release_version_badge.svg
[win-arm-version-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/latest.version
[win-arm-zip-2.2.2XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-win-arm.zip
[win-arm-zip-checksum-2.2.2XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.2xx/dotnet-sdk-latest-win-arm.zip.sha

[win-arm-badge-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/win_arm_Release_version_badge.svg
[win-arm-version-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/latest.version
[win-arm-zip-2.2.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-win-arm.zip
[win-arm-zip-checksum-2.2.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/2.2.1xx/dotnet-sdk-latest-win-arm.zip.sha

[win-arm-64-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/win_arm64_Release_version_badge.svg
[win-arm-64-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[win-arm-64-zip-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-arm64.zip

[freebsd-x64-badge-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/freebsd_x64_Release_version_badge.svg
[freebsd-x64-version-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
[freebsd-x64-zip-master]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-freebsd-x64.tar.gz
[freebsd-x64-zip-checksum-master]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-freebsd-x64.tar.gz.sha

[freebsd-x64-badge-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/freebsd_x64_Release_version_badge.svg
[freebsd-x64-version-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/latest.version
[freebsd-x64-zip-3.1.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-freebsd-x64.tar.gz
[freebsd-x64-zip-checksum-3.1.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.1.1xx/dotnet-sdk-latest-freebsd-x64.tar.gz.sha

[freebsd-x64-badge-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/freebsd_x64_Release_version_badge.svg
[freebsd-x64-version-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/latest.version
[freebsd-x64-zip-3.0.1XX]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-freebsd-x64.tar.gz
[freebsd-x64-zip-checksum-3.0.1XX]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/release/3.0.1xx/dotnet-sdk-latest-freebsd-x64.tar.gz.sha

[sdk-shas-2.2.1XX]: https://github.com/dotnet/versions/tree/master/build-info/dotnet/product/cli/release/2.2#built-repositories"""
