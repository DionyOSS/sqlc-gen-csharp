using Google.Protobuf;
using System;
using File = Plugin.File;


namespace SqlcGenCsharp.Generators;

internal class CsprojGen(string projectName, Options options)
{
    public File GenerateFile()
    {
        var csprojContents = GetFileContents();
        return new File
        {
            Name = $"{projectName}.csproj",
            Contents = ByteString.CopyFromUtf8(csprojContents)
        };
    }

    private string GetFileContents()
    {
        var nullableProperty = options.DotnetFramework.LatestDotnetSupported() ? "\n<Nullable>enable</Nullable>" : "";
        return $"""
                <!--{Consts.AutoGeneratedComment}-->
                <!--Run the following to add the project to the solution:
                   dotnet sln add {projectName}/{projectName}.csproj
                  -->
                <Project Sdk="Microsoft.NET.Sdk">
                
                    <PropertyGroup>
                        <TargetFramework>{options.DotnetFramework.ToName()}</TargetFramework>{nullableProperty}
                        <OutputType>Library</OutputType>
                    </PropertyGroup>
                
                    <ItemGroup>
                        <PackageReference Include="{options.DriverName}" Version="{GetPackageVersion(options.DriverName)}"/>
                    </ItemGroup>

                </Project>
                """;
    }

    private static string GetPackageVersion(DriverName driverName)
    {
        return driverName switch
        {
            DriverName.Npgsql => "8.0.3",
            DriverName.MySqlConnector => "2.3.6",
            _ => throw new NotSupportedException($"unsupported driver: {driverName}")
        };
    }
}