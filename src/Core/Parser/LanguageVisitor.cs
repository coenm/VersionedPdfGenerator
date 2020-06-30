namespace Core.Parser
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.VariableProviders;

    public class LanguageVisitor : LanguageBaseVisitor<string>
    {
        private readonly Context _context;
        private readonly List<IVariableProvider> _providers;

        public LanguageVisitor(List<IVariableProvider> providers, Context context)
        {
            _context = context;
            _providers = providers.ToList();
        }

        public string InnerVisitMyvar2(LanguageParser.Myvar2Context context)
        {
            string key = context.AB(0).GetText();
            string args = null;

            if (context.SEP() != null)
            {
                args = context.AB(1).GetText();
            }

            var selectedProvider = _providers.FirstOrDefault(p => p.CanProvide(key));
            if (selectedProvider == null)
                return string.Empty;

            return selectedProvider.Provide(_context, key, args);
        }

        public override string VisitMyvar2(LanguageParser.Myvar2Context context)
        {
            return $"[{InnerVisitMyvar2(context)}]";
        }

        private string InnerVisitStatic(LanguageParser.StaticContext context)
        {
            return context.NAME().GetText();
        }

        public override string VisitStatic(LanguageParser.StaticContext context)
        {
            return $"[{InnerVisitStatic(context)}]";
        }

        protected override string AggregateResult(string aggregate, string nextResult)
        {
            if (aggregate is null)
                return nextResult;

            if (nextResult is null)
                return aggregate;

            return aggregate + nextResult;
        }
    }
}