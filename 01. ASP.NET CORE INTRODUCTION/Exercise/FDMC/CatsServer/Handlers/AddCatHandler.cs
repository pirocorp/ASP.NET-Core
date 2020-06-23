namespace CatsServer.Handlers
{
    using System;
    using CatServer.Infrastructure.Common;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class AddCatHandler : IHandler
    {
        public int Order => 2;

        public Func<HttpContext, bool> Condition
            => ctx => ctx.Request.Path.Value == "/cat/add";

        public RequestDelegate RequestHandler
            => async (context) =>
            {
                if (context.Request.Method == HttpMethod.Get)
                {
                    context.Response.StatusCode = 302;
                    context.Response.Headers.Add("Location", "/cats-add-form.html");
                }
                else if (context.Request.Method == HttpMethod.Post)
                {
                    if (!context.Request.HasFormContentType)
                    {
                        context.Response.Redirect("/cats-add-form.html");
                    }
                    else
                    {
                        var db = context.RequestServices.GetService<CatsDbContext>();

                        var formData = context.Request.Form;

                        var isParsed = int.TryParse(formData["Age"], out var age);

                        if (!isParsed)
                        {
                            context.Response.Redirect("/cats-add-form.html");
                        }

                        var cat = new Cat()
                        {
                            Name = formData["Name"],
                            Age = age,
                            Breed = formData["Breed"],
                            ImageUrl = formData["ImageUrl"],
                        };

                        if (string.IsNullOrWhiteSpace(cat.Name) ||
                            string.IsNullOrWhiteSpace(cat.Breed) ||
                            string.IsNullOrWhiteSpace(cat.ImageUrl))
                        {
                            context.Response.Redirect("/cats-add-form.html");
                            return;
                        }

                        db.Add(cat);

                        try
                        {
                            await db.SaveChangesAsync();
                            context.Response.Redirect("/");
                        }
                        catch (Exception e)
                        {
                            await context.Response.WriteAsync("<h2>Invalid cat data!</h2>");
                            await context.Response.WriteAsync(@"<a href=""/cat/add"">Back</h2>");
                        }
                    }
                }
            };
    }
}
