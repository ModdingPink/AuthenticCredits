using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticCredits.Utilities
{
    internal class Credits : IDisposable
    {
        public static Dictionary<string, string[]> IDToMappers = new Dictionary<string, string[]>();

        const string mappersEndpoint = "https://modding.pink/bs_mappers.json";

        public Credits() {
            if(Config.Instance.enabled)
                Task.Run(async () => { IDToMappers = await GetMapperData(); });
        }

        async Task<string> GetDataFromEndpoint()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(mappersEndpoint);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            return json;
        }

        async Task<Dictionary<string, string[]>> GetMapperData()
        {
            string path = Path.Combine(Config.Instance.creditsPath, Config.Instance.creditsFile);
            string jsonData = string.Empty;

            try
            {
                jsonData = await GetDataFromEndpoint();

                if(!Directory.Exists(Config.Instance.creditsPath))
                    Directory.CreateDirectory(Config.Instance.creditsPath);
                
                await File.WriteAllTextAsync(path, jsonData);
            }
            catch
            {
                if (File.Exists(path))
                    jsonData = await File.ReadAllTextAsync(path);
            }

            if (string.IsNullOrWhiteSpace(jsonData))
                return new Dictionary<string, string[]>();

            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonData)
                    ?? new Dictionary<string, string[]>();
            }
            catch
            {
                return new Dictionary<string, string[]>();
            }
        }

        public void Dispose()
        {
            IDToMappers.Clear();
        }
    }
}
