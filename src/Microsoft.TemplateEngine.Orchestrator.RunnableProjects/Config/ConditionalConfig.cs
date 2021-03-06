using System;
using System.Collections.Generic;
using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateEngine.Abstractions.Mount;
using Microsoft.TemplateEngine.Core.Contracts;
using Microsoft.TemplateEngine.Core.Operations;
using Newtonsoft.Json.Linq;

namespace Microsoft.TemplateEngine.Orchestrator.RunnableProjects.Config
{
    public class ConditionalConfig : IOperationConfig
    {
        public int Order => -7000;

        public string Key => "conditionals";

        public Guid Id => new Guid("3E8BCBF0-D631-45BA-A12D-FBF1DE03AA38");

        public IEnumerable<IOperationProvider> Process(IComponentManager componentManager, JObject rawConfiguration, IDirectory templateRoot, IVariableCollection variables, IParameterSet parameters)
        {
            IReadOnlyList<string> ifToken = rawConfiguration.ArrayAsStrings("if");
            IReadOnlyList<string> elseToken = rawConfiguration.ArrayAsStrings("else");
            IReadOnlyList<string> elseIfToken = rawConfiguration.ArrayAsStrings("elseif");
            IReadOnlyList<string> actionableIfToken = rawConfiguration.ArrayAsStrings("actionableIf");
            IReadOnlyList<string> actionableElseToken = rawConfiguration.ArrayAsStrings("actionableElse");
            IReadOnlyList<string> actionableElseIfToken = rawConfiguration.ArrayAsStrings("actionableElseif");
            IReadOnlyList<string> actionsToken = rawConfiguration.ArrayAsStrings("actions");
            IReadOnlyList<string> endIfToken = rawConfiguration.ArrayAsStrings("endif");
            string id = rawConfiguration.ToString("id");
            bool trim = rawConfiguration.ToBool("trim");
            bool wholeLine = rawConfiguration.ToBool("wholeLine");

            string evaluatorName = rawConfiguration.ToString("evaluator");
            ConditionEvaluator evaluator = EvaluatorSelector.Select(evaluatorName);

            ConditionalTokens tokenVariants = new ConditionalTokens
            {
                IfTokens = ifToken,
                ElseTokens = elseToken,
                ElseIfTokens = elseIfToken,
                EndIfTokens = endIfToken,
                ActionableElseIfTokens = actionableElseIfToken,
                ActionableElseTokens = actionableElseToken,
                ActionableIfTokens = actionableIfToken,
                ActionableOperations = actionsToken
            };

            yield return new Conditional(tokenVariants, wholeLine, trim, evaluator, id);
        }
    }
}