using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineWebCrawler
{
    public class AirLineSeat
    {
        public AirLineSeat(HtmlNode row, int index, string airlineName)
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
                    case "Seat Legroom":
                        string[] split0 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        SeatLegroom = split0[split0.Length - 1].Substring(0, 1);
                        break;
                    case "Seat Recline":
                        string[] split1 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        SeatRecline = split1[split1.Length - 1].Substring(0, 1);
                        break;
                    case "Seat Width":
                        string[] split2 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        SeatWidth = split2[split2.Length - 1].Substring(0, 1);
                        break;
                    case "Aisle Space":
                        string[] split3 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        AisleSpace = split3[split3.Length - 1].Substring(0, 1);
                        break;
                    case "Viewing Tv Screen":
                        string[] split4 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        ViewingTvScreen = split4[split4.Length - 1].Substring(0, 1);
                        break;
                    case "Seat Storage":
                        string[] split5 = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].OuterHtml.ToString().Split(new string[] { @"<span class=""star fill"">" }, StringSplitOptions.RemoveEmptyEntries);
                        SeatStorage = split5[split5.Length - 1].Substring(0, 1);
                        break;
                    case "Aircraft Type":
                        AircraftType = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Seat Layout":
                        SeatLayout = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Date Flown":
                        DateFlown = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
                        break;
                    case "Cabin Flown":
                        CabinFlown = htmlDocument.DocumentNode.SelectNodes("//td")[i + 1].InnerText;
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
        public string AircraftType { get; set; } = "NoData";
        public string SeatLayout { get; set; } = "NoData";
        public string DateFlown { get; set; } = "NoData";
        public string CabinFlown { get; set; } = "NoData";
        public string TypeOfTraveller { get; set; } = "NoData";
        public string SeatLegroom { get; set; } = "NoData";
        public string SeatRecline	 { get; set; } = "NoData";
        public string SeatWidth { get; set; } = "NoData";
        public string AisleSpace { get; set; } = "NoData";
        public string ViewingTvScreen { get; set; } = "NoData";
        public string SeatStorage { get; set; } = "NoData";
        public string Recommended { get; set; } = "NoData";
        public string AirLineName { get; set; } = "NoData";
    }
}
