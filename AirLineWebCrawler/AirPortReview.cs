using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineWebCrawler
{
    public class AirPortReview
    {
        public AirPortReview(HtmlNode row, int index, string airPortName)
        {
            AirPortName = airPortName;
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
            string[] spArray = row.SelectNodes("//div[@class='body']//h3[@class='text_sub_header userStatusWrapper']")[index].InnerText.Trim().Split(new String[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            if (spArray.Length > 1)
                Country = row.SelectNodes("//div[@class='body']//h3[@class='text_sub_header userStatusWrapper']")[index].InnerText.Trim().Split(new String[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries)[1];
            Content = row.SelectNodes("//div[@class='body']//div[@class='text_content ']")[index].InnerText;
            int i = 0;
            HtmlNode rate = row.SelectNodes("//div[@class='body']//div[@class='tc_mobile']//table[@class='review-ratings']")[index];
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(rate.InnerHtml);
            foreach (HtmlNode data in htmlDocument.DocumentNode.SelectNodes("//td"))
            {
                switch (data.InnerText)
                {
                    case "Queuing Times":
                        string[] split0 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        QueuingTimes = split0[split0.Length - 1].Substring(0, 1);
                        break;
                    case "Terminal Cleanliness":
                        string[] split1 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        TerminalCleanliness = split1[split1.Length - 1].Substring(0, 1);
                        break;
                    case "Terminal Seating":
                        string[] split2 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        TerminalSeating = split2[split2.Length - 1].Substring(0, 1);
                        break;
                    case "Terminal Signs":
                        string[] split3 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        TerminalSigns = split3[split3.Length - 1].Substring(0, 1);
                        break;
                    case "Airport Shopping":
                        string[] split4 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        AirportShopping = split4[split4.Length - 1].Substring(0, 1);
                        break;
                    case "Wifi Connectivity":
                        string[] split5 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        WifiConnectivity = split5[split5.Length - 1].Substring(0, 1);
                        break;
                    case "Airport Staff":
                        string[] split6 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        AirportStaff = split6[split6.Length - 1].Substring(0, 1);
                        break;
                    case "Experience At Airport":
                        ExperienceAtAirport = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Date Visit":
                        DateVisit = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Type Of Traveller":
                        TypeOfTraveller = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Food Beverages":
                        string[] split7 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        FoodBeverages = split7[split7.Length - 1].Substring(0, 1);
                        break;
                    case "Recommended":
                        Recommended = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                }
                i++;
            }
        }
        public string Point { get; set; }
        public string Header { get; set; }
        public string AuthorName { get; set; }
        public string Country { get; set; } = "NoData";
        public string Date { get; set; }
        public string Content { get; set; }
        public string ExperienceAtAirport { get; set; }
        public string DateVisit { get; set; }
        public string TypeOfTraveller { get; set; }
        public string QueuingTimes { get; set; } = "NoData";
        public string TerminalCleanliness { get; set; } = "NoData";
        public string TerminalSeating { get; set; } = "NoData";
        public string TerminalSigns { get; set; } = "NoData";
        public string FoodBeverages { get; set; } = "NoData";
        public string AirportShopping { get; set; } = "NoData";
        public string WifiConnectivity { get; set; } = "NoData";
        public string AirportStaff { get; set; } = "NoData";
        public string Recommended { get; set; }
        public string AirPortName { get; set; }
    }
}
