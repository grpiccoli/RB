using MaReB.Models.VM;
using Newtonsoft.Json;
using System.IO;

namespace MaReB.Services
{
    public static class Libman
    {
        public static Libs LoadJson()
        {
            using (StreamReader r = new StreamReader("libman.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Libs>(json);
            }
        }
    }
}
