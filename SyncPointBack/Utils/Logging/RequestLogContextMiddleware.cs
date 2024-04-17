using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace SyncPointBack.Utils.Logging
{
    public class RequestLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLogContextMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
            {
                return _next(context);
            }
        }
    }
}