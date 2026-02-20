using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Umsetzung_III.Services
{
    public class BuzzerService
    {
        public void Buzz()
        {
            Console.Beep(350, 2500);
        }

        public virtual void Peep()
        {
            Console.Beep(1500, 2000);
        }
    }
}
