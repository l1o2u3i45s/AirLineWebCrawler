using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineWebCrawler
{
   public class AirPortScore
    {
        public AirPortScore(HtmlNode row, string airportName)
        {
            AirPortName = airportName;

            Score = row.SelectSingleNode("//span[@itemprop='ratingValue']").InnerText.Trim() + "/10";
            int i = 0;
            HtmlNode rate = row.SelectSingleNode("//div[@class='ratings']//table[@class='review-ratings']");
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(rate.InnerHtml);
            foreach (HtmlNode data in htmlDocument.DocumentNode.SelectNodes("//td"))
            {
                switch (data.InnerText)
                {
                    case "Terminal Seating":
                        string[] split0 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        TerminalSeating = split0[split0.Length - 1].Substring(0, 1);
                        break;
                    case "Terminal Cleanliness":
                        string[] split1 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        TerminalCleanliness = split1[split1.Length - 1].Substring(0, 1);
                        break;
                    case "Queuing Times":
                        string[] split2 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        QueuingTimes = split2[split2.Length - 1].Substring(0, 1);
                        break; 
                }
                i++;
            }
        }
        public string AirPortName { get; set; }
        public string Score { get; set; }
        public string TerminalSeating { get; set; }
        public string TerminalCleanliness { get; set; }
        public string QueuingTimes { get; set; } 
    }
}
