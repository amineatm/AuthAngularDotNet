namespace AuthECApi.Middlewares.Jobsmart.Application.Middlewares
{
    public interface IApiKeyValidator
    {
        bool Validate(HttpContext context);

    }
}