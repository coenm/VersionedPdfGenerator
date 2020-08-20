namespace WebHost
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core;
    using Core.VariableProviders;
    using Microsoft.AspNetCore.Hosting.Server;
    using Microsoft.AspNetCore.Hosting.Server.Features;

    internal class WebHostVariableProvider : IVariableProvider
    {
        private readonly IServer _server;
        private const string KEY = "Host";

        internal WebHostVariableProvider(IServer server)
        {
            _server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public bool CanProvide(string key)
        {
            if (key is null)
                return false;

            return key.Equals(KEY, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return GetHostUrl();
        }

        public IEnumerable<VariableDescription> Get()
        {
            yield return new VariableDescription(KEY, $"The host url of the asp web server (ending with '/'). The current value is '{GetHostUrl()}'.");
        }

        private string GetHostUrl()
        {
            var addresses = _server.Features.Get<IServerAddressesFeature>()?.Addresses?.ToArray();

            if (addresses == null || addresses.Length == 0)
                return string.Empty;

            var result = addresses[0];
            if (result.EndsWith("/"))
                return result;
            return result + "/";
        }
    }
}
