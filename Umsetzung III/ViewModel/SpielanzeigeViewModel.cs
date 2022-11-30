using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Umsetzung_III.Controller;
using Umsetzung_III.Model;
using Umsetzung_III.Stores;
using static Umsetzung_III.Actions;

namespace Umsetzung_III
{
    public class SpielanzeigeViewModel : ViewModelBase
    {
        // MemberVariablen
        private readonly SpielanzeigeModel _spielanzeige;
        private readonly StrafenStore _strafenHeim;
        private readonly StrafenStore _strafenGast;
        private readonly LogoController _logoStore;
        private readonly EffStrafzeitController _halfTimeStore;
        private readonly SpielzeitStore _spielzeitStore;
        private readonly PauseOrTimeOutButtonController _pauseOrTimeOutButtonStore;

        // Properties, die von der View abgefragt werden, um Buttons zu verstecken/ anzuzeigen
        public bool ButtonVisibilityStart => _spielzeitStore.IsStartButtonVisible;
        public bool ButtonVisibilityStop => !_spielzeitStore.IsStartButtonVisible;
        public bool ButtonVisibilityPause => _pauseOrTimeOutButtonStore.IsPauseButtonVisible && ButtonVisibilityStart;
        public bool ButtonVisibilityTimeOut => !_pauseOrTimeOutButtonStore.IsPauseButtonVisible && ButtonVisibilityStart;

        public bool LogoVisibility => _logoStore.IsLogoVisible;
        public bool EffektiveSpielzeitVisibility => _halfTimeStore.IsEffektiveSpielzeitVisible; //public bool EffektiveSpielzeitVisibility => _spielzeitStore.EffektiveSpielzeitVisibility;

        // Properties, die von der View abgefragt werden, um Informationen darzustellen
        public string Spielzeit
        {
            get { return SpielMinute.ToString("00") + ":" + SpielSekunde.ToString("00"); }
        }
        public int SpielMinute
        { get { return _spielzeitStore.Minute; } }
        public int SpielSekunde
        { get { return _spielzeitStore.Second; } }
        public ObservableCollection<AngezeigteStrafe> HeimTeamStrafe
        {
            get
            {
                return _strafenHeim.Strafen;
            }
        }
        public ObservableCollection<AngezeigteStrafe> GastTeamStrafe
        {
            get
            {
                return _strafenGast.Strafen;

            }
        }
        public bool HeimTeamStrafeRunning
        {
            get { return _strafenHeim.IsStrafeRunning; }
        }
        public bool GastTeamStrafeRunning
        {
            get { return _strafenGast.IsStrafeRunning; }
        }
        public int HeimStrafenAnzeigeGroesse
        {
            get { return _strafenHeim.StrafenAnzeigeGroesse; }
        }
        public int GastStrafenAnzeigeGroesse
        {
            get { return _strafenGast.StrafenAnzeigeGroesse; }
        }
        public int Halbzeit
        {
            get { return _spielanzeige.Halbzeit; }
            set
            {
                if (_spielanzeige.Halbzeit != value)
                {
                    _spielanzeige.Halbzeit = value;
                    OnPropertyChanged("Halbzeit");
                }
            }
        }
        public string HeimTeamName
        {
            get { return _spielanzeige.HeimTeamName; }
            set
            {
                if (_spielanzeige.HeimTeamName != value)
                {
                    _spielanzeige.HeimTeamName = value;
                    OnPropertyChanged("HeimTeamName");
                }
            }
        }
        public string GastTeamName
        {
            get { return _spielanzeige.GastTeamName; }
            set
            {
                if (_spielanzeige.GastTeamName != value)
                {
                    _spielanzeige.GastTeamName = value;
                    OnPropertyChanged("GastTeamName");
                }
            }
        }
        public int GastTeamScore
        {
            get { return _spielanzeige.GastTeamScore; }
            set
            {
                if (_spielanzeige.GastTeamScore != value)
                {
                    _spielanzeige.GastTeamScore = value;
                    OnPropertyChanged("GastTeamScore");
                }
            }
        }
        public int HeimTeamScore
        {
            get { return _spielanzeige.HeimTeamScore; }
            set
            {
                if (_spielanzeige.HeimTeamScore != value)
                {
                    _spielanzeige.HeimTeamScore = value;
                    OnPropertyChanged("HeimTeamScore");
                }
            }
        }

        // Definition der Buttons der Views
        public ICommand HeimScoreUp { get; }
        public ICommand HeimScoreDown { get; }
        public ICommand GastScoreUp { get; }
        public ICommand GastScoreDown { get; }

        public ICommand StartTime { get; }
        public ICommand StopTime { get; }
        public ICommand ResetTime { get; }
        public ICommand SpaceButton { get; }
        public ICommand ResetAll { get; }
        public ICommand TimeMinusOne { get; }
        public ICommand TimePlusOne { get; }
        public ICommand StartPausenzeit { get; }
        public ICommand StartTimeOut { get; }

        public ICommand HeimStrafeZwei { get; }
        public ICommand HeimStrafeVier { get; }
        public ICommand HeimStrafeZehn { get; }
        public ICommand HeimStrafeDelete { get; }

        public ICommand GastStrafeZwei { get; }
        public ICommand GastStrafeVier { get; }
        public ICommand GastStrafeZehn { get; }
        public ICommand GastStrafeDelete { get; }

        // Zuruecksetzen des gesamten ViewModels auf den Anfangszustand
        public void ResetViewModel()
        {
            _spielanzeige.ResetModel();
            _spielzeitStore.Reset();
            _strafenGast.Reset();
            _strafenHeim.Reset();
            OnPropertyChanged("HeimTeamScore");
            OnPropertyChanged("GastTeamScore");
            OnPropertyChanged("HeimTeamName");
            OnPropertyChanged("GastTeamName");
            OnPropertyChanged("Halbzeit");
        }

        public SpielanzeigeViewModel()
        {
            // Initialisierung des Models und der Stores
            _spielanzeige = new SpielanzeigeModel();
            _logoStore = new LogoController(this);
            _spielzeitStore = new SpielzeitStore(this);
            _strafenGast = new StrafenStore(_spielzeitStore);
            _strafenHeim = new StrafenStore(_spielzeitStore);
            _halfTimeStore = new EffStrafzeitController(this, _spielzeitStore);
            _pauseOrTimeOutButtonStore = new PauseOrTimeOutButtonController(this, _spielzeitStore);

            // EventBinding
            _spielzeitStore.OnStartButtonVisibilityChanged += SpielzeitStore_ButtonVisibilityChanged;

            _spielzeitStore.OnSpielzeitChanged += SpielzeitStore_SpielzeitChanged;
         //   _spielzeitStore.EffektiveSpielzeitVisibilityChanged += SpielzeitStore_EffektiveSpielzeitVisibilityChanged;


            _strafenHeim.OnStrafenChanged += StrafenHeim_StrafenChanged;
            _strafenGast.OnStrafenChanged += StrafenGast_StrafenChanged;
            _strafenHeim.OnStrafenAnzeigeGroesseChanged += StrafenHeim_OnStrafenAnzeigeGroesseChanged;
            _strafenGast.OnStrafenAnzeigeGroesseChanged += StrafenGast_OnStrafenAnzeigeGroesseChanged;


            _logoStore.OnLogoVisibilityChanged += LogoStore_LogoVisibilityChanged;
            _halfTimeStore.OnEffektiveSpielzeitVisibilityChanged += HalfTimeStore_EffektiveSpielzeitVisibilityChanged;
            _pauseOrTimeOutButtonStore.OnButtonVisibilityChanged += PauseOrTimeOutButtonStore_ButtonVisibilityChanged;

            // Zuteilung fuer Buttons
            // Buttons fuer die Kontrolle des Punktestandes
            GastScoreUp = new ScoreCommand(this, Team.Gast, StandVeraenderung.Hoch);
            GastScoreDown = new ScoreCommand(this, Team.Gast, StandVeraenderung.Runter);
            HeimScoreUp = new ScoreCommand(this, Team.Heim, StandVeraenderung.Hoch);
            HeimScoreDown = new ScoreCommand(this, Team.Heim, StandVeraenderung.Runter);

            // Buttons fuer die Kontrolle der Spielzeit
            StartTime = new TimeCommand(_spielzeitStore, ZeitAktion.Start);
            StopTime = new TimeCommand(_spielzeitStore, ZeitAktion.Stop);
            ResetTime = new TimeCommand(_spielzeitStore, ZeitAktion.Reset);
            SpaceButton = new TimeCommand(_spielzeitStore, ZeitAktion.Space);
            TimeMinusOne = new TimeCommand(_spielzeitStore, ZeitAktion.MinusOne);
            TimePlusOne = new TimeCommand(_spielzeitStore, ZeitAktion.PlusOne);
            StartPausenzeit = new TimeCommand(_spielzeitStore, ZeitAktion.StartPausenzeit);
            StartTimeOut = new TimeCommand(_spielzeitStore, ZeitAktion.StartTimeOut);

            // Buttons fuer die Kontrolle der Strafen: Gast
            GastStrafeZwei = new StrafenCommand(_strafenGast, Strafe.Zwei);
            GastStrafeVier = new StrafenCommand(_strafenGast, Strafe.Vier);
            GastStrafeZehn = new StrafenCommand(_strafenGast, Strafe.Zehn);
            GastStrafeDelete = new StrafenCommand(_strafenGast, Strafe.Reset);

            // Buttons fuer die Kontrolle der Strafen: Heim
            HeimStrafeZwei = new StrafenCommand(_strafenHeim, Strafe.Zwei);
            HeimStrafeVier = new StrafenCommand(_strafenHeim, Strafe.Vier);
            HeimStrafeZehn = new StrafenCommand(_strafenHeim, Strafe.Zehn);
            HeimStrafeDelete = new StrafenCommand(_strafenHeim, Strafe.Reset);

            // Button um das ViewModel zurueckzusetzen
            ResetAll = new ResetAllCommand(this);
        }

        // Funktionen, die an die Events der Stores gebunden sind: Im Konstruktor verlinkt

        private void SpielzeitStore_ButtonVisibilityChanged()
        {
            OnPropertyChanged("ButtonVisibilityStart");
            OnPropertyChanged("ButtonVisibilityStop");
            OnPropertyChanged("ButtonVisibilityPause");
            OnPropertyChanged("ButtonVisibilityTimeOut");
        }
        private void TimerStore_TimerElapsed()
        {
            //_strafenGast.CheckIfStrafeStillActive();
            //_strafenHeim.CheckIfStrafeStillActive();
        }

        private void HalfTimeStore_EffektiveSpielzeitVisibilityChanged()
        {
            OnPropertyChanged("EffektiveSpielzeitVisibility");
        }
        private void SpielzeitStore_SpielzeitChanged()
        {
            _halfTimeStore.CheckIfEffektiveSpielzeitMustBeVisible();
            OnPropertyChanged("Spielzeit");
        }

        private void StrafenHeim_StrafenChanged()
        {
            _logoStore.CheckIfLogoMustBeVisible();
        }

        private void StrafenGast_StrafenChanged()
        {
            _logoStore.CheckIfLogoMustBeVisible();
        }

        private void StrafenGast_OnStrafenAnzeigeGroesseChanged()
        {
            OnPropertyChanged("GastStrafenAnzeigeGroesse");
        }

        private void StrafenHeim_OnStrafenAnzeigeGroesseChanged()
        {
            OnPropertyChanged("HeimStrafenAnzeigeGroesse");
        }

        private void LogoStore_LogoVisibilityChanged()
        {
            OnPropertyChanged("LogoVisibility");
        }
        private void PauseOrTimeOutButtonStore_ButtonVisibilityChanged()
        {
            OnPropertyChanged("ButtonVisibilityPause");
            OnPropertyChanged("ButtonVisibilityTimeOut");
        }
    }
}
