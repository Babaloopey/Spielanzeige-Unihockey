using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umsetzung_III.Model
{
    public class AngezeigteStrafe
    {
        public int minute { get; set; }
        public int second { get; set; }

        public string displayStrafe { get; set; }

        public AngezeigteStrafe(int minute, int second)
        {
            this.minute = minute;
            this.second = second;
            
            displayStrafe = minute.ToString("00") + ":" + second.ToString("00");
        }
    }
}
