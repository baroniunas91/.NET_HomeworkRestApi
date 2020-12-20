using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Gaunam user id

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseObjectsList = JsonConvert.DeserializeObject<List<User>>(responseBody);

            var user = responseObjectsList.Where(item => item.Name == "Mrs. Dennis Schulist").ToList();

            int userId = user[0].Id;


            //Gaunam visus user albumus

            var response2 = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/albums");

            response2.EnsureSuccessStatusCode();

            var responseBody2 = await response2.Content.ReadAsStringAsync();

            var responseObjectsList2 = JsonConvert.DeserializeObject<List<Album>>(responseBody2);

            var albums = responseObjectsList2.Where(item => item.UserId == userId).ToList();


            //Gaunam user visu albumu nuotraukas, vienas albumas turi daug nuotraukų

            var response3 = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/photos");

            response3.EnsureSuccessStatusCode();

            var responseBody3 = await response3.Content.ReadAsStringAsync();

            var responseObjectsList3 = JsonConvert.DeserializeObject<List<Photo>>(responseBody3);

            List<string> photos = new List<string>();

            foreach (var item in albums)
            {
                foreach (var value in responseObjectsList3)
                {
                    if (value.AlbumId == item.Id)
                    {
                        photos.Add(value.Url);
                        Console.WriteLine(value.Url);
                    }
                }
            }
        }
    }
}
