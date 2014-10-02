﻿using System;
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
            htmlDoc.LoadHtml(HtmlCode);

            if (htmlDoc.DocumentNode != null)
            {
                SccaDrivers = ParseHTML(htmlDoc);
            }
            //(Proof of concept) output here: would be return of webservice
            foreach (DriverData car in SccaDrivers)
            {
                Console.WriteLine(car.Name + " : " + car.Number);
            }

        }
        //Function to Parse the HTML
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
    }
}
