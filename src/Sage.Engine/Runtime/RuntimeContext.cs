﻿// Copyright (c) 2022, salesforce.com, inc.
// All rights reserved.
// SPDX-License-Identifier: Apache-2.0
// For full license text, see the LICENSE file in the repo root or https://opensource.org/licenses/Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sage.Engine.Runtime
{
    public partial class RuntimeContext
    {
        StringBuilder _outputStream = new StringBuilder();
        private Dictionary<string, object?> _variables = new();

        public object? GetVariable(string name)
        {
            if (!_variables.TryGetValue(name, out object? result))
            {
                return null;
            }

            return result;
        }

        public void SetVariable(string name, object? value)
        {
            _variables[name] = value;
        }

        public void Output(object? data)
        {
            _outputStream.Append(data?.ToString());
        }

        public void OutputLine(object? data)
        {
            _outputStream.AppendLine(data?.ToString());
        }

        public string FlushOutputStream()
        {
            string results = _outputStream.ToString();
            _outputStream.Clear();
            return results;
        }
    }
}
