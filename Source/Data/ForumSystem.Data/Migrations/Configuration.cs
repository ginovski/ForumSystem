namespace ForumSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using ForumSystem.Common;
    using ForumSystem.Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            // TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.SeedRoles(context);
            this.SeedAdmin(context);
            this.SeedPostsWithTags(context);
        }

        private void SeedAdmin(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var admin = new ApplicationUser()
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com"
            };

            userManager.Create(admin, "admin123456");
            userManager.AddToRole(admin.Id, "Admin");

            for (int i = 0; i < 10; i++)
            {
                var user = new ApplicationUser
                {
                    Email = string.Format(
                            "{0}@{1}.com",
                            RandomDataGenerator.Instance.GetRandomString(6, 16),
                            RandomDataGenerator.Instance.GetRandomString(6, 16)),
                    UserName = RandomDataGenerator.Instance.GetRandomString(6, 16)
                };

                userManager.Create(user, "123456");
            }

            context.SaveChanges();
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var adminRole = new IdentityRole { Name = "Admin" };
            roleManager.Create(adminRole);

            context.SaveChanges();
        }

        private void SeedPostsWithTags(ApplicationDbContext context)
        {
            if (context.Posts.Any())
            {
                return;
            }

            var users = context.Users.Take(10).ToList();
            for (int j = 0; j < 10; j++)
            {
                var post = new Post
                {
                    Author = users[RandomDataGenerator.Instance.GetRandomInt(0, users.Count - 1)],
                    Title = RandomDataGenerator.Instance.GetRandomString(5, 40),
                    Content = RandomDataGenerator.Instance.GetRandomString(20, 40),
                    Vote = new Vote
                    {
                        Value = 0
                    }
                };

                for (int k = 0; k < 3; k++)
                {
                    var tag = new Tag
                    {
                        Name = RandomDataGenerator.Instance.GetRandomString(5, 40),
                    };

                    post.Tags.Add(tag);
                }

                context.Posts.Add(post);
                context.SaveChanges();
            }
        }
    }
}
