using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace WebApi.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : class
    {
        protected string JsonPath = "./Json/";

        protected readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        public JsonRepository()
        {

        }


        public async Task<IEnumerable<T>> GetAll()
        {
            var jArray = await GetJsonArray();
            var result = jArray.ToObject<IEnumerable<T>>();
            return result;
        }

        public async Task<T> GetById(int id)
        {
            var jArray = await GetJsonArray();
            foreach (var jToken in jArray)
            {
                if (jToken["id"].Value<int>() == id)
                {
                    return jToken.ToObject<T>();
                }
            }
            return default;
        }



        public async Task<T> Create(T item)
        {
            var jArray = await GetJsonArray();
            string newiItem = JsonConvert.SerializeObject(item, JsonSettings);
            var newJObject = JObject.Parse(newiItem);
            newJObject["id"] = GetNewId();
            jArray.Add(newJObject);

            await Save(jArray);

            return newJObject.ToObject<T>();
        }

        public async Task Delete(int id)
        {
            var jArray = await GetJsonArray();

            string nameId = nameof(id);
            var itemToDel = jArray.FirstOrDefault(x => x[nameId].Value<int>() == id);
            jArray.Remove(itemToDel);

            await Save(jArray);
        }

        protected async Task<JArray> GetJsonArray()
        {
            string name = GetJsonModelName();
            string path = Path.Combine(JsonPath, name);
            string json = await File.ReadAllTextAsync(path);
            var jArray = JArray.Parse(json);
            return jArray;
        }

        protected async Task Save(JArray jArray)
        {
            string updatedJson = JsonConvert.SerializeObject(jArray, JsonSettings);
            string name = GetJsonModelName();
            string path = Path.Combine(JsonPath, name);
            await File.WriteAllTextAsync(path, updatedJson);
        }

        private string GetJsonModelName()
        {
            string className = typeof(T).Name.ToLower();
            string jsonModelName = $"{className}s.json";
            return jsonModelName;
        }

        private int GetNewId()
        {
            var allItems = GetJsonArray().GetAwaiter().GetResult();
            if(!allItems.Any())
            {
                return 1;
            }
            int lastElementId = allItems.OrderByDescending(x => x["id"].Value<int>())
            .FirstOrDefault()["id"].Value<int>();
            return lastElementId + 1;
        }
    }
}