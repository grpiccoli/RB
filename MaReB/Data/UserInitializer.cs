using MaReB.Data;
using MaReB.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaReB.Models
{
    public class UserInitializer
    {
        public static Task Initialize(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<ApplicationRole>(context);
            var userStore = new UserStore<ApplicationUser>(context);

            if (!context.AppUserRole.Any())
            {
                if (!context.Users.Any())
                {
                    if (!context.ApplicationRole.Any())
                    {
                        var applicationRoles = new List<ApplicationRole> { };
                        foreach (var item in RoleData.AppRoles)
                        {
                            applicationRoles.Add(
                                new ApplicationRole
                                {
                                    CreatedDate = DateTime.Now,
                                    Name = item,
                                    Description = "",
                                    NormalizedName = item.ToLower()
                                });
                        };

                        foreach (var role in applicationRoles)
                        {
                            context.ApplicationRole.Add(role);
                        }
                        context.SaveChanges();
                    }

                    var users = new UserInitializerVM[]
                    {
                        new UserInitializerVM
                        {
                            Name = "Guillermo Rodriguez",
                            Email = "guillermo.rodriguez@mareb.cl",
                            Roles = RoleData.AppRoles.ToArray(),
                            Key = "mareb2018",
                            Image = "/images/ico/capia.svg",
                            Claims = ClaimData.UserClaims.ToArray()
                        }
                    };

                    foreach (var item in users)
                    {
                        var user = new ApplicationUser
                        {
                            UserName = item.Name,
                            NormalizedUserName = item.Name.ToLower(),
                            Email = item.Email,
                            NormalizedEmail = item.Email.ToLower(),
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            SecurityStamp = Guid.NewGuid().ToString(),
                            ProfileImageUrl = item.Image
                        };

                        var hasher = new PasswordHasher<ApplicationUser>();
                        var hashedPassword = hasher.HashPassword(user, item.Key);
                        user.PasswordHash = hashedPassword;

                        foreach (var claim in item.Claims)
                        {
                            user.Claims.Add(new IdentityUserClaim<string>
                            {
                                ClaimType = claim,
                                ClaimValue = claim
                            });
                        }
                        context.Users.Add(user);
                        context.SaveChanges();

                        foreach (var role in item.Roles)
                        {
                            var roller = context.Roles.SingleOrDefault(r => r.Name == role);
                            user.Roles.Add(new IdentityUserRole<string> {
                                UserId = user.Id,
                                RoleId = roller.Id
                            });
                        }
                        context.Update(user);
                        context.SaveChanges();

                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
