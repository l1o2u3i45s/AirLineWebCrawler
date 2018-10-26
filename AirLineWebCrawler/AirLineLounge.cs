using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineWebCrawler
{
   public class AirLineLounge
    {
        public AirLineLounge(HtmlNode row, int index, string airlineName)
        {
            AirLineName = airlineName;
            string[] pointBuilder = row.InnerHtml.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            HtmlDocument pointDocument = new HtmlDocument();
            foreach (string point in pointBuilder)
            {
                if (point.Contains(@"<span itemprop=""ratingValue"">"))
                {
                    pointDocument.LoadHtml(point);
                    break;
                }
            }
            if (pointDocument.DocumentNode.InnerText == string.Empty)
                Point = "NoData";
            else
                Point = pointDocument.DocumentNode.SelectSingleNode("//span[@itemprop='ratingValue']").InnerText + "/10";



            Header = row.SelectNodes("//div[@class='body']//h2[@class='text_header']")[index].InnerText;
            AuthorName = row.SelectNodes("//div[@class='body']//h3[@class='text_sub_header userStatusWrapper']//span[@itemprop='author']//span[@itemprop='name']")[index].InnerText;
            Date = row.SelectNodes("//div[@class='body']//h3[@class='text_sub_header userStatusWrapper']//time[@itemprop='datePublished']")[index].InnerText;
            Content = row.SelectNodes("//div[@class='body']//div[@class='text_content ']")[index].InnerText;
            int i = 0;
            HtmlNode rate = row.SelectNodes("//div[@class='body']//div[@class='tc_mobile']//table[@class='review-ratings']")[index];
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(rate.InnerHtml);
            foreach (HtmlNode data in htmlDocument.DocumentNode.SelectNodes("//td"))
            {
                switch (data.InnerText)
                {
                    case "Comfort":
                        string[] split0 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        Comfort = split0[split0.Length - 1].Substring(0, 1);
                        break;
                    case "Cleanliness":
                        string[] split1 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        Cleanliness = split1[split1.Length - 1].Substring(0, 1);
                        break;
                    case "Bar &amp; Beverages":
                        string[] split2 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        BarBeverages = split2[split2.Length - 1].Substring(0, 1);
                        break;
                    case "Catering":
                        string[] split3 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        Catering = split3[split3.Length - 1].Substring(0, 1);
                        break;
                    case "Washrooms":
                        string[] split4 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        Washrooms = split4[split4.Length - 1].Substring(0, 1);
                        break;
                    case "Wifi Connectivity":
                        string[] split5 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        WifiConnectivity = split5[split5.Length - 1].Substring(0, 1);
                        break;
                    case "Staff Service":
                        string[] split6 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        StaffService = split6[split6.Length - 1].Substring(0, 1);
                        break; 
                    case "Lounge Name":
                        LoungeName = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Airport":
                        Airport = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Type Of Lounge":
                        TypeOfLounge = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Date Visit":
                        DateVisit = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Type Of Traveller":
                        TypeOfTraveller = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Recommended":
                        Recommended = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                }
                i++;
            }
        }
        public string Point { get; set; }
        public string Header { get; set; } = "NoData";
        public string AuthorName { get; set; } = "NoData";
        public string Date { get; set; } = "NoData";
        public string Content { get; set; } = "NoData";
        public string LoungeName { get; set; } = "NoData";
        public string Airport { get; set; } = "NoData";
        public string TypeOfLounge { get; set; } = "NoData";
        public string DateVisit { get; set; } = "NoData";
        public string TypeOfTraveller { get; set; } = "NoData";
        public string Comfort { get; set; } = "NoData";
        public string Cleanliness { get; set; } = "NoData";
        public string BarBeverages { get; set; } = "NoData";
        public string Catering { get; set; } = "NoData";
        public string Washrooms { get; set; } = "NoData";
        public string WifiConnectivity { get; set; } = "NoData";
        public string StaffService { get; set; } = "NoData";
        public string Recommended { get; set; } = "NoData";
        public string AirLineName { get; set; } = "NoData";
    }
}
