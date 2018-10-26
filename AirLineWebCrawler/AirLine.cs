using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineWebCrawler
{
    public class AirLine
    {
        public AirLine(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            Name = doc.DocumentNode.SelectSingleNode("//a").InnerText;
            Url = doc.DocumentNode.SelectSingleNode("//a/@href").Attributes[0].Value.Replace("/airline-reviews/", "");
        }
        public string Name { get; set; }
        public string Url { get; set; }

    }
}
