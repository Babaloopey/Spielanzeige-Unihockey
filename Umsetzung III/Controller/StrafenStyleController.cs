using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umsetzung_III.Controller
{
    internal class StrafenStyleController
    {
        StrafenStore heimStrafen;

        StrafenStore gastStrafen;
        public StrafenStyleController(StrafenStore heimStrafenStore, StrafenStore gastStrafenStore)
        {
            this.heimStrafen = heimStrafenStore;
            this.gastStrafen = gastStrafenStore;
        }

        public string GetStrafenGastMargin()
        {
            if (gastStrafen.StrafenAnzeigeGroesse == StrafenStore.Schrift.gross)
            {
                return "0,-40,0,0";
            }
            else if (gastStrafen.StrafenAnzeigeGroesse == StrafenStore.Schrift.mittel)
            {
                return "0,-60,60,0";
            }
            else
            {
                return "0,-80,60,0";
            }
        }

        public string GetStrafenHeimMargin()
        {
            if (heimStrafen.StrafenAnzeigeGroesse == StrafenStore.Schrift.gross)
            {
                return "0,-40,0,0";
            }
            else if (heimStrafen.StrafenAnzeigeGroesse == StrafenStore.Schrift.mittel)
            {
                return "60,-60,0,0";
            }
            else
            {
                return "60,-80,0,0";
            }
        }
        
    }
}
