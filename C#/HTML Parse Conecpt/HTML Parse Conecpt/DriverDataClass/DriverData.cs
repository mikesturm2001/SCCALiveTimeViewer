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
            Time1 = time1;
            Time2 = time2;
            Time3 = time3;
            Time4 = time4;
            Time5 = time5;
            Time6 = time6;
            Total = total;
            Diff = diff;

        }
    }
}
