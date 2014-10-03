﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SccaLiveWebService.Models;

namespace SccaLiveWebService.Controllers
{
    public class DriverDataController : ApiController
    {

        public IHttpActionResult GetDriverData()
        {
            //Create List of Drivers
            DriverData[] SccaDrivers = getDrivers();
            if(SccaDrivers != null)
            {
                return Ok(SccaDrivers);
                //return Ok("Testing");
            }
            else
            {
                return NotFound();
            }
            
        }

        public static DriverData[] getDrivers()
        {
            //Create the Web Client
            WebClient client = new WebClient();

            //Create the html document option
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;

            //Read in the HTML
            String HtmlCode = client.DownloadString("http://www.sololive.texasscca.org");

            htmlDoc.LoadHtml(HtmlCode);

            if (htmlDoc.DocumentNode != null)
            {
                //Change the URL above, comment out this line, and un-comment next line to view previous events
                return ParseHTML(htmlDoc);
                //SccaDrivers = ParseResultsHTML(htmlDoc);
            }
            else
            {
                return null;
            }

        }

        //Function to Parse the HTML for live events
        public static DriverData[] ParseHTML(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            string driverClass = "";
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
                            if (cell.FirstChild.Name.Equals("a"))
                            {
                                driverClass = cell.ChildNodes[1].InnerHtml.Split(' ')[0];
                            }
                            else
                            {
                                break;
                            }

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
            return parsedSccaRacers.ToArray();
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
