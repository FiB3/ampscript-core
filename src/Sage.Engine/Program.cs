﻿using System.CommandLine;
using CommandHandler = System.CommandLine.NamingConventionBinder.CommandHandler;
using Microsoft.CodeAnalysis;
using Sage.Engine.Compiler;
using CompilationOptions = Sage.Engine.Compiler.CompilationOptions;

Argument<string> sourceFile = new Argument<string>(name: "--source", description: "Path to the AMPscript program to debug").LegalFilePathsOnly();
Option<bool> debugOption = new(new[] { "--debug", "-d" }, "Whether or not to build debug information and debug the output");

RootCommand rootCommand = new(description: "Run an AMPscript program under the debugger")
{
    sourceFile,
    debugOption
};

rootCommand.Handler = CommandHandler.Create((string source, bool debug, IConsole console) =>
{
    DirectoryInfo tempPath = Directory.CreateTempSubdirectory("Sage");

    var optimizeLevel = debug ? OptimizationLevel.Debug : OptimizationLevel.Release;

    var options =
        new CompilationOptions(source, tempPath.FullName, optimizeLevel);
    
    try
    {
        console.Write(CSharpCompiler.CompileAndExecute(options));
    }
    catch (Exception e)
    {
        Console.Error.WriteLine(e);
        return -1;
    }

    return 0;
});

rootCommand.Invoke(args);