using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_Parse_Conecpt.DriverDataClass
{
    public class DriverData
    {
        public string Place { get; set; }
        public string CarClass { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Car { get; set; }
        public string PaxPos { get; set; }
        public string PaxTime { get; set; }
        public string Time1 { get; set; }
        public string Time2 { get; set; }
        public string Time3 { get; set; }
        public string Time4 { get; set; }
        public string Time5 { get; set; }
        public string Time6 { get; set; }
        public string Total { get; set; }
        public string Diff { get; set; }

        public DriverData(string place, string carclass, string number, string name, string car, string paxpos, string paxtime, string time1, string time2, string time3, string time4, string time5, string time6, string total, string diff)
        {
            Place = place;
            CarClass = carclass;
            Number = number;
            Name = name;
            Car = car;
            PaxPos = paxpos;
            PaxTime = paxtime;                                           
            Time1 = cleanUpTimes(time1);
            Time2 = cleanUpTimes(time2);
            Time3 = cleanUpTimes(time3);
            Time4 = cleanUpTimes(time4);
            Time5 = cleanUpTimes(time5);
            Time6 = cleanUpTimes(time6);
            Total = cleanUpTimes(total);                 
            Diff = diff;
        }

        public string cleanUpTimes(string time)
        {
            if (time != "")
            {
                //If the HTML isn't trying to bold the cell, the starting index is 1
                int start = 1;
                //checks to see if the HTML is FUBAR
                if(!(time.Substring(0,1).Equals(" ")))
                {
                    start = time.IndexOf(">") + 2;
                }               
                //finds the tag at the end of the time to set the ending index
                int end = time.IndexOf("<", start);                
                //there was one case where there was no tag at the end where the system messed up
                if (end < 0)
                    return time;
                //takes the substring of the given indexes to return the time
                string result = time.Substring(start, end - start);
                return result;
            }
            else
                return time;
        }

        public string ToString()
        {
            return Place + " " + CarClass + " " + Number + " " + Name + " " + Car + " " + PaxPos + " " + PaxTime + " " + Time1 + " " + Time2 + " " + Time3 + " " + Time4 + " " + Time5 + " " + Time6 + " " + Total + " " + Diff;
        }
    }
}
