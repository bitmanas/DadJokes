using SwaggerApi.DummyData;
using System.Net;

namespace SwaggerApi.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY = "ApiKey";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            //In real scenario, this will be from any persistance storage to get the clients with its chosen plan           
            var clientList = DummyRegisteredClient.Clients;              
            if (!context.Request.Headers.TryGetValue(APIKEY, out var clientApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("ApiKey not provided");
                return;
            }

            //find the apikey of any registered client
            if (clientList.Where(x => x.ApiKey.Equals(clientApiKey)).ToList().Count ==0 )
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized access");
                return;
            }

            //increment hit count for every client request
            var client = clientList.FirstOrDefault(x => x.ApiKey.Equals(clientApiKey));            
            if (client.NoOfHitToday < client.Plan.MaxHitPerDay)
            {
                client.NoOfHitToday += 1;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.Response.WriteAsync($"{client.Plan.PlanType.ToString()} plan has daily limits {client.Plan.MaxHitPerDay} to access this service. You have reached the limit!");
                return;
            }

            await _next(context);
        }
    }
}
