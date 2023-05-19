
namespace Umsetzung_III.Model
{
    public class Actions
    {
        public enum Team { Gast, Heim}
        public enum StandVeraenderung { Hoch, Runter}
        public enum Strafe { Zwei, Vier, Zehn, Delete}
        public enum ZeitAktion { Start, Stop, Reset, Space, PlusOne, MinusOne, StartPausenzeit, StartTimeOut}
    }
}
