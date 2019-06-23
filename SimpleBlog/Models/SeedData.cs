using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleBlog.Models
{
    public class SeedData
    {
        private const string userName = "Admin";
        private const string userEmail = "admin@mail.com";
        private const string userPassword = "Secret123$";
        private const string userRole = "Admins";

        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                UserManager<User> userManager;
                userManager = scope.ServiceProvider.GetService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager =
                    scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                User user = await userManager.FindByIdAsync(userName);

                if (user == null)
                {
                    if (await roleManager.FindByNameAsync(userRole) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(userRole));
                    }

                    user = new User {UserName = userName, Email = userEmail};
                    IdentityResult result = await userManager
                        .CreateAsync(user, userPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, userRole);
                    }
                }

                ApplicationDbContext context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                if (!context.Posts.Any())
                {
                    await context.Posts.AddRangeAsync(
                        new Post
                        {
                            Title = "First Post",
                            Content =
                                "Lorem ipsum dolor sit amet, id assum dolores his, ne dolor laudem mea. Soleat constituto ullamcorper" +
                                " ei vis. Id qui velit fuisset, tale torquatos omittantur nam eu, vel causae reformidans id. " +
                                "Quis laudem an his. Cu amet docendi concludaturque cum, pri cu assum periculis. Maiorum facilisis sea cu." +
                                " Sed ut mundi accusamus. Diceret luptatum ex usu, ex vim tibique phaedrum. Sed nostrud legendos sadipscing et," +
                                " vis tale honestatis ex. Qui unum aeterno et, id aeterno vocibus temporibus sea, ut amet laboramus sed. " +
                                "His veri essent dicunt cu, ad alia tollit vim, zril copiosae vis in. Est causae rationibus ne, te has menandri " +
                                "lobortis. Qui no qualisque temporibus, dignissim definitionem ex pri, et nec illum dicant platonem.",
                            Author = user,
                            Category = new Category
                            {
                                Name = "Test Category 1"
                            },
                            PublishedDateTime = DateTime.Now,
                            Comments = new List<Comment>
                            {
                                new Comment
                                {
                                    User = user,
                                    Text =
                                        "Quis laudem an his. Cu amet docendi concludaturque cum, pri cu assum periculis.",
                                    DateTime = DateTime.Now
                                },
                                new Comment
                                {
                                    User = user,
                                    Text =
                                        "Quis laudem an his. Cu amet docendi concludaturque cum, pri cu assum periculis.",
                                    DateTime = DateTime.Now
                                },
                                new Comment
                                {
                                    User = user,
                                    Text =
                                        "Quis laudem an his. Cu amet docendi concludaturque cum, pri cu assum periculis.",
                                    DateTime = DateTime.Now
                                },
                                new Comment
                                {
                                    User = user,
                                    Text =
                                        "Quis laudem an his. Cu amet docendi concludaturque cum, pri cu assum periculis.",
                                    DateTime = DateTime.Now
                                },
                            }
                        },
                        new Post
                        {
                            Title = "Second Post",
                            Content =
                                "Lorem ipsum dolor sit amet, id assum dolores his, ne dolor laudem mea. Soleat constituto ullamcorper" +
                                " ei vis. Id qui velit fuisset, tale torquatos omittantur nam eu, vel causae reformidans id. " +
                                "Quis laudem an his. Cu amet docendi concludaturque cum, pri cu assum periculis. Maiorum facilisis sea cu." +
                                " Sed ut mundi accusamus. Diceret luptatum ex usu, ex vim tibique phaedrum. Sed nostrud legendos sadipscing et," +
                                " vis tale honestatis ex. Qui unum aeterno et, id aeterno vocibus temporibus sea, ut amet laboramus sed. " +
                                "His veri essent dicunt cu, ad alia tollit vim, zril copiosae vis in. Est causae rationibus ne, te has menandri " +
                                "lobortis. Qui no qualisque temporibus, dignissim definitionem ex pri, et nec illum dicant platonem.",
                            Author = user,
                            Category = new Category
                            {
                                Name = "Test Category 2"
                            },
                            PublishedDateTime = DateTime.Now,
                            Comments = new List<Comment>()
                        },
                        new Post
                        {
                            Title = "Third Post",
                            Content =
                                "Lorem ipsum dolor sit amet, id assum dolores his, ne dolor laudem mea. Soleat constituto ullamcorper" +
                                " ei vis. Id qui velit fuisset, tale torquatos omittantur nam eu, vel causae reformidans id. " +
                                "Quis laudem an his. Cu amet docendi concludaturque cum, pri cu assum periculis. Maiorum facilisis sea cu." +
                                " Sed ut mundi accusamus. Diceret luptatum ex usu, ex vim tibique phaedrum. Sed nostrud legendos sadipscing et," +
                                " vis tale honestatis ex. Qui unum aeterno et, id aeterno vocibus temporibus sea, ut amet laboramus sed. " +
                                "His veri essent dicunt cu, ad alia tollit vim, zril copiosae vis in. Est causae rationibus ne, te has menandri " +
                                "lobortis. Qui no qualisque temporibus, dignissim definitionem ex pri, et nec illum dicant platonem.",
                            Author = user,
                            Category = new Category
                            {
                                Name = "Test Category 3"
                            },
                            PublishedDateTime = DateTime.Now,
                            Comments = new List<Comment>()
                        },
                        new Post
                        {
                            Title = "Fourth Post",
                            Content =
                                "Lorem ipsum dolor sit amet, id assum dolores his, ne dolor laudem mea. Soleat constituto ullamcorper" +
                                " ei vis. Id qui velit fuisset, tale torquatos omittantur nam eu, vel causae reformidans id. " +
                                "Quis laudem an his. Cu amet docendi concludaturque cum, pri cu assum periculis. Maiorum facilisis sea cu." +
                                " Sed ut mundi accusamus. Diceret luptatum ex usu, ex vim tibique phaedrum. Sed nostrud legendos sadipscing et," +
                                " vis tale honestatis ex. Qui unum aeterno et, id aeterno vocibus temporibus sea, ut amet laboramus sed. " +
                                "His veri essent dicunt cu, ad alia tollit vim, zril copiosae vis in. Est causae rationibus ne, te has menandri " +
                                "lobortis. Qui no qualisque temporibus, dignissim definitionem ex pri, et nec illum dicant platonem.",
                            Author = user,
                            Category = new Category
                            {
                                Name = "Test Category 4"
                            },
                            PublishedDateTime = DateTime.Now,
                            Comments = new List<Comment>()
                        },
                        new Post
                        {
                            Title = "Fifth Post",
                            Content =
                                "Lorem ipsum dolor sit amet, id assum dolores his, ne dolor laudem mea. Soleat constituto ullamcorper" +
                                " ei vis. Id qui velit fuisset, tale torquatos omittantur nam eu, vel causae reformidans id. " +
                                "Quis laudem an his. Cu amet docendi concludaturque cum, pri cu assum periculis. Maiorum facilisis sea cu." +
                                " Sed ut mundi accusamus. Diceret luptatum ex usu, ex vim tibique phaedrum. Sed nostrud legendos sadipscing et," +
                                " vis tale honestatis ex. Qui unum aeterno et, id aeterno vocibus temporibus sea, ut amet laboramus sed. " +
                                "His veri essent dicunt cu, ad alia tollit vim, zril copiosae vis in. Est causae rationibus ne, te has menandri " +
                                "lobortis. Qui no qualisque temporibus, dignissim definitionem ex pri, et nec illum dicant platonem.",
                            Author = user,
                            Category = new Category
                            {
                                Name = "Test Category 5"
                            },
                            PublishedDateTime = DateTime.Now,
                            Comments = new List<Comment>()
                        },
                        new Post
                        {
                            Title = "Sixth Post",
                            Content =
                                "Lorem ipsum dolor sit amet, id assum dolores his, ne dolor laudem mea. Soleat constituto ullamcorper" +
                                " ei vis. Id qui velit fuisset, tale torquatos omittantur nam eu, vel causae reformidans id. " +
                                "Quis laudem an his. Cu amet docendi concludaturque cum, pri cu assum periculis. Maiorum facilisis sea cu." +
                                " Sed ut mundi accusamus. Diceret luptatum ex usu, ex vim tibique phaedrum. Sed nostrud legendos sadipscing et," +
                                " vis tale honestatis ex. Qui unum aeterno et, id aeterno vocibus temporibus sea, ut amet laboramus sed. " +
                                "His veri essent dicunt cu, ad alia tollit vim, zril copiosae vis in. Est causae rationibus ne, te has menandri " +
                                "lobortis. Qui no qualisque temporibus, dignissim definitionem ex pri, et nec illum dicant platonem.",
                            Author = user,
                            Category = new Category
                            {
                                Name = "Test Category 6"
                            },
                            PublishedDateTime = DateTime.Now,
                            Comments = new List<Comment>()
                        },
                        new Post
                        {
                            Title = "Seventh Post",
                            Content =
                                "Lorem ipsum dolor sit amet, id assum dolores his, ne dolor laudem mea. Soleat constituto ullamcorper" +
                                " ei vis. Id qui velit fuisset, tale torquatos omittantur nam eu, vel causae reformidans id. " +
                                "Quis laudem an his. Cu amet docendi sconcludaturque cum, pri cu assum periculis. Maiorum facilisis sea cu." +
                                " Sed ut mundi accusamus. Diceret luptatum ex usu, ex vim tibique phaedrum. Sed nostrud legendos sadipscing et," +
                                " vis tale honestatis ex. Qui unum aeterno et, id aeterno vocibus temporibus sea, ut amet laboramus sed. " +
                                "His veri essent dicunt cu, ad alia tollit vim, zril copiosae vis in. Est causae rationibus ne, te has menandri " +
                                "lobortis. Qui no qualisque temporibus, dignissim definitionem ex pri, et nec illum dicant platonem.",
                            Author = user,
                            Category = new Category
                            {
                                Name = "Test Category 7"
                            },
                            PublishedDateTime = DateTime.Now,
                            Comments = new List<Comment>()
                        }
                    );

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}