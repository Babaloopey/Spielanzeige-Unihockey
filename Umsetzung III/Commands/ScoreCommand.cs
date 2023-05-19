
using static Umsetzung_III.Model.Actions;

namespace Umsetzung_III
{

    // Command, der den Punktestand der Teams je nach Paramter des Buttons veraendert
    public class ScoreCommand : CommandBase
    {
        private readonly SpielanzeigeViewModel _spielanzeige;
        private readonly Team _team;
        private readonly StandVeraenderung _standVeraenderung;

        public ScoreCommand(SpielanzeigeViewModel spielanzeige, Team team, StandVeraenderung standVeraenderung)
        {
            _spielanzeige = spielanzeige;
            _team = team;
            _standVeraenderung = standVeraenderung;

        }
        public override void Execute(object? parameter)
        {
            switch (this._team)
            {
                case Team.Gast:
                    switch (this._standVeraenderung)
                    {
                        case StandVeraenderung.Hoch:
                            _spielanzeige.GastTeamScore++;
                            break;
                        case StandVeraenderung.Runter:
                            if(_spielanzeige.GastTeamScore > 0)
                            {
                                _spielanzeige.GastTeamScore--;
                            }
                            break;
                    }
                    _spielanzeige.OnPropertyChanged("GastTeamScore");
                    break;

                case Team.Heim:
                    switch (this._standVeraenderung)
                    {
                        case StandVeraenderung.Hoch:
                            _spielanzeige.HeimTeamScore++;
                            break;
                        case StandVeraenderung.Runter:
                            if(_spielanzeige.HeimTeamScore > 0)
                            {
                                _spielanzeige.HeimTeamScore--;
                            }
                            break;
                    }
                    _spielanzeige.OnPropertyChanged("HeimTeamScore");
                    break;
            }
        }
    }
}
