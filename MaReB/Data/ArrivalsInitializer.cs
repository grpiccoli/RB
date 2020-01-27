using System;
using System.Linq;
using MaReB.Models;
using System.IO;
using System.Diagnostics;

namespace MaReB.Data
{
    public class ArrivalsInitializer
    {
        public static void Initialize(ApplicationDbContext context, string path)
        {
            System.Globalization.CultureInfo.GetCultureInfo("es");
            context.Database.EnsureCreated();
            var tsvPath = Path.Combine(path, "Data");

            string os = Environment.OSVersion.Platform.ToString();

            string args = os == "Win32NT" ?
                $@"-S (localdb)\mssqllocaldb -d MaReB"
                : $@"-S localhost -d MaReB -U SA -P 34erdfERDF";

            if (os == "Unix")
            {
                var cpInfo = new ProcessStartInfo()
                {
                    FileName = "cp",
                    Arguments = $"{tsvPath}/*.tsv /tmp/",
                    RedirectStandardInput = false,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var getPath = new Process())
                {
                    getPath.StartInfo = cpInfo;
                    getPath.Start();
                    getPath.StandardOutput.ReadToEnd().TrimEnd(Environment.NewLine.ToCharArray());
                }
            }

            var pathInfo = new ProcessStartInfo()
            {
                FileName = "sqlcmd",
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            string output = string.Empty;

            if (!context.Arrival.Any())
            {
                var entries = Path.Combine(os == "Win32NT" ? tsvPath : "tmp", "Arrival.tsv");

                pathInfo.Arguments = args + $@" -Q ""BULK INSERT dbo.Arrival FROM '{entries}'""";

                using (var getPath = new Process())
                {
                    getPath.StartInfo = pathInfo;
                    getPath.Start();
                    output = getPath.StandardOutput.ReadToEnd().TrimEnd(Environment.NewLine.ToCharArray());
                }
            }
        }
    }
}