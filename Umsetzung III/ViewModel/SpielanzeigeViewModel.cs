﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;
using Umsetzung_III.Model;
using Umsetzung_III.Stores;
using static Umsetzung_III.Actions;

namespace Umsetzung_III
{
    public class SpielanzeigeViewModel : ViewModelBase                         
    {
        // MemberVariablen
        private readonly SpielanzeigeModel _spielanzeige;
        private readonly TimerStore _timerStore;
        private readonly StrafenStore _strafenHeim;
        private readonly StrafenStore _strafenGast;
        private readonly LogoStore _logoStore;
        private readonly SpielzeitStore _spielzeitStore;

        // Properties, die von der View abgefragt werden, um Buttons zu verstecken/ anzuzeigen
        public bool ButtonVisibilityStart => _timerStore.IsStartButtonVisible;
        public bool ButtonVisibilityStop => !_timerStore.IsStartButtonVisible;

        public bool LogoVisibility => _logoStore.IsLogoVisible;
        public bool EffektiveSpielzeitVisibility => _spielzeitStore.EffektiveSpielzeitVisibility;

        // Properties, die von der View abgefragt werden, um Informationen darzustellen
        public string Spielzeit
        {
            get { return _spielzeitStore.Spielzeit; }
        }
        public int SpielMinute
        { get { return _spielzeitStore.SpielMinute; } }
        public int SpielSekunde
        { get { return _spielzeitStore.SpielSekunde; } }
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
            _timerStore.Reset();
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
            _timerStore = new TimerStore();
            _strafenGast = new StrafenStore(this);
            _strafenHeim = new StrafenStore(this);
            _logoStore = new LogoStore(this);
            _spielzeitStore = new SpielzeitStore(this);

            // EventBinding
            _timerStore.OnButtonVisibilityChanged += TimerStore_ButtonVisibilityChanged;
            _timerStore.OnTimerElapsed += TimerStore_TimerElapsed;

            _spielzeitStore.OnSpielzeitChanged += SpielzeitStore_SpielzeitChanged;
            _spielzeitStore.EffektiveSpielzeitVisibilityChanged += SpielzeitStore_EffektiveSpielzeitVisibilityChanged;
            _spielzeitStore.OnSpielzeitAbgelaufen += SpielzeitStore_SpielzeitAbgelaufen;
            _spielzeitStore.StartPausenzeit += SpielzeitStore_StartPausenzeit;

            _strafenHeim.OnStrafenChanged += StrafenHeim_StrafenChanged; 
            _strafenGast.OnStrafenChanged += StrafenGast_StrafenChanged;
            _strafenHeim.OnStrafenAnzeigeGroesseChanged += StrafenHeim_OnStrafenAnzeigeGroesseChanged;
            _strafenGast.OnStrafenAnzeigeGroesseChanged += StrafenGast_OnStrafenAnzeigeGroesseChanged;


            _logoStore.OnLogoVisibilityChanged += LogoStore_LogoVisibilityChanged; ;

            // Zuteilung fuer Buttons
            // Buttons fuer die Kontrolle des Punktestandes
            GastScoreUp = new ScoreCommand(this, Team.Gast, StandVeraenderung.Hoch);
            GastScoreDown = new ScoreCommand(this, Team.Gast, StandVeraenderung.Runter);
            HeimScoreUp = new ScoreCommand(this, Team.Heim, StandVeraenderung.Hoch);
            HeimScoreDown = new ScoreCommand(this, Team.Heim, StandVeraenderung.Runter);

            // Buttons fuer die Kontrolle der Spielzeit
            StartTime = new TimeCommand(_timerStore, _spielzeitStore, ZeitAktion.Start);
            StopTime = new TimeCommand(_timerStore, _spielzeitStore, ZeitAktion.Stop);
            ResetTime = new TimeCommand(_timerStore, _spielzeitStore, ZeitAktion.Reset);
            SpaceButton = new TimeCommand(_timerStore, _spielzeitStore, ZeitAktion.Space);
            TimeMinusOne = new TimeCommand(_timerStore, _spielzeitStore, ZeitAktion.MinusOne);
            TimePlusOne = new TimeCommand(_timerStore, _spielzeitStore, ZeitAktion.PlusOne);
            StartPausenzeit = new TimeCommand(_timerStore, _spielzeitStore, ZeitAktion.StartPausenzeit);



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

        private void TimerStore_ButtonVisibilityChanged()
        {
            OnPropertyChanged("ButtonVisibilityStart");
            OnPropertyChanged("ButtonVisibilityStop");
        }
        private void TimerStore_TimerElapsed()
        {
            //_strafenGast.CheckIfStrafeStillActive();
            //_strafenHeim.CheckIfStrafeStillActive();
            _spielzeitStore.TimerElapsed();
        }

        private void SpielzeitStore_EffektiveSpielzeitVisibilityChanged()
        {
            OnPropertyChanged("EffektiveSpielzeitVisibility");
        }
        private void SpielzeitStore_SpielzeitChanged()
        {
            OnPropertyChanged("Spielzeit");
        }
        private void SpielzeitStore_SpielzeitAbgelaufen()
        {
            _timerStore.Stop();
        }
        private void SpielzeitStore_StartPausenzeit()
        {
            _timerStore.Start();
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
    }
}
