﻿namespace MinimalAPI.Server.Filters;

public class ProblemDetailStartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {

        return app =>
        {
            app.Use(nextRequestDelegate =>
            {
                return async httpContext =>
                {
                    await nextRequestDelegate(httpContext);

                    var endpoint = httpContext.GetEndpoint();
                    if (endpoint != null && httpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
                    {
                        httpContext.Response.Headers.Append("TEST_FLAGS", "ERROR");
                    }
                };
            });
            next(app);
        };
    }
}