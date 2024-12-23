namespace AuthECApi.Middlewares.Jobsmart.Application.Middlewares
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        private readonly List<ApiKey> webApiKeys;

        private Func<string, IEnumerable<ApiKey>> SearchKeys => (apiKey) => webApiKeys.Where(q => q.Key == apiKey).ToList();

        public ApiKeyValidator(IConfiguration configuration)
        {
            webApiKeys = configuration.GetSection("ApiKeys").Get<List<ApiKey>>();
        }

        public bool Validate(HttpContext context)
        {
            string apiKey = context.Request.Headers["Api-Key"];
            if (string.IsNullOrWhiteSpace(apiKey)) return false;

            var foundKeys = SearchKeys(apiKey);
            return foundKeys.Any();
        }
    }
}
