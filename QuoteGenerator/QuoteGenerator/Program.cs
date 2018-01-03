using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace QuoteGenerator
{
    class Program
    {
        private static readonly string BASE_URI = "https://pocket-wisdom-quotes.herokuapp.com/";

        static void Main(string[] args)
        {


            while (true)
            {
                Console.WriteLine("\n Type in your request, or type \"Quit\" to quit.");
                string userInput = Console.ReadLine();
                if (userInput.ToUpper() == "QUIT")
                    break;
                else 
                    ProcessQuoteRequest(userInput).Wait();
            }
        }

        private static async Task ProcessQuoteRequest(string request)
        {
            
            HttpClient client = BuildHttpClient();

            try
            {
                var stringTask = client.GetStringAsync(BASE_URI + request);

                var msg = await stringTask;
                //Console.Write(msg);
                RootObject quotes = JsonConvert.DeserializeObject<RootObject>(msg);

                PrintQuotesToConsole(quotes);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("QuoteGenerator could not connect to: " + BASE_URI + request);
                Console.WriteLine(e.Message);
            }

        }

        private static HttpClient BuildHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            return client;
        }

        private static void PrintQuotesToConsole(RootObject quotes)
        {
            List<Quote> allQuotes = quotes.QuoteSet;
            foreach (Quote quote in allQuotes)
                Console.WriteLine(quote.ToString());

        }


    }
}
