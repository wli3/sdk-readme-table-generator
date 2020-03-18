module TableGenerator.Reference

open System
open TableGenerator.Shared

// https://aka.ms/dotnet/net5/dev/Sdk/dotnet-sdk-win-x64.exe

// https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-win-x64.exe
// https://aka.ms/                         dotnet/net5/preview2/Sdk/dotnet-sdk-win-x64.exe

// https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/win_x64_Release_version_badge.svg
// https://aka.ms/                         dotnet/net5/preview2/Sdk/win_x64_Release_version_badge.svg

//  https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/latest.version
//  https://aka.ms/dotnet/net5/preview2/Sdk/latest.version

let referenceTemplate: ReferenceTemplate = {
    LegacyTemplate = """[{0}-badge-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/{3}_Release_version_badge.svg
[{0}-version-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/latest.version
[{0}-installer-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.exe
[{0}-installer-checksum-{1}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.exe.sha
[{0}-zip-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.zip
[{0}-zip-checksum-{1}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.zip.sha"""

    AkaMSTemplate = """[{0}-badge-{1}]: https://aka.ms/dotnet/{2}/Sdk/{3}_Release_version_badge.svg
[{0}-version-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/latest.version
[{0}-installer-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.exe
[{0}-installer-checksum-{1}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.exe.sha
[{0}-zip-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.zip
[{0}-zip-checksum-{1}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.zip.sha"""
}

let targzReferenceTemplate: ReferenceTemplate =
    """[{0}-badge-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/{3}_Release_version_badge.svg
[{0}-version-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/latest.version
[{0}-targz-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.tar.gz
[{0}-targz-checksum-{1}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.tar.gz.sha"""

let linuxArmNoArchitectureReferenceTemplate: ReferenceTemplate =
    """[{0}-badge-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/{3}_Release_version_badge.svg
[{0}-version-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/latest.version
[{0}-targz-{1}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.tar.gz
[{0}-targz-checksum-{1}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{2}/dotnet-sdk-latest-{0}.tar.gz.sha"""

let formatTemplate (platform: String) (template: ReferenceTemplate) (branch: Branch): Option<string> =
    Some
        (String.Format
            (template, platform, (branchNameShorten branch), branch.GitBranchName, (platform.Replace('-', '_'))))

let winX64ReferenceTemplate = formatTemplate "win-x64" referenceTemplate

let winX86ReferenceTemplate = formatTemplate "win-x86" referenceTemplate

let osxX64ReferenceTemplate branch =
    let template =
        """[osx-badge-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/osx_x64_Release_version_badge.svg
[osx-version-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/latest.version
[osx-installer-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-osx-x64.pkg
[osx-installer-checksum-{0}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-osx-x64.pkg.sha
[osx-targz-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-osx-x64.tar.gz
[osx-targz-checksum-{0}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-osx-x64.tar.gz.sha"""
    Some(String.Format(template, branchNameShorten branch, branch.GitBranchName))

let linuxX64ReferenceTemplate branch =
    let template =
        """[linux-badge-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/linux_x64_Release_version_badge.svg
[linux-version-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/latest.version
[linux-DEB-installer-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-x64.deb
[linux-DEB-installer-checksum-{0}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-x64.deb.sha
[linux-RPM-installer-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-x64.rpm
[linux-RPM-installer-checksum-{0}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-x64.rpm.sha
[linux-targz-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-linux-x64.tar.gz
[linux-targz-checksum-{0}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-linux-x64.tar.gz.sha"""
    Some(String.Format(template, branchNameShorten branch, branch.GitBranchName))

let linuxArmReferenceTemplate = formatTemplate "linux-arm" linuxArmNoArchitectureReferenceTemplate

let linuxArm64ReferenceTemplate = formatTemplate "linux-arm64" linuxArmNoArchitectureReferenceTemplate

let rhel6ReferenceTemplate branch =
    let template =
        """[rhel-6-badge-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/rhel.6_x64_Release_version_badge.svg
[rhel-6-version-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/latest.version
[rhel-6-targz-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-rhel.6-x64.tar.gz
[rhel-6-targz-checksum-{0}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-rhel.6-x64.tar.gz.sha"""
    Some(String.Format(template, branchNameShorten branch, branch.GitBranchName))

let linuxMuslReferenceTemplate branch =
    let template =
        """[linux-musl-badge-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/linux_musl_x64_Release_version_badge.svg
[linux-musl-version-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/latest.version
[linux-musl-targz-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-linux-musl-x64.tar.gz
[linux-musl-targz-checksum-{0}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-linux-musl-x64.tar.gz.sha"""
    Some(String.Format(template, branchNameShorten branch, branch.GitBranchName))

let winArmMuslReferenceTemplate branch =
    match getMajorMinor branch with
    | NoVersion -> None
    | MajorMinor { Major = major; Minor = minor } when major <= 2 && minor <= 1 -> None
    | _ ->
        let template =
            """[win-arm-badge-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/win_arm_Release_version_badge.svg
[win-arm-version-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/latest.version
[win-arm-zip-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-win-arm.zip
[win-arm-zip-checksum-{0}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-win-arm.zip.sha"""
        Some(String.Format(template, branchNameShorten branch, branch.GitBranchName))

let freebsdReferenceTemplate branch =
    match getMajorMinor branch with
    | NoVersion -> None
    | MajorMinor { Major = major; Minor = _minor } when major <= 2 -> None
    | _ ->
        let template =
            """[freebsd-x64-badge-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/freebsd_x64_Release_version_badge.svg
[freebsd-x64-version-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/latest.version
[freebsd-x64-zip-{0}]: https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-freebsd-x64.tar.gz
[freebsd-x64-zip-checksum-{0}]: https://dotnetclichecksums.blob.core.windows.net/dotnet/Sdk/{1}/dotnet-sdk-latest-freebsd-x64.tar.gz.sha"""
        Some(String.Format(template, branchNameShorten branch, branch.GitBranchName))

let templates =
    [ winX64ReferenceTemplate
      winX86ReferenceTemplate
      osxX64ReferenceTemplate
      linuxX64ReferenceTemplate
      linuxArmReferenceTemplate
      linuxArm64ReferenceTemplate
      rhel6ReferenceTemplate
      linuxMuslReferenceTemplate
      winArmMuslReferenceTemplate
      freebsdReferenceTemplate ]

let referenceList branches =
    String.Join
        (Environment.NewLine + Environment.NewLine,
         templates
         |> List.collect (fun template ->
             branches
             |> List.map template
             |> List.choose id))
