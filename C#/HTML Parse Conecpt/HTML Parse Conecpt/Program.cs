using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HTML_Parse_Conecpt.DriverDataClass;

namespace HTML_Parse_Conecpt
{
    class Program
    {
        static void Main(string[] args)
        {
            #region properties
            //Create the Web Client
            WebClient client = new WebClient();

            //Create the html document option
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;

            //Create List of Drivers
            List<DriverData> SccaDrivers = new List<DriverData>();
            #endregion

            //Read in the HTML
            String HtmlCode = client.DownloadString("http://www.sololive.texasscca.org");
            
            /*past events 
             * http://texasscca.org/2014_solo_results/tr14_1_final.htm
             * http://texasscca.org/2014_solo_results/tr14_2_final.htm
             * http://texasscca.org/2014_solo_results/tr14_3_final.htm
             * http://texasscca.org/2014_solo_results/tr14_4_final.htm
             * http://texasscca.org/2014_solo_results/tr14_5_final.htm
             * http://texasscca.org/2014_solo_results/tr14_6_final.htm
             * http://texasscca.org/2014_solo_results/tr14_7_final.htm    
             * -----------------------------basically only ^ changes--
             */

            //Live Event: http://www.sololive.texasscca.org

            htmlDoc.LoadHtml(HtmlCode);

            if (htmlDoc.DocumentNode != null)
            {
                //Change the URL above, comment out this line, and un-comment next line to view previous events
                SccaDrivers = ParseHTML(htmlDoc);                
                //SccaDrivers = ParseResultsHTML(htmlDoc);
            }
            //(Proof of concept) output here: would be return of webservice
            foreach (DriverData car in SccaDrivers)
            {
         
                Console.WriteLine("#" + car.Number + " " + car.Name + " : " + car.Total);
                Console.WriteLine("Run 1: " + car.Time1 + " Run 2: " + car.Time2 + " Run 3: " + car.Time3 + " Run 4: " + car.Time4 + " Run 5: " + car.Time5);
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("To String: " + car.ToString());
                Console.WriteLine("---------------------------------------------------");
            }

        }
        //Function to Parse the HTML for live events
        public static List<DriverData> ParseHTML(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            List<DriverData> parsedSccaRacers = new List<DriverData>();
            HtmlAgilityPack.HtmlNode node = htmlDoc.DocumentNode.SelectNodes("//tbody")[1];
            foreach (HtmlAgilityPack.HtmlNode row in node.ChildNodes)
            {
                if (row.Name.Equals("tr"))
                {
                    //row found - begin to parse
                    List<string> driverData = new List<string>();
                    foreach (HtmlAgilityPack.HtmlNode cell in row.ChildNodes)
                    {
                        if (cell.Name.Equals("th"))
                        {
                            //table header here
                            break;
                        }
                        else if (cell.Name.Equals("td"))
                        {
                            driverData.Add(cell.InnerHtml);
                        }
                    }

                    //Create Driver object and add to list
                    if (driverData.Count != 17)
                    {
                        //error, somehow the html was read in wrong
                    }
                    else
                    {                        
                        DriverData driverObject = new DriverData(driverData[0], driverData[1], driverData[2], driverData[3], driverData[4], driverData[5], driverData[6], driverData[7], driverData[8], driverData[9], driverData[10], driverData[11], driverData[12], driverData[15], driverData[16]);
                        parsedSccaRacers.Add(driverObject);

                    }
                }
            }
            return parsedSccaRacers;
        }
        //End Function

        //Parser for previously posted results
        public static List<DriverData> ParseResultsHTML(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            List<DriverData> parsedSccaRacers = new List<DriverData>();
            HtmlAgilityPack.HtmlNode node = htmlDoc.DocumentNode.SelectNodes("//tbody")[1];
            foreach (HtmlAgilityPack.HtmlNode row in node.ChildNodes)
            {
                if (row.Name.Equals("tr"))
                {
                    //row found - begin to parse
                    List<string> driverData = new List<string>();
                    foreach (HtmlAgilityPack.HtmlNode cell in row.ChildNodes)
                    {
                        if (cell.Name.Equals("th"))
                        {
                            //table header here
                            break;
                        }
                        else if (cell.Name.Equals("td"))
                        {
                            driverData.Add(cell.InnerHtml);
                        }
                    }

                    //Create Driver object and add to list
                    //Only 12 items, but wasn't sure if it would change
                    if (driverData.Count < 1)
                    {
                        //error, somehow the html was read in wrong
                    }
                    else
                    {
                        DriverData driverObject = new DriverData(driverData[0], driverData[1], driverData[2], driverData[3], driverData[4], driverData[5], driverData[6], driverData[7], driverData[8], driverData[9], driverData[10], driverData[11]);
                        parsedSccaRacers.Add(driverObject);

                    }
                }
            }
            return parsedSccaRacers;
        }
        //End 2nd function
    }
}
