﻿using System.Collections.Generic;

namespace Mutant.Chicken
{
    public class EngineConfig
    {
        public static IReadOnlyList<string> DefaultLineEndings = new[] {"\r", "\n", "\r\n"};

        public static IReadOnlyList<string> DefaultWhitespaces = new[] {" ", "\t"};

        public EngineConfig(IReadOnlyList<string> whitespaces, IReadOnlyList<string> lineEndings, VariableCollection variables, string variableFormatString = "{0}")
        {
            Whitespaces = whitespaces;
            LineEndings = lineEndings;
            Variables = variables;
            VariableFormatString = variableFormatString;
        }

        public IReadOnlyList<string> LineEndings { get; }

        public string VariableFormatString { get; }

        public VariableCollection Variables { get; }

        public IReadOnlyList<string> Whitespaces { get; }
    }
}