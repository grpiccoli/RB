using MaReB.Data;
using MaReB.Models;
using MaReB.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Pluralize.NET.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MaReB.Services
{
    public class SeedService : ISeed
    {
        private readonly ILogger _logger;
        private readonly IStringLocalizer _localizer;
        public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _environment;
        private readonly string _os;
        private readonly string _conn;
        private readonly ApplicationDbContext _context;
        public SeedService(
            ILogger<SeedService> logger,
            IStringLocalizer<SeedService> localizer,
            IConfiguration configuration,
            IHostingEnvironment environment,
            ApplicationDbContext context
            //IUser user
            //Bulk bulk
            )
        {
            _logger = logger;
            _localizer = localizer;
            Configuration = configuration;
            _environment = environment;
            _os = Environment.OSVersion.Platform.ToString();
            _conn = Configuration.GetConnectionString($"{_os}Connection");
            _context = context;
            //_user = user;
            //_bulk = bulk;
        }
        public async Task Seed()
        {
            try
            {
                await AddProcedure().ConfigureAwait(false);
                var tsvPath = Path.Combine(_environment.ContentRootPath, "Data");
                if (!_context.ApplicationUsers.Any())
                    await User().ConfigureAwait(false);
                if (!_context.Continents.Any())
                    await Insert<Continent>(tsvPath).ConfigureAwait(false);
                if (!_context.Countries.Any())
                    await Insert<Country>(tsvPath).ConfigureAwait(false);

                if (!_context.Regions.Any())
                    await Insert<Region>(tsvPath).ConfigureAwait(false);
                if (!_context.Provinces.Any())
                    await Insert<Province>(tsvPath).ConfigureAwait(false);
                if (!_context.Communes.Any())
                    await Insert<Commune>(tsvPath).ConfigureAwait(false);

                if (!_context.Arrivals.Any())
                    await Insert<Arrival>(tsvPath).ConfigureAwait(false);

                if (!_context.Exports.Any())
                    await Insert<Export>(tsvPath).ConfigureAwait(false);

                if (!_context.Stations.Any())
                    await Insert<Station>(tsvPath).ConfigureAwait(false);

                if (!_context.Ports.Any())
                    await Insert<Port>(tsvPath).ConfigureAwait(false);

                if (!_context.Origins.Any())
                    await Insert<Origin>(tsvPath).ConfigureAwait(false);

                if (!_context.Captures.Any())
                    await Insert<Capture>(tsvPath).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _localizer["There has been an error while seeding the database."]);
                throw;
            }
        }
        public async Task AddProcedure()
        {
            string query = "select * from sysobjects where type='P' and name='BulkInsert'";
            var sp = @"CREATE PROCEDURE BulkInsert(@TableName NVARCHAR(50), @Tsv NVARCHAR(100))
AS
BEGIN 
DECLARE @SQLSelectQuery NVARCHAR(MAX)=''
SET @SQLSelectQuery = 'BULK INSERT ' + @TableName + ' FROM ' + QUOTENAME(@Tsv) + ' WITH (DATAFILETYPE=''widechar'')'
  exec(@SQLSelectQuery)
END";
            bool spExists = false;
            using (SqlConnection connection = new SqlConnection(_conn))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = query;
                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            spExists = true;
                            break;
                        }
                    }
                    if (!spExists)
                    {
                        command.CommandText = sp;
                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                spExists = true;
                                break;
                            }
                        }
                    }
                    connection.Close();
                }
            }
        }
        public async Task Insert<TSource>(string path)
        {
            var name = new Pluralizer().Pluralize(typeof(TSource).ToString().Split(".").Last());
            _context.Database.SetCommandTimeout(10000);
            var tableName = $"dbo.{name}";
            var tsv = Path.Combine(path, $"{name}.tsv");
            var tmp = Path.Combine(Path.GetTempPath(), $"{name}.tsv");
            File.Copy(tsv, tmp, true);
            await _context.Database
                .ExecuteSqlCommandAsync($"BulkInsert @p0, @p1;", tableName, tmp)
                .ConfigureAwait(false);
            File.Delete(tmp);
            return;
        }
        public async Task User()
        {
            var roleStore = new RoleStore<ApplicationRole>(_context);
            var userStore = new UserStore<ApplicationUser>(_context);

            if (!_context.ApplicationUserRoles.Any())
            {
                if (!_context.Users.Any())
                {
                    if (!_context.ApplicationRoles.Any())
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
                            await _context.ApplicationRoles.AddAsync(role).ConfigureAwait(false);
                        }
                        await _context.SaveChangesAsync().ConfigureAwait(false);
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
                        await _context.Users.AddAsync(user).ConfigureAwait(false);
                        await _context.SaveChangesAsync().ConfigureAwait(false);

                        foreach (var role in item.Roles)
                        {
                            var roller = _context.Roles.SingleOrDefault(r => r.Name == role);
                            user.Roles.Add(new IdentityUserRole<string>
                            {
                                UserId = user.Id,
                                RoleId = roller.Id
                            });
                        }
                        _context.Update(user);
                        await _context.SaveChangesAsync().ConfigureAwait(false);

                    }
                }
            }
            return;
        }
    }
}
