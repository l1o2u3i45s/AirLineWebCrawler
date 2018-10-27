using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineWebCrawler
{
    public class AirLineReview
    {
        public AirLineReview(HtmlNode row, int index, string airlineName)
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
            string[] spArray = row.SelectNodes("//div[@class='body']//h3[@class='text_sub_header userStatusWrapper']")[index].InnerText.Trim().Split(new String[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            if (spArray.Length < 1)
                Country = row.SelectNodes("//div[@class='body']//h3[@class='text_sub_header userStatusWrapper']")[index].InnerText.Trim().Split(new String[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries)[1];
            int i = 0;
            HtmlNode rate = row.SelectNodes("//div[@class='body']//div[@class='tc_mobile']//table[@class='review-ratings']")[index];
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(rate.InnerHtml);
            foreach (HtmlNode data in htmlDocument.DocumentNode.SelectNodes("//td"))
            {
                switch (data.InnerText)
                {
                    case "Seat Comfort":
                        string[] split0 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        SeatComfort = split0[split0.Length - 1].Substring(0, 1);
                        break;
                    case "Cabin Staff Service":
                        string[] split1 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        CabinStaffService = split1[split1.Length - 1].Substring(0, 1);
                        break;
                    case "Food &amp; Beverages":
                        string[] split2 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        FoodBeverages = split2[split2.Length - 1].Substring(0, 1);
                        break;
                    case "Ground Service":
                        string[] split3 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        GroundService = split3[split3.Length - 1].Substring(0, 1);
                        break;
                    case "Value For Money":
                        string[] split4 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        ValueForMoney = split4[split4.Length - 1].Substring(0, 1);
                        break;
                    case "Type Of Traveller":
                        TypeOfTraveller = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Cabin Flown":
                        CabinFlown = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Route":
                        Route = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Date Flown":
                        DateFlown = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Inflight Entertainment": 
                        string[] split8 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        InflightEntertainment = split8[split8.Length - 1].Substring(0, 1); 
                        break;
                    case "Wifi &amp; Connectivity":
                        string[] split9 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        WifiConnectivity = split9[split9.Length - 1].Substring(0, 1); 
                        break;
                    case "Recommended":
                        Recommended = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;

                         
                }
                i++;
            }
        }
        public string Point { get; set; } = "NoData";
        public string Header { get; set; } = "NoData";
        public string AuthorName { get; set; } = "NoData";
        public string Country { get; set; } = "NoData";
        public string Date { get; set; } = "NoData";
        public string Content { get; set; } = "NoData";
        public string Aircraft { get; set; } = "NoData";
        public string TypeOfTraveller { get; set; } = "NoData";
        public string CabinFlown { get; set; } = "NoData";
        public string Route { get; set; } = "NoData";
        public string DateFlown { get; set; } = "NoData";
        public string SeatComfort { get; set; } = "NoData";
        public string CabinStaffService { get; set; } = "NoData";
        public string FoodBeverages { get; set; } = "NoData";
        public string GroundService { get; set; } = "NoData";
        public string ValueForMoney { get; set; } = "NoData"; 
        public string Recommended { get; set; }
        public string InflightEntertainment { get; set; } = "NoData";
        public string WifiConnectivity { get; set; } = "NoData";
        

        public string AirLineName { get; set; }
    }
}
