using System.Text.Json;
using frontend.Data;

namespace frontend.ApiConnection
{
    public class api
    {
        public const string Url = "https://localhost:7280";
        public async Task<List<movie>> GetMovies()
        {
            try
            {
                using (HttpClient client = new())
                {
                    client.Timeout = new(0,0,10);
                    var response= await client.GetAsync(Url+"/api/DB");
                    string json = await response.Content.ReadAsStringAsync();
                    movie[] movies = Newtonsoft.Json.JsonConvert.DeserializeObject<movie[]>(json);
                    if (movies == null) return new();
                    return movies.ToList();
                }
            }
            catch (Exception e)
            {
                return new();
            }
        }
    }
}