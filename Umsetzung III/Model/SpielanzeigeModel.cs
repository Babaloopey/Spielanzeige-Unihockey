﻿
namespace Umsetzung_III.Model
{
    public class SpielanzeigeModel
    {
        public string HeimTeamName { get; set; }
        public string GastTeamName { get; set; }
        public int HeimTeamScore { get; set; }
        public int GastTeamScore { get; set; }
        public int Halbzeit { get; set; }

        public SpielanzeigeModel()
        {
            this.HeimTeamName = "Heim";
            this.GastTeamName = "Gast";
            this.HeimTeamScore = 0;
            this.GastTeamScore = 0;
            this.Halbzeit = 1;

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
