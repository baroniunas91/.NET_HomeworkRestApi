using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Homework3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://www.cvbankas.lt/?miestas=Vilnius&padalinys%5B%5D=76&keyw=");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            //var responseObject = JsonConvert.DeserializeObject<Post>(responseBody);

            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(responseBody);

            var links = htmlDoc.DocumentNode.Descendants("h3")
                .Where(node => node.GetAttributeValue("class", "").Contains("list_h3")).ToList();

            var children = links.Select(l => l.InnerText);

        }
    }
}
