namespace CatsServer.Handlers
{
    using System;
    using CatServer.Infrastructure.Common;
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class CatDetailsHandler : IHandler
    {
        public int Order => 3;

        public Func<HttpContext, bool> Condition
            => ctx => ctx.Request.Path.Value.StartsWith("/cat")
                   && ctx.Request.Method == HttpMethod.Get;

        public RequestDelegate RequestHandler
            => async (context) =>
            {
                var urlParts = context.Request.Path.Value.Split('/');

                if (urlParts.Length < 2)
                {
                    context.Response.Redirect("/");
                }
                else
                {
                    var isParsed = int.TryParse(urlParts[2], out var catId);

                    if (!isParsed)
                    {
                        context.Response.Redirect("/");
                        return;
                    }

                    var db = context.RequestServices.GetService<CatsDbContext>();

                    await using (db)
                    {
                        var cat = await db.Cats.FindAsync(catId);

                        if (cat == null)
                        {
                            context.Response.Redirect("/");
                            return;
                        }

                        await context.Response.WriteAsync($"<h1>{cat.Name}</h1>");
                        await context.Response.WriteAsync($@"<img src=""{cat.ImageUrl}"" alt=""{cat.Name}"" width=""300""/>");
                        await context.Response.WriteAsync($"<p>Age: {cat.Age}</p>");
                        await context.Response.WriteAsync($"<h2>Breed: {cat.Breed}</h2>");
                    }
                }
            };
    }
}
