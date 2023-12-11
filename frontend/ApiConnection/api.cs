using System.Text.Json;
using frontend.Data;

namespace frontend.ApiConnection
{
    public class ApiHandle
    {
        public const string Url = "https://localhost:7280";
        public async Task<List<movie>> GetAllFood()
        {
            try
            {
                using (HttpClient client = new())
                {
                    client.Timeout = new(0,0,10);
                    var response= await client.GetAsync(Url+"/api/DB");
                    string json = await response.Content.ReadAsStringAsync();
                    movie[] foodModel = Newtonsoft.Json.JsonConvert.DeserializeObject<movie[]>(json);
                    if (foodModel == null) return new();
                    return foodModel.ToList();
                }
            }
            catch (Exception e)
            {
                return new();
            }
        }
    }
}