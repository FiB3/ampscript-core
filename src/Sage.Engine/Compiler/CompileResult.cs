﻿// Copyright (c) 2022, salesforce.com, inc.
// All rights reserved.
// SPDX-License-Identifier: Apache-2.0
// For full license text, see the LICENSE file in the repo root or https://opensource.org/licenses/Apache-2.0

using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;

namespace Sage.Engine.Compiler
{
    /// <summary>
    /// Represents the results of compiling AMPscript, including all of the internal details of the compilation process
    /// </summary>
    /// <param name="ContentSourceFile">The source AMPscript code</param>
    /// <param name="CompilationUnit">The C# compilation unit produced by the transpiler</param>
    /// <param name="Compilation">The C# code compilation, produced by the C# compiler</param>
    /// <param name="EmitResult">The result of running the C# compiler and getting PDBs and assemblies</param>
    /// <param name="TranspiledSource">The transpiled C# source code representation</param>
    internal record CompileResult(
        string ContentSourceFile,
        CompilationUnitSyntax CompilationUnit,
        CSharpCompilation Compilation,
        EmitResult EmitResult,
        Assembly? Assembly,
        string TranspiledSource);
}
