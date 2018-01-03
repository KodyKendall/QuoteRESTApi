using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QuoteGenerator
{
    public class Quote
    {
        [JsonProperty]
        public string Author { get; set; }

        [JsonProperty("Quote")]
        public string QuoteText { get; set; }

        [JsonProperty]
        public int QuoteId { get; set; }

        [JsonProperty]
        public string TitleOfWork { get; set; }

        public override string ToString()
        {
            return this.QuoteText + " -- " + this.Author;
        }
    }

    public class RootObject
    {
        [JsonProperty("quote")]
        public List<Quote> QuoteSet { get; set; }
    }
}
