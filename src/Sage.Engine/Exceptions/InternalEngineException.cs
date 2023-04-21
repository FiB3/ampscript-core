﻿// Copyright (c) 2022, salesforce.com, inc.
// All rights reserved.
// SPDX-License-Identifier: Apache-2.0
// For full license text, see the LICENSE file in the repo root or https://opensource.org/licenses/Apache-2.0

namespace Sage.Engine;
internal class InternalEngineException : Exception
{
    public InternalEngineException(string message)
        : base(message)
    {
    }
}
