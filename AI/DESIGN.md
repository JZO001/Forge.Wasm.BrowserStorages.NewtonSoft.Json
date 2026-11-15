# Design

## The parts

| File / folder | Namespace | Role |
| --- | --- | --- |
| `Abstraction/` | `….Abstraction` | `JsonSerializerBase<TOptions>`, `SerializerOptionsBase` |
| `LocalStorage/`, `SessionStorage/` | `….LocalStorage`, `….SessionStorage` | `JsonSerializer`, `SerializerOptions` and the registration extensions of each storage |

## The targets

| Target | Package references |
| --- | --- |
| `netstandard2.0` | `Newtonsoft.Json` 13.0.4 |
| `net8.0` | `Newtonsoft.Json` 13.0.4 |
| `net9.0` | `Newtonsoft.Json` 13.0.4 |
| `net10.0` | `Newtonsoft.Json` 13.0.4 |
| `net11.0` | `Newtonsoft.Json` 13.0.4 |

`Forge.Wasm.BrowserStorages` (project reference) on every target.

## The build and the package

- SDK: `Microsoft.NET.Sdk`. `SignAssembly` with `..\.Documents\Forge.snk`.
- `GenerateDocumentationFile`: each target writes its own `Forge.Wasm.BrowserStorages.NewtonSoft.Json.xml` under
  `obj/<configuration>/<target>/`, and the build copies it next to the assembly (since 2026-11-15; before, one
  `DocumentationFile` in the project folder served every target).
- `GeneratePackageOnBuild` for Release (in every configuration until 2026-11-15); `PackageOutputPath` is
  `..\.Documents\`.
- The package holds `lib/<target>/Forge.Wasm.BrowserStorages.NewtonSoft.Json.dll` and `.xml` for every target, and at
  its root `LICENSE` (the license is stated as `PackageLicenseExpression` `MIT` since 2026-11-15; the file is still
  packed), `README.md` (`PackageReadmeFile`) and `logo.png` (`PackageIcon`, from 2026-11-15).
- The post-build copy (`xcopy` of the `.dll`, `.pdb`, `.xml` to `$(SolutionDir).Output\<target>\`) is commented out in
  the project file (Zoltán, 2026-11-15).
- The `Scc*` properties (`SAK`) and the `.vspscc` file belong to the TFS binding of the solution.

## Why each choice was made

- **One package version of the framework libraries per target:** the same pattern as in `Forge.Next.Shared`, where the
  reason is recorded: the package does not force a newer framework package on a consumer than its framework's own.
- **Strong naming:** the seven packages of the solution are signed with the same key, so the assemblies have a stable
  identity. Until 2026-11-15 the projects named the password-protected `Forge.pfx`, which only Visual Studio's MSBuild
  can sign with; since then its key pair as `Forge.snk`, which `dotnet build` can use too.
- **One Newtonsoft.Json version for every target:** Newtonsoft.Json is not part of any framework, and 13.0.4 supports
  all the targets.
