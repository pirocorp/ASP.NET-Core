namespace CatsServer.Handlers
{
    using System;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// These acts like Actions in Controllers
    /// </summary>
    public interface IHandler
    {
        int Order { get; }

        Func<HttpContext, bool> Condition { get; }

        RequestDelegate RequestHandler { get; }
    }
}
