namespace ForumSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<string[]>
            {
               new[] { "Sport", "https://us.123rf.com/450wm/sudowoodo/sudowoodo1804/sudowoodo180400048/99840238-ic%C3%B4nes-d-activit%C3%A9-de-triathlon-natation-course-v%C3%A9lo-ensemble-de-pictogrammes-sportifs-simples-logo-vectorie.jpg?ver=6" },
               new[] { "Coronavirus", "https://www.boma.org/images/Advocacy/State%20and%20Local/coronavirus1371x800.jpg" },
               new[] { "News", "https://www.lendacademy.com/wp-content/uploads/2015/05/Marketplace-Lending-News.jpg" },
               new[] { "Music", "https://vgywm.com/wp-content/uploads/2019/07/apple-music-note-800x420.jpg" },
               new[] { "Programming", "https://blogs.biomedcentral.com/on-physicalsciences/wp-content/uploads/sites/14/2019/02/data-1-620x342.jpg" },
               new[] { "Cats", "https://www.humanesociety.org/sites/default/files/styles/1240x698/public/2018/08/kitten-440379.jpg" },
               new[] { "Dogs", "https://img.webmd.com/dtmcms/live/webmd/consumer_assets/site_images/article_thumbnails/slideshows/surprises_about_dogs_and_cats_slideshow/1800x1200_surprises_about_dogs_and_cats_slideshow.jpg" },
            };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(
                    new Category()
                    {
                        Name = category[0],
                        Description = category[0],
                        Title = category[0],
                        ImageUrl = category[1],
                    });
            }
        }
    }
}
