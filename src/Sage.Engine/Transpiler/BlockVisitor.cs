﻿// Copyright (c) 2022, salesforce.com, inc.
// All rights reserved.
// SPDX-License-Identifier: Apache-2.0
// For full license text, see the LICENSE file in the repo root or https://opensource.org/licenses/Apache-2.0

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Sage.Engine.Parser;

namespace Sage.Engine.Transpiler;

/// <summary>
/// This visits blocks of AMPScript code, such as the contents of a file, what's inside an IF statement, or what's in a DO loop.
/// </summary>
internal class BlockVisitor : SageParserBaseVisitor<IEnumerable<StatementSyntax>>
{
    private readonly CSharpTranspiler _transpiler;

    public BlockVisitor(CSharpTranspiler transpiler)
    {
        this._transpiler = transpiler;
    }

    /// <summary>
    /// A content block can be HTML, AMP, SSJS, GUIDE - anything!
    /// </summary>
    public override IEnumerable<StatementSyntax> VisitContentBlock(SageParser.ContentBlockContext context)
    {
        foreach (SageParser.AmpOrEmbeddedContentContext block in context.ampOrEmbeddedContent())
        {
            foreach (StatementSyntax statement in base.Visit(block))
            {
                yield return statement;
            }
        }
    }

    /// <summary>
    /// An AmpBlock is an ampscript block contained within the %%[ ]%% braces.
    /// </summary>
    public override IEnumerable<StatementSyntax> VisitAmpBlock(SageParser.AmpBlockContext context)
    {
        for (int i = 0; i < context.ampStatement().Length; i++)
        {
            foreach (StatementSyntax statement in _transpiler.StatementVisitor.Visit(context.ampStatement(i)))
            {
                yield return statement;
            }
        }
    }

    public override IEnumerable<StatementSyntax> VisitInlineAmpBlock(SageParser.InlineAmpBlockContext context)
    {
        return new[]
        {
            ExpressionStatement(_transpiler.ExpressionVisitor.Visit(context.expression()))
        };
    }

    /// <summary>
    /// Inline HTML only needs to add the content to the output stream.
    ///
    /// Example:
    /// __outputStream.Append("<html><body></body></html>");
    /// </summary>
    public override IEnumerable<StatementSyntax> VisitInlineHtml(SageParser.InlineHtmlContext context)
    {
        return new[]
        {
            _transpiler.Runtime.EmitToOutputStream(context.GetText())
        };
    }
}
