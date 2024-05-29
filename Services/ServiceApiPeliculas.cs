using LambdaAlexaPelis.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LambdaAlexaPelis.Services
{
    public class ServiceApiPeliculas
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiPeliculas()
        {
            this.UrlApi = "https://zs4wew9ko4.execute-api.us-east-1.amazonaws.com/Prod/";
            this.header = new MediaTypeWithQualityHeaderValue
                ("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(this.UrlApi + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            string request = "api/peliculas";
            List<Pelicula> data =
                await this.CallApiAsync<List<Pelicula>>(request);
            return data;
        }

        public async Task<List<Pelicula>> GetPeliculasActorAsync(string actor)
        {
            string request = "api/peliculas/FindActor/"+actor;
            List<Pelicula> data =
                await this.CallApiAsync<List<Pelicula>>(request);
            return data;
        }        
        public async Task<Pelicula> FindPeliculaAsync(int id)
        {
            string request = "api/peliculas/"+id;
            Pelicula data =
                await this.CallApiAsync<Pelicula>(request);
            return data;
        }

        public async Task CreatePeliculaAsync(Pelicula pelicula)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/peliculas";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                string json = JsonConvert.SerializeObject(pelicula);
                StringContent content =
                    new StringContent(json, this.header);
                HttpResponseMessage response = 
                    await client.PostAsync(this.UrlApi + request, content);
            }
        }        
        public async Task UpdatePeliculaAsync(Pelicula pelicula)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/peliculas";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                string json = JsonConvert.SerializeObject(pelicula);
                StringContent content =
                    new StringContent(json, this.header);
                HttpResponseMessage response = 
                    await client.PutAsync(this.UrlApi + request, content);
            }
        }        
        public async Task DeletePeliculaAsync(int idPelicula)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/peliculas/"+idPelicula;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = 
                    await client.DeleteAsync(this.UrlApi + request);
            }
        }


    }
}
