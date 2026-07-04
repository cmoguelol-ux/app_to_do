using System;
using System.Net;
using System.Text.RegularExpressions;

namespace DogService.WCF
{
    public class DogService : IDogService
    {
        public string ObtenerPerritoDelDia()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string url = "https://dog.ceo/api/breeds/image/random";

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "Mozilla/5.0");

                    string json = client.DownloadString(url);

                    Match match = Regex.Match(
                        json,
                        "\"message\"\\s*:\\s*\"([^\"]+)\"",
                        RegexOptions.IgnoreCase
                    );

                    if (!match.Success)
                    {
                        return ObtenerImagenDeRespaldo();
                    }

                    string imagenUrl = match.Groups[1].Value;
                    imagenUrl = imagenUrl.Replace("\\/", "/");

                    return imagenUrl;
                }
            }
            catch
            {
                return ObtenerImagenDeRespaldo();
            }
        }

        private string ObtenerImagenDeRespaldo()
        {
            return "https://images.dog.ceo/breeds/hound-afghan/n02088094_1003.jpg";
        }
    }
}