﻿namespace SqlcGenCsharp;

using System;
using System.IO;
using Google.Protobuf;
using Plugin;

public static class PluginRunner
{
    public static void Run()
    {
        var generateRequest = ReadInput();
        DebugHelper.Instance.Append("starting");
        var codeGenerator = new CodeGenerator(generateRequest);
        WriteOutput(codeGenerator.GenerateResponse);
    }

    private static GenerateRequest ReadInput()
    {
        DebugHelper.Instance.Append("reading input");
        using var memoryStream = new MemoryStream();
        Console.OpenStandardInput().CopyTo(memoryStream);
        memoryStream.Position = 0;
        var generateRequest = GenerateRequest.Parser.ParseFrom(memoryStream);
        return generateRequest;
    }

    private static void WriteOutput(GenerateResponse output)
    {
        DebugHelper.Instance.Append("writing output");
        var encodedOutput = output.ToByteArray();
        using var stdout = Console.OpenStandardOutput();
        stdout.Write(encodedOutput, 0, encodedOutput.Length);
    }
}