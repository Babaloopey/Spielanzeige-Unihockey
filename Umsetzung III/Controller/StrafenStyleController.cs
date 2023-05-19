using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III.Model;

namespace Umsetzung_III.Controller
{
    public class StrafenStyleController
    {
        const string MarginGross = "0,-40,0,0";
        const string MarginMiddle = "0,-60,60,0";
        const string MarginSmall = "0,-80,60,0";

        public StrafenStyleController() { }

        public string GetStrafenMargin(StrafenStore strafenStore)
        {
            if (strafenStore.StrafenAnzeigeGroesse == Schrift.gross)
            {
                return MarginGross;
            }
            else if (strafenStore.StrafenAnzeigeGroesse == Schrift.mittel)
            {
                return MarginMiddle;
            }
            else
            {
                return MarginSmall;
            }
        }

    }
}
