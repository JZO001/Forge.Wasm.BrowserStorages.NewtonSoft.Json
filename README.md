# Forge.Wasm.BrowserStorages.NewtonSoft.Json
Forge.Wasm.BrowserStorages.NewtonSoft.Json is a library extension that provides Nettonsoft.Json serialization provider Forge.Wasm.BrowserStorages library.


## Installing

To install the package add the following line to you csproj file replacing x.x.x with the latest version number:

```
<PackageReference Include="Forge.Wasm.BrowserStorages.NewtonSoft.Json" Version="x.x.x" />
```

You can also install via the .NET CLI with the following command:

```
dotnet add package Forge.Wasm.BrowserStorages.NewtonSoft.Json
```

If you're using Visual Studio you can also install via the built in NuGet package manager.

## Setup

You will need to register the local storage services with the service collection in your _Startup.cs_ file in Blazor Server.

```c#
public void ConfigureServices(IServiceCollection services)
{
	// ... preinitialization steps

	// always add this code after the "Forge.Wasm.BrowserStorages" library initialization
	services.AddForgeLocalStorageWithNewtonsoftSerializer();
	services.AddForgeSessionStorageWithNewtonsoftSerializer();
}
``` 

Or in your _Program.cs_ file in Blazor WebAssembly.

```c#
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

	// ... preinitialization steps

	// always add this code after the "Forge.Wasm.BrowserStorages" library initialization
    builder.Services.AddForgeLocalStorageWithNewtonsoftSerializer();
    builder.Services.AddForgeSessionStorageWithNewtonsoftSerializer();

    await builder.Build().RunAsync();
}
```

### Registering services as Singleton
If you would like to register the serialization provider as singletons, it is possible by using the following method:

```csharp
builder.Services.AddForgeLocalStorageAsSingletonWithNewtonsoftSerializer();
builder.Services.AddForgeSessionStorageAsSingletonWithNewtonsoftSerializer();
```

This method is not recommended in the most cases, try to avoid using it.
