using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FilmLibraryProject
{
    class OmdbProcessor
    {
        public static async Task<OmdbModel> LoadMovie(string movieTitle)
        {
            string url = $"http://www.omdbapi.com/?apikey=fb244ae6&t={ movieTitle }";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    OmdbModel movie = await response.Content.ReadAsAsync<OmdbModel>();

                    return movie;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
