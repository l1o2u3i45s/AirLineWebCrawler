using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace AirLineWebCrawler
{
    class Program
    {
      
       
        public static List<AirPort> AirPortList = new List<AirPort>();
        public static List<AirPortReview> AirPortReview = new List<AirPortReview>();
        public static List<AirPortScore> AirPortScore = new List<AirPortScore>();

        public static List<AirLine> AirLineList = new List<AirLine>();
        public static List<AirLineReview> AirLineReview = new List<AirLineReview>();
        public static List<AirLineSeat> AirLineSeat = new List<AirLineSeat>();
        public static List<AirLineLounge> AirLineLounge = new List<AirLineLounge>();
        public static List<AirLineScore> AirLineScore = new List<AirLineScore>();
        static void Main(string[] args)
        {
           GetAirPortList();
           foreach (AirPort airPort in AirPortList)
           {
               GetEachAirPortScore(airPort.Name, airPort.Url);
               GetEachAirPortReview(airPort.Name, airPort.Url);
           }
           ExportAirPortReviewToCsv();
           ExportAirPortScoreToCsv();

            GetAirLineList();
           foreach (AirLine airLine in AirLineList)
           {
               GetEachAirLineScore(airLine.Name, airLine.Url);
              GetEachAirLineReview(airLine.Name, airLine.Url);
               GetEachAirLineSeat(airLine.Name, airLine.Url);
              GetEachAirLineLounge(airLine.Name, airLine.Url);
           }
            ExportAirLineReviewToCsv();
            ExportAirLineSeatToCsv();
            ExportAirLineLoungeToCsv();
            ExportAirLineScoreToCsv();
        }

        public static void GetAirLineList()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.airlinequality.com/review-pages/a-z-airline-reviews/");
            Console.WriteLine("Get All AirLine List");
            int i = 0;
            foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//div[@class='a_z_col_group']//ul[@class='items']//li"))
            {
                AirLine airLine = new AirLine(row.InnerHtml);
                AirLineList.Add(airLine);
                Console.WriteLine(airLine.Name + "   Get Url:" + airLine.Url);
                i++;
            }
        }
        public static void GetAirPortList()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.airlinequality.com/review-pages/a-z-airport-reviews/");
            Console.WriteLine("Get All AirPort List");
            int i = 0;
            foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//div[@class='a_z_col_group']//ul[@class='items']//li"))
            {
                AirPort airPort = new AirPort(row.InnerHtml);
                AirPortList.Add(airPort);
                Console.WriteLine(airPort.Name + "   Get Url:" + airPort.Url);
                i++;
            }
        }

        public static void GetEachAirPortReview(string airPortName,string airPortUrl) {
           
            HtmlWeb web = new HtmlWeb();
            int index = 0;
            string url = "https://www.airlinequality.com/airport-reviews/" + airPortUrl.Trim().Replace(' ','-') + "/?sortby=post_date%3ADesc&pagesize=10000";
            HtmlDocument doc = web.Load(url); 
            Console.WriteLine("Visit Url:" + url);
            foreach (HtmlNode data in doc.DocumentNode.SelectNodes("//article[@class='comp comp_reviews-airline querylist position-content  ']//article[@itemprop='review']"))
            {
                AirPortReview review = new AirPortReview(data,index, airPortName.Trim());
                AirPortReview.Add(review); 
               index++;
            }
            Console.WriteLine("Get " + airPortName + " Data"); 
        }
        public static void GetEachAirLineReview(string airLineName, string airLineUrl)
        {
            HtmlWeb web = new HtmlWeb();
            int index = 0;
            string url = "https://www.airlinequality.com/airline-reviews/" + airLineUrl.Trim().Replace(' ', '-') + "/?sortby=post_date%3ADesc&pagesize=100000";
            HtmlDocument doc = web.Load(url);
            Console.WriteLine("Visit Url:" + url);
            foreach (HtmlNode data in doc.DocumentNode.SelectNodes("//article[@class='comp comp_reviews-airline querylist position-content  ']//article[@itemprop='review']"))
            {
                AirLineReview review = new AirLineReview(data, index, airLineName.Trim());
                AirLineReview.Add(review);
                index++;
            }
            Console.WriteLine("Get " + airLineName + " Data");
        }
        public static void GetEachAirLineSeat(string airLineName, string airLineUrl)
        {
            HtmlWeb web = new HtmlWeb();
            int index = 0;
            string url = "https://www.airlinequality.com/seat-reviews/" + airLineUrl.Trim().Replace(' ', '-') + "/?sortby=post_date%3ADesc&pagesize=10000";
            HtmlDocument doc = web.Load(url);
            Console.WriteLine("Visit Url:" + url);
            if (doc.DocumentNode.SelectNodes("//article[@class='comp comp_reviews-airline querylist position-content  ']//article[@itemprop='review']") is null)
                return;
            foreach (HtmlNode data in doc.DocumentNode.SelectNodes("//article[@class='comp comp_reviews-airline querylist position-content  ']//article[@itemprop='review']"))
            {
                AirLineSeat review = new AirLineSeat(data, index, airLineName.Trim());
                AirLineSeat.Add(review);
                index++;
            }
            Console.WriteLine("Get " + airLineName + " Data");
        }
        public static void GetEachAirLineLounge(string airLineName, string airLineUrl)
        {
            HtmlWeb web = new HtmlWeb();
            int index = 0;
            string url = "https://www.airlinequality.com/lounge-reviews/" + airLineUrl.Trim().Replace(' ', '-') + "/?sortby=post_date%3ADesc&pagesize=10000";
            HtmlDocument doc = web.Load(url);
            Console.WriteLine("Visit Url:" + url);
            if (doc.DocumentNode.SelectNodes("//article[@class='comp comp_reviews-airline querylist position-content  ']//article[@itemprop='review']") is null)
                return;
            foreach (HtmlNode data in doc.DocumentNode.SelectNodes("//article[@class='comp comp_reviews-airline querylist position-content  ']//article[@itemprop='review']"))
            {
                AirLineLounge review = new AirLineLounge(data, index, airLineName.Trim());
                AirLineLounge.Add(review);
                index++;
            }
            Console.WriteLine("Get " + airLineName + " Data");
        }
        public static void GetEachAirLineScore(string airLineName, string airLineUrl)
        {
            HtmlWeb web = new HtmlWeb();
            string url = "https://www.airlinequality.com/airline-reviews/" + airLineUrl.Trim().Replace(' ', '-') + "/?sortby=post_date%3ADesc&pagesize=10000";
            HtmlDocument doc = web.Load(url);
            Console.WriteLine("Visit Url:" + url);
            if (doc.DocumentNode.SelectNodes("//div[@class='review-info']") is null)
                return;
            AirLineScore review = new AirLineScore(doc.DocumentNode.SelectSingleNode("//div[@class='customer-rating']"), airLineName.Trim());
            AirLineScore.Add(review);
            Console.WriteLine("Get " + airLineName + " Data");
        }
        public static void GetEachAirPortScore(string airPortName, string airPortUrl)
        {
            HtmlWeb web = new HtmlWeb();
            string url = "https://www.airlinequality.com/airport-reviews/" + airPortUrl.Trim().Replace(' ', '-') + "/?sortby=post_date%3ADesc&pagesize=10000";
            HtmlDocument doc = web.Load(url);
            Console.WriteLine("Visit Url:" + url);
            if (doc.DocumentNode.SelectNodes("//div[@class='review-info']") is null)
                return;
            AirPortScore review = new AirPortScore(doc.DocumentNode.SelectSingleNode("//div[@class='customer-rating']"), airPortName.Trim());
            AirPortScore.Add(review);
            Console.WriteLine("Get " + airPortName + " Data");
        }
        public static void ExportAirPortReviewToCsv()
        { 
             FileStream fs = new FileStream(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "AirPortReview.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(@"Point" + "," + "AirPortName" + "," + "Header" + "," + "AuthorName" + "," + "Country" +  "," + "Date" + "," + "Content" + "," + "ExperienceAtAirport" + "," + "DateVisit" + "," + "TypeOfTraveller" + "," +
                          "QueuingTimes" + "," + "TerminalCleanliness" + "," + "TerminalSeating" + "," + "TerminalSigns" + "," + "FoodBeverages" + "," + "AirportShopping" + ","
                          + "WifiConnectivity" + "," + "AirportStaff" + "," + "Recommended");
            foreach (AirPortReview review in AirPortReview) {
                sw.WriteLine(review.Point + "," + review.AirPortName + "," + review.Header + "," + review.AuthorName + "," + review.Country + "," + review.Date + "," + review.Content.Replace(',', ' ')  + "," + review.ExperienceAtAirport + "," +
                    review.DateVisit + "," + review.TypeOfTraveller + "," + review.QueuingTimes + "," + review.TerminalCleanliness + "," + review.TerminalSeating + "," + review.TerminalSigns + "," +
                    review.FoodBeverages + "," + review.AirportShopping + "," + review.WifiConnectivity + "," + review.AirportStaff + "," + review.Recommended);  
            } 
            sw.Flush(); 
            sw.Close();
            fs.Close(); 
        }
        public static void ExportAirLineReviewToCsv()
        {
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "AirLineReview.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(@"Point" + "," + "AirLineName" + "," + "Header" + "," + "AuthorName" + "," + "Country" + "," + "Date" + "," + "Content" + "," + "Aircraft" + "," + "TypeOfTraveller" + "," + "CabinFlown" + "," +
                          "Route" + "," + "DateFlown" + "," + "SeatComfort" + "," + "CabinStaffService" + "," + "FoodBeverages" + "," + "GroundService" + ","
                          + "ValueForMoney" + "," + "InflightEntertainment" + "," + "WifiConnectivity" + "," + "Recommended");
            foreach (AirLineReview review in AirLineReview)
            {
                sw.WriteLine(review.Point + "," + review.AirLineName + "," + review.Header + "," + review.AuthorName + "," + review.Country + "," + review.Date + "," + review.Content.Replace(',', ' ') + "," + review.Aircraft + "," +
                    review.TypeOfTraveller + "," + review.CabinFlown + "," + review.Route + "," + review.DateFlown + "," + review.SeatComfort + "," + review.CabinStaffService + "," +
                    review.FoodBeverages + "," + review.GroundService + "," + review.ValueForMoney + "," + review.InflightEntertainment + "," + review.WifiConnectivity + "," + review.Recommended);
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        public static void ExportAirLineSeatToCsv()
        {
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "AirLineSeat.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(@"Point" + "," + "AirLineName" + "," + "Header" + "," + "AuthorName" + "," + "Country" + "," + "Date" + "," + "Content" + "," + "AircraftType" + "," + "SeatLayout" + "," + "DateFlown" + "," +
                          "CabinFlown" + "," + "TypeOfTraveller" + "," + "SeatLegroom" + "," + "SeatRecline" + "," + "SeatWidth" + "," + "AisleSpace" + ","
                          + "ViewingTvScreen" + "," + "SeatStorage" +  "," + "Recommended");
            foreach (AirLineSeat review in AirLineSeat)
            {
                sw.WriteLine(review.Point + "," + review.AirLineName + "," + review.Header + "," + review.AuthorName + "," + review.Country + "," + review.Date + "," + review.Content.Replace(',', ' ') + "," + review.AircraftType + "," +
                    review.SeatLayout + "," + review.DateFlown + "," + review.CabinFlown + "," + review.TypeOfTraveller + "," + review.SeatLegroom + "," + review.SeatRecline + "," +
                    review.SeatWidth + "," + review.AisleSpace + "," + review.ViewingTvScreen + "," + review.SeatStorage +  "," + review.Recommended);
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        public static void ExportAirLineLoungeToCsv()
        {
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "AirLineLounge.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(@"Point" + "," + "AirLineName" + "," + "Header" + "," + "AuthorName" + "," + "Country" + "," + "Date" + "," + "Content" + "," + "LoungeName" + "," + "Airport" + "," + "TypeOfLounge" + "," +
                          "DateVisit" + "," + "TypeOfTraveller" + "," + "Comfort" + "," + "Cleanliness" + "," + "BarBeverages" + "," + "Catering" + ","
                          + "Washrooms" + "," + "WifiConnectivity" + "," + "StaffService" + "," + "Recommended");
            foreach (AirLineLounge review in AirLineLounge)
            {
                sw.WriteLine(review.Point + "," + review.AirLineName + "," + review.Header + "," + review.AuthorName + "," + review.Country + "," + review.Date + "," + review.Content.Replace(',', ' ') + "," + review.LoungeName + "," +
                    review.Airport + "," + review.TypeOfLounge + "," + review.DateVisit + "," + review.TypeOfTraveller + "," + review.Comfort + "," + review.Cleanliness + "," +
                    review.BarBeverages + "," + review.Catering + "," + review.Washrooms + "," + review.WifiConnectivity + "," + review.StaffService + "," + review.Recommended);
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        public static void ExportAirLineScoreToCsv()
        {
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "AirLineScore.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(@"Score" + "," + "AirLineName" + "," + "FoodBeverages" + "," + "InflightEntertainment" + "," + "SeatComfort" + "," + "StaffService" + "," + "ValueforMoney");
            foreach (AirLineScore review in AirLineScore)
            {
                sw.WriteLine(review.Score + "," + review.AirLineName + "," + review.FoodBeverages + "," 
                    + review.InflightEntertainment + "," + review.SeatComfort +  "," + review.StaffService + "," + review.ValueforMoney );
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        public static void ExportAirPortScoreToCsv()
        {
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "AirPortScore.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(@"Score" + "," + "AirPortName" + "," + "TerminalSeating" + "," + "TerminalCleanliness" + "," + "QueuingTimes");
            foreach (AirPortScore review in AirPortScore)
            {
                sw.WriteLine(review.Score + "," + review.AirPortName + "," + review.TerminalSeating + "," + review.TerminalCleanliness + "," + review.QueuingTimes );
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
