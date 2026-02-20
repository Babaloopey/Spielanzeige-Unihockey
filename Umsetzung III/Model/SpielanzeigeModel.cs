
namespace Umsetzung_III.Model
{
    public class SpielanzeigeModel
    {
        public string HeimTeamName { get; set; }
        public string GastTeamName { get; set; }
        public int HeimTeamScore { get; set; }
        public int GastTeamScore { get; set; }
        public int Halbzeit { get; set; }
        public bool IsEJuniorenModus { get; set; }

        public int DurationHalftime
        {
            get
            {
                return IsEJuniorenModus ? 24 : 20;
            }
        }

        public SpielanzeigeModel()
        {
            this.HeimTeamName = "Heim";
            this.GastTeamName = "Gast";
            this.HeimTeamScore = 0;
            this.GastTeamScore = 0;
            this.Halbzeit = 1;
            this.IsEJuniorenModus = false;

        }
        public void ResetModel()
        {
            this.HeimTeamName = "Heim";
            this.GastTeamName = "Gast";
            this.HeimTeamScore = 0;
            this.GastTeamScore = 0;
            this.Halbzeit = 1;
        }
    }
}
