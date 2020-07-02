namespace Core.Parser
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.VariableProviders;

    public class LanguageVisitor : LanguageBaseVisitor<string>
    {
        private readonly Context _context;
        private readonly bool _debug;
        private readonly List<IVariableProvider> _providers;

        public LanguageVisitor(List<IVariableProvider> providers, Context context, bool debug = false)
        {
            _context = context;
            _debug = debug; // this is really not okay.
            _providers = providers.ToList();
        }

        public string InnerVisitVariable(LanguageParser.VariableContext context)
        {
            var key = context.KEY().GetText();
            string args = null;

            var contextArg = context.arg;
            if (contextArg != null)
                args = contextArg.GetText();

            var selectedProvider = _providers.FirstOrDefault(p => p.CanProvide(key));
            if (selectedProvider == null)
                return string.Empty;

            return selectedProvider.Provide(_context, key, args);
        }

        public override string VisitVariable(LanguageParser.VariableContext context)
        {
            return _debug ? $"[{InnerVisitVariable(context)}]" : InnerVisitVariable(context);
        }

        private string InnerVisitText(LanguageParser.TextContext context)
        {
            return context.GetText();
        }

        public override string VisitText(LanguageParser.TextContext context)
        {
            return _debug ? $"[{InnerVisitText(context)}]" : InnerVisitText(context);
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