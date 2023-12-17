using System.Text;
using System.Text.Json;
using frontend.Data;

namespace frontend.Classes
{
    public class ApiConnection
    {
        public const string Url = "https://localhost:7280";

        public async Task<List<movie>> GetAllMovies()
        {
            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    var response = await client.GetAsync($"{Url}/api/movies");
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var movie = Newtonsoft.Json.JsonConvert.DeserializeObject<movie[]>(json);

                    return movie?.ToList() ?? new List<movie>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<movie>();
            }
        }

        public async Task<movie> GetMovieById(int id)
        {
            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    var response = await client.GetAsync($"{Url}/api/movies/{id}");
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var movie = Newtonsoft.Json.JsonConvert.DeserializeObject<movie>(json);

                    return movie;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> CreateMovie(movie model)
        {
            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{Url}/api/movies", content);
                    response.EnsureSuccessStatusCode();

                    return true;
                }
            }
            catch (Exception e)
            {   
                Console.WriteLine("failed create");
                return false;
            }
        }

        public async Task<bool> UpdateMovie(int id, movie model)
        {
            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"{Url}/api/movies/{id}", content);
                    response.EnsureSuccessStatusCode();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMovie(int id)
        {
            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    var response = await client.DeleteAsync($"{Url}/api/movies/{id}");
                    response.EnsureSuccessStatusCode();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

}