using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III.Model;
using static Umsetzung_III.Model.Actions;

namespace Umsetzung_III.Controller
{
    public class StrafenStyleController
    {

        // left, top, right, bottom
        const string MarginBigHeim = "0,0,0,0";
        const string MarginMiddleHeim = "70,-60,0,0";
        const string MarginSmallHeim = "110,-80,0,0";    
        
        const string MarginBigGast = "0,0,0,0";
        const string MarginMiddleGast = "0,-60,70,0";
        const string MarginSmallGast = "0,-80,110,0";

        public StrafenStyleController() { }

        public string GetStrafenMargin(StrafenStore strafenStore)
        {
            if(strafenStore.team == Team.Heim)
            {
                if (strafenStore.StrafenAnzeigeGroesse == Schrift.gross)
                {
                    return MarginBigHeim;
                }
                else if (strafenStore.StrafenAnzeigeGroesse == Schrift.mittel)
                {
                    return MarginMiddleHeim;
                }
                else
                {
                    return MarginSmallHeim;
                }
            } else
            {
                if (strafenStore.StrafenAnzeigeGroesse == Schrift.gross)
                {
                    return MarginBigGast;
                }
                else if (strafenStore.StrafenAnzeigeGroesse == Schrift.mittel)
                {
                    return MarginMiddleGast;
                }
                else
                {
                    return MarginSmallGast;
                }
            }
           
        }

    }
}
