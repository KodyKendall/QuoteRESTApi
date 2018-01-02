using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QuoteGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n Type in your request, or type \"Quit\" to quit.");
                string userInput = Console.ReadLine();
                if (userInput == "Quit")
                    break;
                else 
                    ProcessQuoteRequest(userInput).Wait();
            }
        }

        private static async Task ProcessQuoteRequest(string request)
        {
            string baseUri = "https://pocket-wisdom-quotes.herokuapp.com/";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            try
            {
                var stringTask = client.GetStringAsync(baseUri + request);
                var msg = await stringTask;
                Console.Write(msg);
            }
            catch (Exception)
            {
                Console.WriteLine("QuoteGenerator could not connect to: " + baseUri + request);
            }

        }
    }
}
