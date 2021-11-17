using System;
using System.IO;
using System.Threading.Tasks;
using WebApi.Clients;
using WebApi.Responses;
using System.Collections.Generic;

namespace WebApi.Services
{
    public class JsonObjectsInitializer : IJsonObjectsInitializer
    {
        private IApiClient _client;
        private string _path = "./Json/";
        private IEnumerable<string> jsonObjectNames = new List<string>()
        {
            "posts","comments", "albums", "photos", "todos", "users"
        };
        public JsonObjectsInitializer(IApiClient client)
        {
            _client = client;
        }
        public async Task Reload()
        {
            IList<Task<ResponseModel>> tasks = new List<Task<ResponseModel>>();
            foreach (var name in jsonObjectNames)
            {
                tasks.Add(_client.GetAll(name));
            }

            var result = await Task.WhenAll(tasks);

            foreach (var item in result)
            {
                await SaveToFile(item.Key, item.JsonValue);
                System.Console.WriteLine($"{item.Key} created: " + DateTime.Now);
            }
        }
        private async Task SaveToFile(string name, string jsonObject)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string path = _path + $"{name}.json";
                using (TextWriter tw = new StreamWriter(path))
                {
                    await tw.WriteAsync(jsonObject);
                };
            }

        }
    }
}