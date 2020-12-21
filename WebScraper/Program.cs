using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://www.cvonline.lt/darbo-skelbimai/informacines-technologijos");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(responseBody);

            var jobs = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("offer_primary_info")).ToList();

            var children = jobs.Select(l => l.FirstChild.FirstChild.InnerHtml);

            foreach (var item in children)
            {
                Console.WriteLine(item);
            }
        }
    }
}
