# History and decisions

Newest last. Each entry: what was asked or decided, by whom, and why. The entries before 2026-11-15 are reconstructed
from the commit messages and the package files in `..\.Documents\`; a file's date there is when it was last written, not
necessarily when the version was published (see `OPEN.md`).

## 2022 – 2026-07: the versions

| Date | Commit message | Package files in `..\.Documents\` |
| --- | --- | --- |
| 2022-10-16 | Initial commit |  |
| 2022-10-19 | Initial commit, first version |  |
| 2022-10-22 | Create README.md |  |
| 2023-05-16 | Maintenance update, .NET 7 support |  |
| 2023-05-25 | minor fix |  |
| 2025-03-26 | v1.0.4 | 1.0.3 (file of 2024-01-17), 1.0.4 |
| 2026-02-03 | 1.0.5.0 | 1.0.5 (file rewritten on 2026-07-12) |
| 2026-07-12 | Project file changes, no new version |  |

## 2026-11-15 – The `net11.0` target

**Zoltán's change:** `net11.0` added to `TargetFrameworks`, in all seven packages of the solution. The version is not
raised.

Zoltán also added `net11.0` to the `SupportedPlatform browser` condition. The project was not built at the first
analysis build (`Forge.Wasm.BrowserStorages` failed on `net11.0`). With Zoltán's `net11.0` reference there, it builds on
every target; it needs no `net11.0` reference of its own.

## 2026-11-15 – The knowledge base

Created at Zoltán's request, from the knowledge base of `Forge.Next.Shared` (`CLAUDE.md`, `AI/`), adapted to this
solution. First planned once for the solution folder; Zoltán decided that each package gets its own, because each
package folder is a git repository of its own. The rules in `CLAUDE.md` were carried over and adapted, not decided one
by one (see `OPEN.md`).

## 2026-11-15 – The package icon: the Forge mark

At Zoltán's request, `logo.png` of `Forge.Next.Shared` is this package's icon: the Forge mark, a hexagon with a smaller,
filled hexagon inside, the two joined by three lines, on a dark rounded tile, 128 by 128 pixels. It sits in the
repository folder, like `LICENSE` and `README.md`, so that each git repository holds what its package needs;
`PackageIcon` names it, and it is packed to the package root. The published versions have no icon; the next version
carries it.

## 2026-11-15 – Packaging analysis

At Zoltán's request, the packaging of the solution was analysed, with `Forge.Next.Shared` as the model: the project
files, the package files in `..\.Documents\`, and builds of a copy of the solution (so that `..\.Documents\` and
`..\.Output\` stayed untouched). The findings for this package are in `OPEN.md`, the checks in `VERIFICATION.md`.
Nothing was fixed in the analysis itself, apart from the icon above; Zoltán's changes after it are recorded in this
file.

## 2026-11-15 – `net6.0` and `net7.0` dropped

**Zoltán's change,** after the analysis listed them as out of support: `net6.0` and `net7.0` left `TargetFrameworks`,
with their reference groups and `SupportedPlatform` conditions. The console harness `..\Testing.Security.Jwt` moved to
`net10.0`. A consumer on `net6.0` or `net7.0` gets the `netstandard2.0` build of the new version. A breaking change for
those consumers, so the version is raised by a minor step (below).

## 2026-11-15 – One XML documentation file per target

At Zoltán's request (a finding of the analysis), as in `Forge.Next.Shared` 3.3.2: `GenerateDocumentationFile` instead of
`DocumentationFile`. The `<None Remove="Forge.Wasm.BrowserStorages.NewtonSoft.Json.xml" />` item and the `.gitignore`
entry (`# For Future`, `Forge.Wasm.BrowserStorages.NewtonSoft.Json.xml`) were removed, and the file in the project
folder deleted. Each target writes its own file under `obj/<configuration>/<target>/`; the build copies it next to the
assembly, the post-build step to `..\.Output\<target>\`, and the pack takes it from there.

## 2026-11-15 – 1.0.5.0 → 1.1.0.0

At Zoltán's request. A minor version, because `net6.0` and `net7.0` were dropped; besides: `net11.0`, the icon, the XML
documentation per target. For publishing: the package depends on `Forge.Wasm.BrowserStorages` 1.1.0, which is published
before it.

## 2026-11-15 – The commit before the Release build

The analysis found that `Forge.Security.Jwt.Shared` 1.8.5 and `Forge.Security.Jwt.Service` 1.7.3 were built before their
commits, so their packages name an earlier commit. Zoltán: the published packages stay as they are; from now on the
change is committed before the Release build. A release step in `CLAUDE.md`.

## 2026-11-15 – `Forge.snk` instead of `Forge.pfx`

Zoltán decided to sign with a `.snk` exported from `Forge.pfx`, so that `dotnet build` and `dotnet pack` can build the
packages as well. `..\.Documents\Export-ForgeSnk.ps1` asks for the password of the `.pfx`, and writes
`..\.Documents\Forge.snk` only after checking that the key gives the public key of the signed assemblies (token
`97960309cfe8d317`). Found while testing it: .NET writes `CALG_RSA_KEYX` into the key blob whatever the `KeyNumber`, and
the public key, so the token, would then differ; the script writes `CALG_RSA_SIGN`, as `sn.exe` does. The `.snk` has no
password: whoever has it can sign as Forge. Zoltán ran it the same day. The `.snk` holds a 1024-bit key with
`CALG_RSA_SIGN`, its public key is that of the signed assemblies (token `97960309cfe8d317`), and its private key signs.
All seven projects now name `..\.Documents\Forge.snk` in `AssemblyOriginatorKeyFile`; this also put right the three
projects (`Forge.Security.Jwt.Service.Storage.SqlServer`, `Forge.Wasm.BrowserStorages`,
`Forge.Wasm.BrowserStorages.NewtonSoft.Json`) that named `..\..\.Documents\Forge.pfx`, the `.Documents` folder above the
solution folder. `dotnet build` builds the solution and its packages since, with the same public key token
(`VERIFICATION.md`). `Forge.pfx` stays in `..\.Documents\`; no project names it.

## 2026-11-15 – 1.1.0 → 1.1.1

Zoltán raised the version once more the same day (a patch step); the reason is not recorded. Neither version is
published: on nuget.org 1.0.5 is the latest (checked on 2026-11-15).

## 2026-11-15 – The other findings fixed

Zoltán commented out the post-build `xcopy` target in all seven projects, so a project builds alone as well. Then, at
Zoltán's request, the other findings:
- in this project: `GeneratePackageOnBuild` for Release only, as in the other projects, so a Debug build no longer
  writes a package; the README's first sentence ("Newtonsoft.Json").
- in all seven: the license is stated as `PackageLicenseExpression` `MIT` instead of `PackageLicenseFile`; the `LICENSE`
  file is still packed, since the MIT license asks for the notice to go with the copies.

The seven projects build with no warning since (the console harness `..\Testing.Security.Jwt` has eight of its own). Not
changed: a Release build still writes the package of the current version into `..\.Documents\` (`OPEN.md`).
