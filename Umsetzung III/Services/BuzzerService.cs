using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umsetzung_III.Services
{
    public class BuzzerService
    {
        private int duration = 1000; // in ms
        private int hertz = 350;
        public void Buzz()
        {
            Console.Beep(hertz, duration);
        }
    }
}
