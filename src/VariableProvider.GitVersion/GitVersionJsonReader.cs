namespace VariableProvider.GitVersion
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class GitVersionJsonReader
    {
        private readonly JObject _jsonObject;

        public GitVersionJsonReader(string jsonContent)
        {
            _jsonObject = JsonConvert.DeserializeObject(jsonContent) as JObject;
        }

        public string GetValue(string key)
        {
            var result = _jsonObject[key];

            if (!(result is null))
                return result?.ToString();

            foreach (var k in _jsonObject)
            {
                if (key.Equals(k.Key, StringComparison.CurrentCultureIgnoreCase))
                {
                    return k.Value?.ToString();
                }
            }

            return string.Empty;
        }
    }
}