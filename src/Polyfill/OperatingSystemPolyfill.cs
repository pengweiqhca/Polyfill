﻿// ReSharper disable RedundantUsingDirective

#pragma warning disable

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

// ReSharper disable UnnecessaryWhitespace
// ReSharper disable ArrangeMethodOrOperatorBody
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable InconsistentNaming
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Polyfills;

#if FeatureRuntimeInformation

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]

#if PolyPublic
public
#endif
static class OperatingSystemPolyfill
{
    static Version GetWindowsVersion()
    {
        if(IsWindows())
        {
            return Version.Parse(RuntimeInformation.OSDescription
                .Replace("Microsoft Windows", string.Empty)
                .Replace(" ", string.Empty));
        }
        else
        {
           return Environment.OSVersion.Version;
        }
    }

     static Version GetMacOSVersion()
    {
        if(IsMacOS())
        {
            string versionString = RunProcess(
                    CreateProcess("/usr/bin/sw_vers", ""))
                .Replace("ProductVersion:", string.Empty)
                .Replace(" ", string.Empty);

            return Version.Parse(versionString.Split(Environment.NewLine.ToCharArray())[0]);
        }
        else
        {
            return Environment.OSVersion.Version;
        }
    }

     static Version GetFreeBSDVersion()
    {
        if(IsFreeBSD())
        {
            string versionString = Environment.OSVersion.VersionString
                .Replace("Unix", string.Empty).Replace("FreeBSD", string.Empty)
                .Replace("-release", string.Empty).Replace(" ", string.Empty);

            return Version.Parse(versionString);
        }
        else
        {
            return Environment.OSVersion.Version;
        }
    }

    static Version GetAndroidVersion()
    {
        if(IsAndroid())
        {
            string result = RunProcess(
                    CreateProcess("getprop", "ro.build.version.release"))
                .Replace(" ", string.Empty);

            return Version.Parse(result);
        }
        else
        {
            return Environment.OSVersion.Version;
        }
    }

     static Process CreateProcess(string targetFileName, string arguments)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = targetFileName,
            Arguments = arguments,
            RedirectStandardInput = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process output = new Process
        {
            StartInfo = processStartInfo
        };

        return output;
    }

     static string RunProcess(Process process)
    {
        process.Start();
        process.WaitForExit();

        string output = process.StandardOutput.ReadToEnd();
        process.Close();
        return output;
    }

    /// <summary>
    /// Indicates whether the current application is running on the specified platform.
    /// </summary>
    /// <param name="platform">The case-insensitive platform name. Examples: Browser, Linux, FreeBSD, Android, iOS, macOS, tvOS, watchOS, Windows.</param>
    /// <returns>true if the current application is running on the specified platform; false otherwise.</returns>
    public static bool IsOSPlatform(string platform) => RuntimeInformation.IsOSPlatform(OSPlatform.Create(platform));

    /// <summary>
    /// Checks if the operating system version is greater than or equal to the specified platform version. This method can be used to guard APIs that were added in the specified OS version.
    /// </summary>
    /// <param name="platform">The case-insensitive platform name. Examples: Browser, Linux, FreeBSD, Android, iOS, macOS, tvOS, watchOS, Windows.</param>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number (optional).</param>
    /// <param name="build">The build release number (optional).</param>
    /// <param name="revision">The revision release number (optional).</param>
    /// <returns>true if the current application is running on the specified platform and is at least in the version specified in the parameters; false otherwise.</returns>
    public static bool IsOSPlatformVersionAtLeast(string platform, int major, int minor = 0, int build = 0, int revision = 0)
    => IsOSPlatform(platform) && IsOsVersionAtLeast(major, minor, build, revision);

    /// <summary>
    /// Indicates whether the current application is running on Windows.
    /// </summary>
    /// <returns>true if the current application is running on Windows; false otherwise.</returns>
    public static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);


    /// <summary>
    /// Checks if the Windows version (returned by RtlGetVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified Windows version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <param name="revision">The revision release number.</param>
    /// <returns>true if the current application is running on a Windows version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
    => IsWindows() && GetWindowsVersion() >= new Version(major, minor, build, revision);

    /// <summary>
    /// Indicates whether the current application is running on macOS.
    /// </summary>
    /// <returns>true if the current application is running on macOS; false otherwise.</returns>
    public static bool IsMacOS() => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    /// <summary>
    /// Indicates whether the current application is running on Mac Catalyst.
    /// </summary>
    /// <returns>true if the current application is running on Mac Catalyst; false otherwise.</returns>
    public static bool IsMacCatalyst() => IsMacOS() || IsIOS();

    /// <summary>
    /// Checks if the macOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified macOS version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <returns>true if the current application is running on an macOS version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsMacOSVersionAtLeast(int major, int minor = 0, int build = 0)
    => IsMacOS() && GetMacOSVersion() >= new Version(major, minor, build);

    /// <summary>
    /// Check for the Mac Catalyst version (iOS version as presented in Apple documentation) with a ≤ version comparison. Used to guard APIs that were added in the given Mac Catalyst release.
    /// </summary>
    /// <param name="major">The version major number.</param>
    /// <param name="minor">The version minor number.</param>
    /// <param name="build">The version build number.</param>
    /// <returns>true if the Mac Catalyst version is greater or equal than the specified version comparison; false otherwise.</returns>
    public static bool IsMacCatalystVersionAtLeast(int major, int minor = 0, int build = 0)
    => IsMacCatalyst() && IsOsVersionAtLeast(major, minor, build);

    /// <summary>
    /// Indicates whether the current application is running on Linux.
    /// </summary>
    /// <returns>true if the current application is running on Linux; false otherwise.</returns>
    public static bool IsLinux() => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    /// <summary>
    /// Indicates whether the current application is running on FreeBSD.
    /// </summary>
    /// <returns>true if the current application is running on FreeBSD; false otherwise.</returns>
    public static bool IsFreeBSD() => RuntimeInformation.OSDescription.ToLower().Contains("freebsd");

    /// <summary>
    ///Checks if the FreeBSD version (returned by the Linux command uname) is greater than or equal to the specified version.
    /// This method can be used to guard APIs that were added in the specified version.
    /// </summary>
    /// <param name="major">The version major number.</param>
    /// <param name="minor">The version minor number.</param>
    /// <param name="build">The version build number.</param>
    /// <param name="revision">The version revision number.</param>
    /// <returns>true if the current application is running on a FreeBSD version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsFreeBSDVersionAtLeast(int major, int minor, int build = 0, int revision = 0)
    => GetFreeBSDVersion() >= new Version(major, minor, build, revision);

    /// <summary>
    /// Indicates whether the current application is running on iOS or MacCatalyst.
    /// </summary>
    /// <returns>true if the current application is running on iOS or MacCatalyst; false otherwise.</returns>
    public static bool IsIOS()
    {
        string description = RuntimeInformation.OSDescription.ToLower();
        return description.Contains("ios") || description.Contains("ipados") ||
               (description.Contains("iphone") && description.Contains("os"));
    }

    /// <summary>
    /// Checks if the iOS/MacCatalyst version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified iOS version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <returns>true if the current application is running on an iOS/MacCatalyst version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsIOSVersionAtLeast(int major, int minor = 0, int build = 0)
    => IsIOS() && IsOsVersionAtLeast(major, minor, build);

    /// <summary>
    /// Indicates whether the current application is running on tvOS.
    /// </summary>
    /// <returns>true if the current application is running on tvOS; false otherwise.</returns>
    public static bool IsTvOS() => RuntimeInformation.OSDescription.ToLower().Contains("tvos");

    /// <summary>
    /// Checks if the tvOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified tvOS version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <returns>true if the current application is running on a tvOS version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsTvOSVersionAtLeast(int major, int minor = 0, int build = 0)
    => IsTvOS() && IsOsVersionAtLeast(major, minor, build);

    /// <summary>
    /// Indicates whether the current application is running on Android.
    /// </summary>
    /// <returns>true if the current application is running on Android; false otherwise.</returns>
    public static bool IsAndroid()
    {
        try
        {
            return RunProcess(CreateProcess("uname", "-o")).Replace(" ", string.Empty).ToLower().Equals("android");
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// Checks if the Android version (returned by the Linux command uname) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <param name="revision">The revision release number.</param>
    /// <returns>true if the current application is running on an Android version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsAndroidVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
    => IsAndroid() &&  GetAndroidVersion() >= new Version(major, minor, build, revision);

    /// <summary>
    /// Indicates whether the current application is running on watchOS.
    /// </summary>
    /// <returns>true if the current application is running on watchOS; false otherwise.</returns>
    public static bool IsWatchOS()
    => IsIOS() || RuntimeInformation.OSDescription.ToLower().Contains("watchos");

    /// <summary>
    /// Checks if the watchOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified watchOS version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <returns>true if the current application is running on a watchOS version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsWatchOSVersionAtLeast(int major, int minor = 0, int build = 0)
    => IsWatchOS() && IsOsVersionAtLeast(major, minor, build);

    /// <summary>
    /// Indicates whether the current application is running as WASI.
    /// </summary>
    /// <returns>true if running as WASI; false otherwise.</returns>
    public static bool IsWasi() => RuntimeInformation.FrameworkDescription.ToLower().Contains("wasi");

    /// <summary>
    /// Indicates whether the current application is running as WASM in a browser.
    /// </summary>
    /// <returns>true if running as WASM; false otherwise.</returns>
    public static bool IsBrowser() => RuntimeInformation.FrameworkDescription.Contains(".NET WebAssembly");

    static bool IsOsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
    => Environment.OSVersion.Version >= new Version(major, minor, build, revision);
}

#endif