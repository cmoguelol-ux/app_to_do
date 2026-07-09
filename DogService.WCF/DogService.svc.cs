using System;
using System.Net.Http;
using System.Text.Json;

namespace DogService.WCF
{
    public class DogService : IDogService
    {
        public string ObtenerPerritoDelDia()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "https://dog.ceo/api/breeds/image/random";
                    string json = client.GetStringAsync(url).Result;

                    var response = JsonSerializer.Deserialize<DogApiResponse>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                    if (response == null || response.status != "success")
                        return string.Empty;

                    return response.message;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}

