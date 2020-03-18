module TableGenerator.Table

open System
open TableGenerator.Shared

let notAvailable = "**N/A**"

let windowsDesktopArchTableTemplate =
    """[![][{0}-badge-{1}]][{0}-version-{1}]<br>[Installer][{0}-installer-{1}] - [Checksum][{0}-installer-checksum-{1}]<br>[zip][{0}-zip-{1}] - [Checksum][{0}-zip-checksum-{1}]"""

let linuxArmTableTemplate =
    "[![][{0}-badge-{1}]][{0}-version-{1}]<br>[tar.gz][{0}-targz-{1}] - [Checksum][{0}-targz-checksum-{1}]"

let joinListOfArchs (listOfArchs: List<string>): string = "| " + String.Join(" | ", listOfArchs) + " |"

let formRow rowTitle tableTemplateForThisArch branches =
    let inList =
        List.concat
            [ [ rowTitle ]
              (branches |> List.map tableTemplateForThisArch) ]
    joinListOfArchs inList

let windowsX64Row branches =
    let tableTemplateForThisArch branch =
        String.Format(windowsDesktopArchTableTemplate, "win-x64", branchNameShorten branch)
    formRow "**Windows x64**" tableTemplateForThisArch branches

let windowsX86Row branches =
    let tableTemplateForThisArch branch =
        String.Format(windowsDesktopArchTableTemplate, "win-x86", branchNameShorten branch)
    formRow "**Windows x86**" tableTemplateForThisArch branches

let osxRow branches =
    let osxDesktopArchTableTemplate =
        """[![][osx-badge-{0}]][osx-version-{0}]<br>[Installer][osx-installer-{0}] - [Checksum][osx-installer-checksum-{0}]<br>[tar.gz][osx-targz-{0}] - [Checksum][osx-targz-checksum-{0}]"""
    let tableTemplateForThisArch branch = String.Format(osxDesktopArchTableTemplate, branchNameShorten branch)
    formRow "**macOS**" tableTemplateForThisArch branches

let linuxDesktopArchRow branches =
    let tableTemplate =
        """[![][linux-badge-{0}]][linux-version-{0}]<br>[DEB Installer][linux-DEB-installer-{0}] - [Checksum][linux-DEB-installer-checksum-{0}]<br>[RPM Installer][linux-RPM-installer-{0}] - [Checksum][linux-RPM-installer-checksum-{0}]<br>_see installer note below_<sup>1</sup><br>[tar.gz][linux-targz-{0}] - [Checksum][linux-targz-checksum-{0}]"""
    let tableTemplateForThisArch branch = String.Format(tableTemplate, branchNameShorten branch)
    formRow "**Linux x64**" tableTemplateForThisArch branches

let linuxArmRow branches =
    let tableTemplateForThisArch branch = String.Format(linuxArmTableTemplate, "linux-arm", branchNameShorten branch)
    formRow "**Linux arm**" tableTemplateForThisArch branches

let linuxArmX64Row branches =
    let tableTemplateForThisArch branch = String.Format(linuxArmTableTemplate, "linux-arm64", branchNameShorten branch)
    formRow "**Linux arm64**" tableTemplateForThisArch branches

let rhel6Row branches =
    let tableTemplateForThisArch branch = String.Format(linuxArmTableTemplate, "rhel-6", branchNameShorten branch)
    formRow "**RHEL 6**" tableTemplateForThisArch branches

let linuxMuslRow branches =
    let tableTemplateForThisArch branch = String.Format(linuxArmTableTemplate, "linux-musl", branchNameShorten branch)
    formRow "**Linux-musl**" tableTemplateForThisArch branches

let windowsArmRow branches =
    let tableTemplate =
        "[![][win-arm-badge-{0}]][win-arm-version-{0}]<br>[zip][win-arm-zip-{0}] - [Checksum][win-arm-zip-checksum-{0}]"

    let tableTemplateForThisArch branch =
        match getMajorMinor branch with
        | NoVersion -> notAvailable
        | MajorMinor { Major = major; Minor = minor } when major <= 2 && minor <= 1 -> notAvailable
        | _ -> String.Format(tableTemplate, branchNameShorten branch)
    formRow "**Windows arm**" tableTemplateForThisArch branches

let windowsArm64Row branches =
    let tableTemplate =
        "[![][win-arm-64-badge-{0}]][win-arm-64-version-{0}]<br>[zip][win-arm-64-zip-{0}]"

    let tableTemplateForThisArch branch =
        match getMajorMinor branch with
        | NoVersion -> notAvailable
        | MajorMinor { Major = major; Minor = minor } when major <= 3 -> notAvailable
        | _ -> String.Format(tableTemplate, branchNameShorten branch)
    formRow "**Windows arm64**" tableTemplateForThisArch branches

let freebsdX64Row branches =
    let tableTemplate =
        "[![][freebsd-x64-badge-{0}]][freebsd-x64-version-{0}]<br>[tar.gz][freebsd-x64-zip-{0}] - [Checksum][freebsd-x64-zip-checksum-{0}] "

    let tableTemplateForThisArch branch =
        match getMajorMinor branch with
        | NoVersion -> notAvailable
        | MajorMinor { Major = major; Minor = _minor } when major <= 2 -> notAvailable
        | _ -> String.Format(tableTemplate, branchNameShorten branch)
    formRow "**FreeBSD x64**" tableTemplateForThisArch branches

let titleRow = formRow "Platform" (fun (b: Branch) -> b.DisplayName)

let separator = formRow ":---------" (fun _ -> ":----------:")

let ConstituentRepoShas =
    formRow "**Constituent Repo Shas**"
        (fun (b: Branch) -> if b.GitBranchName = "release/3.0.1xx" then "[Git SHAs][sdk-shas-2.2.1XX]" else notAvailable)

let rows =
    [ titleRow
      separator
      windowsX64Row
      windowsX86Row
      osxRow
      linuxDesktopArchRow
      linuxArmRow
      linuxArmX64Row
      rhel6Row
      linuxMuslRow
      windowsArmRow
      windowsArm64Row
      freebsdX64Row
      ConstituentRepoShas ]

let table branches = String.Join(Environment.NewLine, rows |> List.map (fun row -> row branches))
