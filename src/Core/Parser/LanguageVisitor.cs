namespace Core.Parser
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Formatters;
    using Core.VariableProviders;

    public class LanguageVisitor : LanguageBaseVisitor<string>
    {
        private readonly List<IMethod> _methods;
        private readonly Context _context;
        private readonly List<IVariableProvider> _providers;

        public LanguageVisitor(List<IVariableProvider> providers, List<IMethod> methods, Context context)
        {
            _context = context;
            _providers = providers.ToList();
            _methods = methods.ToList();
        }

        public override string VisitVariable(LanguageParser.VariableContext context)
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

        public override string VisitText(LanguageParser.TextContext context)
        {
            return context.GetText();
        }

        public override string VisitFunction(LanguageParser.FunctionContext context)
        {
            var method = context.KEY().GetText();
            var arg = Visit(context.arg);

            var m = _methods.FirstOrDefault(x => x.CanHandle(method));
            if (m == null)
                return method + arg;

            return m.Handle(method, arg);
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