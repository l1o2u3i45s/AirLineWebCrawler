using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineWebCrawler
{
    public class AirLineScore
    {
        public AirLineScore(HtmlNode row , string airlineName)
        {
            AirLineName = airlineName;

            Score = row.SelectSingleNode("//span[@itemprop='ratingValue']").InnerText.Trim() + "/10"; 
            int i = 0;
            HtmlNode rate = row.SelectSingleNode("//div[@class='ratings']//table[@class='review-ratings']");
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(rate.InnerHtml); 
            foreach (HtmlNode data in htmlDocument.DocumentNode.SelectNodes("//td"))
            {
                switch (data.InnerText)
                {
                    case "Food &amp; Beverages":
                        string[] split0 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        FoodBeverages = split0[split0.Length - 1].Substring(0, 1);
                        break;
                    case "Inflight Entertainment":
                        string[] split1 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        InflightEntertainment = split1[split1.Length - 1].Substring(0, 1);
                        break;
                    case "Seat Comfort":
                        string[] split2 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        SeatComfort = split2[split2.Length - 1].Substring(0, 1);
                        break;
                    case "Staff Service":
                        string[] split3 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        StaffService = split3[split3.Length - 1].Substring(0, 1);
                        break;
                    case "Value for Money":
                        string[] split4 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        ValueforMoney = split4[split4.Length - 1].Substring(0, 1);
                        break;
                }
                i++;
            }
        }
        public string AirLineName { get; set; }
        public string Score { get; set; }
        public string FoodBeverages	{get;set;}
        public string InflightEntertainment { get; set; }
        public string SeatComfort { get; set; }
        public string StaffService { get; set; }
        public string ValueforMoney { get; set; }
        
    }
}
