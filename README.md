![Stride](sources/data/images/Logo/stride-logo-readme.png)

## Should you use this fork?

Probably not. I'm making changes to the engine that I find helpful, but will probably make the engine less useful for other people.

## About
Welcome to the Stride source code repository!

Stride is an open-source C# game engine for realistic rendering and VR. 
The engine is highly modular and aims at giving game makers more flexibility in their development.
Stride comes with an editor that allows you to create and manage the content of your games or applications visually and intuitively.

![Stride Editor](https://stride3d.net/images/external/script-editor.png)

To learn more about Stride, visit [stride3d.net](https://stride3d.net/).

## License and governance

### .NET Foundation

The original project is supported by the [.NET Foundation](https://dotnetfoundation.org).

### License

Stride is covered by the [MIT License](LICENSE.md) unless stated otherwise (i.e. for some files that are copied from other projects).

You can find the list of third party projects [here](THIRD%20PARTY.md).

### Contribution

I am not accepting contributions on this fork, submit changes to the original project.

## Documentation

This documentation is for the original project, it mostly holds true for this fork:
* [Stride Manual](https://doc.stride3d.net/latest/manual/index.html)
* [API Reference](https://doc.stride3d.net/latest/api/index.html)
* [Release Notes](https://doc.stride3d.net/latest/ReleaseNotes/index.html)

## Community

These links are for the original project, not this fork:
* [Chat with the community on Discord](https://discord.gg/f6aerfE) [![Join the chat at https://discord.gg/f6aerfE](https://img.shields.io/discord/500285081265635328.svg?style=flat&logo=discord&label=discord)](https://discord.gg/f6aerfE)
* [Discuss topics on our forums](http://forums.stride3d.net/)
* [Report engine issues](https://github.com/stride3d/stride/issues)
* [Donate to support the project](https://www.patreon.com/stride3d)
* [List of Projects made by users](https://github.com/stride3d/stride-community-projects)
* [Localization](docs/localization.md)

## Building from source

### Prerequisites

1. **Latest** [Git](https://git-scm.com/downloads) **with Large File Support** selected in the setup on the components dialog.
2. [Visual Studio 2019](https://www.visualstudio.com/downloads/) with the following workloads:
  * `.NET desktop development` with `.NET Framework 4.7.2 targeting pack`
  * `Desktop development with C++` with
    * `Windows 10 SDK (10.0.18362.0)` (it's currently enabled by default but it might change)
    * `MSVC v142 - VS2019 C++ x64/x86 build tools (v14.26)` or later version (should be enabled by default)
    * `C++/CLI support for v142 build tools (v14.26)` or later version **(not enabled by default)**
  * `.NET Core cross-platform development`
  * Optional (to target UWP): `Universal Windows Platform development` with
    * `Windows 10 SDK (10.0.18362.0)` or later version
    * `MSVC v142 - VS2019 C++ ARM build tools (v14.26)` or later version **(not enabled by default)**
  * Optional (to target iOS/Android): `Mobile development with .NET` and `Android SDK setup (API level 27)` individual component, then in Visual Studio go to `Tools > Android > Android SDK Manager` and install `NDK` (version 19+) from `Tools` tab.
3. **[FBX SDK 2019.0 VS2015](https://www.autodesk.com/developer-network/platform-technologies/fbx-sdk-2019-0)**

### Build Stride

1. Open a command prompt, point it to a directory and clone Stride to it: `git clone https://github.com/stride3d/stride.git`
2. Open `<StrideDir>\build\Stride.sln` with Visual Studio 2019 and build `Stride.GameStudio` (it should be the default startup project) or run it from VS's toolbar.
* Optionally, open and build `Stride.Android.sln`, `Stride.iOS.sln`, etc.

### Build Stride without Visual Studio

1. Install VS build tools with the same prerequisites listed above
2. Add MSBuild's directory to your system's *PATH*
3. Open a command prompt, point it to a directory and clone Stride to it: `git clone https://github.com/stride3d/stride.git`
4. Navigate to `/Build` with the command prompt, input `dotnet restore Stride.sln` then `compile`

For .Net 5.0 make sure that you have the latest SDK and runtime, navigate to `\sources\targets\Stride.Core.TargetFrameworks.Editor.props` and change `net472` to `net5.0-windows`

If building failed:
* If you skipped one of the `Prerequisites` thinking that you already have the latest version, update to the latest anyway just to be sure.
* Visual Studio might have issues properly building if an outdated version of 2017 is present alongside 2019. If you want to keep VS 2017 make sure that it is up to date and that you are building Stride through VS 2019.
* Some changes might require a system reboot, try that if you haven't yet.
* Make sure that git and visual studio can access the internet.
* Close VS, clear the nuget cache (in your cmd `dotnet nuget locals all --clear`), delete the hidden `.vs` folder inside `\build` and the files inside `bin\packages`, kill any msbuild and other vs processes, build the whole solution then build and run GameStudio.

Do note that test solutions might fail but it should not prevent you from building `Stride.GameStudio`.
